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

namespace mvcJJMS.Controllers{
    public class ClienteController : Controller{

        private readonly JJMSContext _context;
		private readonly UtilizadorController _uController;

		public ClienteController(JJMSContext context, UtilizadorController uController){
			_context=context;
			_uController=uController;
		}

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
			if(this.emailValido(email)==false){
				return EmailInvalido();
			}

			Cliente nCliente = _context.newCliente(user,UtilizadorController.hashFunction(password),email,morada,telefone);
			_context.Clientes.Add(nCliente);
            _context.SaveChanges();
			
			return Sucesso();
		}

		public bool telefoneValido( string telefone) {
			if (telefone.Length != 9) return false;
			foreach (char c in telefone){
				if (!Char.IsDigit(c)) return false;
			}
			return true;
		}

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

		public bool emailValido( string email){
			MailAddress address;
			try{
				address = new MailAddress(email);
			}catch (FormatException){
				return false;
			}
			return true;
		} 

        public string GetClienteMorada( int idCliente) {
			throw new System.Exception("Not implemented");
		}

        public string GetClienteTelefone( int idCliente) {
			throw new System.Exception("Not implemented");
		}

        public void UpdateMorada( int idCliente,  string moradaInput) {
			throw new System.Exception("Not implemented");
		}

		public void UpdateTelefone( int idCliente,  string telefoneInput) {
			throw new System.Exception("Not implemented");
		}

		public void Bloquear( int idCliente) {
			throw new System.Exception("Not implemented");
		}

		public void TransfereMontante( int idCliente,  int idEncomenda) {
			throw new System.Exception("Not implemented");
		}

		public void GerarFatura( int idCliente,  int idEncomenda) {
			throw new System.Exception("Not implemented");
		}

        public FILE GetFatura( int idCliente,  int idEncomenda) {
			throw new System.Exception("Not implemented");
		}

        public async Task<List<Encomenda>> GetHistoricoEnc( int idCliente) {
			return await _context.Encomendas.Where(e=>e.getClienteID()==idCliente).ToListAsync();
		}

        public bool ExisteEncomendaCliente( int idCliente,  int idEncomenda) {
			throw new System.Exception("Not implemented");
		}

        public bool EstaBloqueado( int idCliente) {
			throw new System.Exception("Not implemented");
		}

		public ViewResult EmailEmUso(){
			ViewBag.Title = "Email em uso";
			ViewBag.Msg = "O email inserido já se encontra associado a outro cliente"; 
			return View("~/Views/Registar/EmailEmUso.cshtml"); 
		}

		public ViewResult PasswordInsegura(){
			ViewBag.Title = "Password Insegura";
			ViewBag.Msg = "A password não cumpre requisitos mínimos de segurança"; 
			ViewBag.Item1 = "* 8 ou mais carateres";
			ViewBag.Item2 = "* possuir números, letras e símbolos";
			return View("~/Views/Registar/PasswordInsegura.cshtml"); 
		}

		public ViewResult EmailInvalido(){
			ViewBag.Title = "Email inválido";
			ViewBag.Msg = "O email inserido não é válido"; 
			return View("~/Views/Registar/EmailInvalido.cshtml"); 
		}

		public ViewResult Sucesso(){
			ViewBag.Title = "Sucesso";
			ViewBag.Msg = "Registo efetuado com sucesso"; 
			return View("~/Views/Registar/Sucesso.cshtml"); 
		}

		public ViewResult TelefoneInvalido(){
			ViewBag.Title = "Telefone inválido";
			ViewBag.Msg = "O telefone inserido não é válido"; 
			return View("~/Views/Registar/TelefoneInvalido.cshtml"); 
		}
    }
}
