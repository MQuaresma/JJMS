using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System;
using mvcJJMS.Data;
using mvcJJMS.Models;
using FILE = System.String;
using Date = System.String;
using Time = System.String;

namespace mvcJJMS.Controllers{
	public class SysFacadeController : Controller {
		static private string moradaCD;
		static private JJMSContext _context;

		public SysFacadeController(JJMSContext context){
			SysFacadeController._context=context;
		}

        static public void iniciar(string mCD){
			SysFacadeController.moradaCD=mCD;
			//TODO: buscar os restos dos valores à BD
		}

		static public bool ExisteEncomenda(int idEncomenda) {
			return SysFacadeController._context.Encomendas.Find(idEncomenda)!=null;
		}

		static public bool EmailAssociado( string email) {
			return SysFacadeController._context
									  .Utilizadores
									  .Where(ut => ut.Email.Equals(email))
									  .FirstOrDefault() != default(Utilizador);
		}

		static public string GetUserNome( int idCliente) {
			Cliente cli=SysFacadeController._context.Clientes.Find(idCliente);
			if(cli!=null) return cli.Nome;
			else return null;
		}

		static public string GetUserPassword( int idCliente) {
			throw new System.Exception("Not implemented");
		}

		static public string GetUserEmail( int idCliente) {
			throw new System.Exception("Not implemented");
		}

		static public string GetClienteMorada( int idCliente) {
			throw new System.Exception("Not implemented");
		}

		static public string GetClienteTelefone( int idCliente) {
			throw new System.Exception("Not implemented");
		}

		static public void UpdateNome( int idCliente,  string nomeInput) {
			throw new System.Exception("Not implemented");
		}

		static public void UpdatePassword( int idCliente,  string passwordInput) {
			throw new System.Exception("Not implemented");
		}

		static public void UpdateEmail( int idCliente,  string emailInput) {
			throw new System.Exception("Not implemented");
		}

		static public void UpdateMorada( int idCliente,  string moradaInput) {
			throw new System.Exception("Not implemented");
		}

		static public void UpdateTelefone( int idCliente,  string telefoneInput) {
			throw new System.Exception("Not implemented");
		}

		static public void UpdateCustoEnc( int idEncomenda,  float custoInput) {
			throw new System.Exception("Not implemented");
		}

		static public void UpdateEstadoEnc( int idEncomenda) {
			throw new System.Exception("Not implemented");
		}

		static public void Bloquear( int idCliente) {
			throw new System.Exception("Not implemented");
		}

		static public void TransfereMontante( int idCliente,  int idEncomenda) {
			throw new System.Exception("Not implemented");
		}

		static public void GerarFatura( int idCliente,  int idEncomenda) {
			throw new System.Exception("Not implemented");
		}

		static public int GetFuncionarioResp( int idEncomenda) {
			throw new System.Exception("Not implemented");
		}

		public ActionResult Login(string email, string password) {
			Utilizador u = SysFacadeController._context.Utilizadores.Where(ut => ut.Email.Equals(email)).FirstOrDefault();
			ActionResult ret;

			if (u != default(Utilizador)){
				byte[] passwordH = hashFunction(password);
				if (passwordH.SequenceEqual(hashFunction(u.Password)))
					ret=RedirectToAction("Sucesso", "MenuPrincipal");
				else ret=RedirectToAction("PasswordInvalida","MenuPrincipal");
			}else ret=RedirectToAction("EmailInexistente", "MenuPrincipal");

			return ret;
		}

		static public int IdForn( string nomeForn) {
			throw new System.Exception("Not implemented");
		}

		static public bool CartaoValido( int numCartCredito,  int mes,  int ano,  int cvv,  string pais) {
			throw new System.Exception("Not implemented");
		}

		static public int DelegarFuncionario( int idEncomenda,  string destino) {
			throw new System.Exception("Not implemented");
		}

		static public void SetEncomenda( int idCliente,  string nomeForn,  string morada,  Date dia,  Time hora,  int numCartCredito,  int mes,  int ano,  int cvv,  string pais) {
			throw new System.Exception("Not implemented");
		}

		static public int CalcularRota( string origem,  string destino) {
			throw new System.Exception("Not implemented");
		}

		static public string GetDestinoEnc( int idEncomenda) {
			throw new System.Exception("Not implemented");
		}

		static public string GetMoradaForn( int idEncomenda) {
			throw new System.Exception("Not implemented");
		}

		static public int GetEstadoEnc( int idEncomenda) {
			throw new System.Exception("Not implemented");
		}

		static public bool TelefoneValido( string telefone) {
			if (telefone.Length != 9) return false;
			foreach (char c in telefone){
        		if (!Char.IsDigit(c)) return false;
			}
    		return true;
		}

		static public bool PasswordSegura( string password) {
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

        public ActionResult RealizarRegisto(string user,string password, string email, string morada, string telefone){
			int registar = Registar(password,email,telefone);
			switch (registar){
				case 1:
					Cliente nCliente = _context.newCliente(user,password,email,morada,telefone);
					_context.Clientes.Add(nCliente);
					//Utilizador nUser=_context.newUtilizador(user, password, email);
            		//_context.Utilizadores.Add(nUser);
            		_context.SaveChanges();
					return RedirectToAction("Registar_Sucesso", "MenuPrincipal");
				case 2:
					return RedirectToAction("Registar_EmailEmUso", "MenuPrincipal");
				case 3:
					return RedirectToAction("Registar_TelefoneInvalido", "MenuPrincipal");
				default:
					return RedirectToAction("Registar_PasswordInsegura", "MenuPrincipal");
			}
        }
		
		static public int Registar( string password, string email, string telefone) {
			if (EmailAssociado(email) == true) return 2;
			else if (TelefoneValido(telefone) == false) return 3;
			else if (PasswordSegura(password) == false) return 4;
			else return 1;
		}
		
		static public string GetLocalizacaoEncomenda( int idEncomenda) {
			throw new System.Exception("Not implemented");
		}

		static public string GetEstadoEncomenda( int idEncomenda) {
			throw new System.Exception("Not implemented");
		}

		static public FILE GetFatura( int idCliente,  int idEncomenda) {
			throw new System.Exception("Not implemented");
		}

		static public List<Encomenda> GetHistoricoEnc( int idCliente) {
			throw new System.Exception("Not implemented");
		}

		static public bool EncomendaEntregue( int idEncomenda) {
			throw new System.Exception("Not implemented");
		}

		static public void Avalia( int idEncomenda,  int classServicoEntrega,  int classEstadoEncomenda) {
			throw new System.Exception("Not implemented");
		}

		static public bool ExisteEncomendaCliente( int idCliente,  int idEncomenda) {
			throw new System.Exception("Not implemented");
		}

		static public string GetMoradaCD() {
			return SysFacadeController.moradaCD;
		}

		static public bool Luhn_check( int num_cart_cred) {
			throw new System.Exception("Not implemented");
		}

		static public void EnviarEmail( int idFunc,  int idEncomenda) {
			throw new System.Exception("Not implemented");
		}

		static public int GetZona( string morada) {
			throw new System.Exception("Not implemented");
		}

		static public bool EstaBloqueado( int idCliente) {
			throw new System.Exception("Not implemented");
		}

		static private byte[] hashFunction(string input){
			var sha384 = new SHA384CryptoServiceProvider();
			return sha384.ComputeHash(Encoding.UTF8.GetBytes(input));
		}
	}
}