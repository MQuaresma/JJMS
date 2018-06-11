using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Net.Mail;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using mvcJJMS.Data;
using mvcJJMS.Models;

using FILE=System.String;
using Date = System.String;
using Time = System.String;

namespace mvcJJMS.Controllers{
    public class ClienteController : Controller{

        private readonly JJMSContext _context;
		private readonly UtilizadorController _uController;

		public ClienteController(JJMSContext context, UtilizadorController uController){
			_context=context;
			_uController=uController;
		}

		/// <summary>
		/// Checks input and if all is right, save the Cliente to the DataBase
		/// </summary>
		/// <param name="user"></param>
		/// <param name="password"></param>
		/// <param name="email"></param>
		/// <param name="morada"></param>
		/// <param name="telefone"></param>
		/// <returns>returns the correspondent Action</returns>
		public ActionResult Registar(string user,string password, string email, string morada, string telefone){
			if(_uController.emailAssociado(email)){
				return EmailEmUso();
			}
			if(this.telefoneValido(telefone)==false){
				return TelefoneInvalido();
			}
			if(this.passwordSegura(password)==false){
				return PasswordInsegura();
			}

			Cliente nCliente = _context.newCliente(user,UtilizadorController.hashFunction(password),email,morada,telefone);
			_context.Clientes.Add(nCliente);
            _context.SaveChanges();
			
			return Sucesso();
		}

		/// <summary>
		/// Checks if a string is a valid phone number
		/// </summary>
		/// <param name="telefone">phone number to analyze</param>
		/// <returns>TRUE if the string is a valid phone numer, else FALSE</returns>
		public bool telefoneValido( string telefone) {
			if (telefone.Length != 9) return false;
			foreach (char c in telefone){
				if (!Char.IsDigit(c)) return false;
			}
			return true;
		}

		/// <summary>
		/// Checks if a password is safe enough for the company
		/// </summary>
		/// <param name="password">password to analyze</param>
		/// <returns>TRUE if the password is safe, else FALSE</returns>		
		public bool passwordSegura( string password) {
			if (password.Length < 8) return false;
			int numeros = 0;
			int letras = 0;
			int simbolos = 0;
			foreach (char c in password){
				if (Char.IsDigit(c)) numeros++;
				else if (Char.IsLetter(c)) letras++;
				else simbolos++;
			}
			if (numeros == 0 || letras == 0 || simbolos == 0) return false;
			return true;
		}   

		/// <summary>
		/// Retrieves the address associated with a client
		/// </summary>
		/// <param name="idCliente">Unique identifier for a client</param>
		/// <returns>Client address</returns>
        public string GetClienteMorada( int idCliente) {
			Cliente cliente = _context.Clientes.Find(idCliente);
			return cliente.Morada;
		}

		/// <summary>
		/// Retrieves the phone number associated with a client
		/// </summary>
		/// <param name="idCliente">Unique identifier for a client</param>
		/// <returns>Client phone number</returns>
        public string GetClienteTelefone( int idCliente) {
			Cliente cliente = _context.Clientes.Find(idCliente);
			return cliente.Telefone;
		}

		/// <summary>
		/// Updates the address of a client
		/// </summary>
		/// <param name="idCliente">Unique identifier for a client</param>
		/// <param name="moradaInput">New address</param>
        public void UpdateMorada( int idCliente, string moradaInput) {
			Cliente cliente = _context.Clientes.Find(idCliente);
			cliente.Morada = moradaInput;
			_context.SaveChanges();

		}

		/// <summary>
		/// Updates the phone number of a client
		/// </summary>
		/// <param name="idCliente">Unique identifier for a client</param>
		/// <param name="telefoneInput">New phone number</param>
		public void UpdateTelefone( int idCliente,  string telefoneInput) {
			Cliente cliente = _context.Clientes.Find(idCliente);
			cliente.Telefone = telefoneInput;
			_context.SaveChanges();
		}

		/// <summary>
		/// Blocks a particular client
		/// </summary>
		/// <param name="idCliente">Unique identifier for a client to block</param>
		public void Bloquear( int idCliente) {
			Cliente cliente = _context.Clientes.Find(idCliente);
			cliente.Bloqueia();
			_context.SaveChanges();
		}

		/// <summary>
		/// Performs payment for a given order
		/// </summary>
		/// <param name="idCliente">Unique identifier for a client</param>
		/// <param name="idEncomenda">Unique identifier for a single order</param>
		/// <returns>TRUE if payment is successful, else FALSE</returns>
		public bool TransfereMontante( int idCliente,  int idEncomenda) {
			bool existe = ExisteEncomendaCliente(idCliente,idEncomenda);
			bool suc = false;

			if(existe){
				Encomenda enc = _context.Encomendas.Find(idEncomenda);
				CartaoCredito cc = _context.Cartoes.Find(enc.getCartaoCreditoID());
				suc = cc.Pagamento();
			}
			return suc;
		}

		/// <summary>
		/// Generates order details for a given order
		/// </summary>
		/// <param name="idCliente">Unique identifier for a client</param>
		/// <param name="idEncomenda">Unique identifier for a single order</param>
		public void GerarFatura( int idCliente,  int idEncomenda) {
			Cliente cliente = _context.Clientes.Find(idCliente);
			Encomenda enc = _context.Encomendas.Find(idEncomenda);
			enc.gerarFatura(cliente);
		}

		/// <summary>
		/// Retrieves order details associated with a given order
		/// </summary>
		/// <param name="idCliente">Unique identifier for a client</param>
		/// <param name="idEncomenda">Unique identifier for a single order</param>
		/// <returns>Order details if they exist, else NULL</returns>
        public FILE GetFatura( int idCliente,  int idEncomenda) {
			bool existe = ExisteEncomendaCliente(idCliente,idEncomenda);
			
			if(existe==true){
				Encomenda enc = _context.Encomendas.Find(idEncomenda);
				FILE file = enc.fatura;
				return file;
			}else return null;
		}

		/// <summary>
		/// Retrieves a client's order history
		/// </summary>
		/// <param name="idCliente">Unique identifier for a client</param>
		/// <returns>Order history of the given client</returns>
        public async Task<List<Encomenda>> GetHistoricoEnc( int idCliente) {
			return await _context.Encomendas.Where(e => e.ClienteID==idCliente).ToListAsync();
		}

		/// <summary>
		/// Checks if a given order is associated with a certain client
		/// </summary>
		/// <param name="idCliente">Unique identifier for a client</param>
		/// <param name="idEncomenda">Unique identifier for a single order</param>
		/// <returns>TRUE if the order is associated, else FALSE</returns>
        public bool ExisteEncomendaCliente( int idCliente,  int idEncomenda) {
			Cliente cliente = _context.Clientes.Find(idCliente);
			return cliente.TemEncomenda(idEncomenda);
		}

		/// <summary>
		/// Checks if a given client is blocked
		/// </summary>
		/// <param name="idCliente">Unique identifier for a client</param>
		/// <returns>TRUE if the client is blocked, else FALSE</returns>
        public bool EstaBloqueado( int idCliente) {
			Cliente cliente = _context.Clientes.Find(idCliente);
			return cliente.Bloqueado;
		}

		public ViewResult EmailEmUso(){
			ViewBag.Title = "Email em uso";
			ViewBag.Msg = "O email inserido já se encontra associado a outro cliente"; 
			ViewBag.Func = "Index";
            ViewBag.File = "MenuPrincipal";
            ViewBag.ButtonName = "Ok";
            return View("~/Views/Shared/SimpleMsg.cshtml");
		}

		public ViewResult PasswordInsegura(){
			ViewBag.Title = "Password Insegura";
			ViewBag.Msg = "A password não cumpre requisitos mínimos de segurança:"; 
			ViewBag.Item1 = "* 8 ou mais carateres";
			ViewBag.Item2 = "* possuir números, letras e símbolos";
			ViewBag.Func = "Index";
            ViewBag.File = "MenuPrincipal";
            ViewBag.ButtonName = "Ok";
			return View("~/Views/Shared/PasswordInsegura.cshtml"); 
		}

		public ViewResult EmailInvalido(){
			ViewBag.Title = "Email inválido";
			ViewBag.Msg = "O email inserido não é válido"; 
			ViewBag.Func = "Index";
            ViewBag.File = "MenuPrincipal";
            ViewBag.ButtonName = "Ok";
            return View("~/Views/Shared/SimpleMsg.cshtml");
		}

		public ViewResult Sucesso(){
			ViewBag.Title = "Sucesso";
			ViewBag.Msg = "Registo efetuado com sucesso"; 
			ViewBag.Func = "Index";
            ViewBag.File = "MenuPrincipal";
            ViewBag.ButtonName = "Ok";
            return View("~/Views/Shared/SimpleMsg.cshtml");
		}

		public ViewResult TelefoneInvalido(){
			ViewBag.Title = "Telefone inválido";
			ViewBag.Msg = "O telefone inserido não é válido"; 
			ViewBag.Func = "Index";
            ViewBag.File = "MenuPrincipal";
            ViewBag.ButtonName = "Ok";
            return View("~/Views/Shared/SimpleMsg.cshtml");
		}

		public ViewResult Cancelar(){
			ViewBag.Title = "Cancelar";
			ViewBag.Msg = "Operação Cancelada";
			ViewBag.Func = "Index";
            ViewBag.File = "MenuPrincipal";
            ViewBag.ButtonName = "Ok";
            return View("~/Views/Shared/SimpleMsg.cshtml");
		}

		/// <summary>
		/// Performs payment for a given order and client, if the payment isn't successfully it blocks the client
		/// </summary>
		/// <param name="idCliente">Unique identifier for a client</param>
		/// <param name="idEncomenda">Unique identifier for a single order</param>
		public void PagarServiço(int idCliente, int idEncomenda){
			bool sucesso = TransfereMontante(idCliente,idEncomenda);
			if(!sucesso) Bloquear(idCliente);
			else GerarFatura(idCliente,idEncomenda);
		} 
    }
}
