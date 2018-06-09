using Microsoft.AspNetCore.Mvc;
using mvcJJMS.Data;
using mvcJJMS.Models;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System;

namespace mvcJJMS.Controllers{
    public class UtilizadorController : Controller{
        private readonly JJMSContext _context;
		private static int utilizadorID=-1;

		public UtilizadorController(JJMSContext context){
			_context=context;
		}

        public ActionResult Login(string email, string password) {
			List<Utilizador> uts = _context.Utilizadores.ToList();
			bool found = false;
			int ret=2;
			byte[] pass = hashFunction(password);

			for(int i=0; i< uts.Count && !found; i++){
				string e = uts[i].Email;
				if (email.Equals(e)){
					found = true;
					ret=3;
					if (pass.SequenceEqual(uts[i].Password)){
						if (uts[i] is Cliente) ret=0;
						else ret = 1;
						utilizadorID = uts[i].UtilizadorID;
					}
				}
			}
			switch(ret){
				case 0:
                    return RedirectToAction("Index", "MenuCliente");
                case 1:
                    return RedirectToAction("Index", "MenuFuncionario");
                case 2:
                    return this.EmailInexistente();
                case 3:
                    return this.PasswordInvalida();
				default:
					return RedirectToAction("Index","MenuPrincipal");
			}
		}

		public int getUtilizadorID(){
			return utilizadorID;
		}

		public bool emailAssociado( string email) {
			return (_context.Utilizadores.Where(ut => ut.Email.Equals(email)).FirstOrDefault() != default(Utilizador));
		}

        public string GetUserNome( int idCliente) {
			Utilizador cliente=_context.Utilizadores.Find(idCliente);
			return cliente.Nome;
		}

        public string GetUserPassword( int idCliente) {
			Utilizador cliente =_context.Utilizadores.Find(idCliente);
			return System.Text.Encoding.UTF8.GetString(cliente.Password);
		}

		public string GetUserEmail( int idCliente) {
			Utilizador cliente =_context.Utilizadores.Find(idCliente);
			return cliente.Email;
		}

		public void UpdateNome( int idCliente,  string nomeInput) {
			Utilizador cliente = _context.Utilizadores.Find(idCliente);
			cliente.Nome = nomeInput;
			_context.SaveChanges();
		}

		public void UpdatePassword( int idCliente, string passwordInput) {
			Utilizador cliente = _context.Utilizadores.Find(idCliente);
			cliente.Password = hashFunction(passwordInput);
			_context.SaveChanges();
		}

		public void UpdateEmail( int idCliente,  string emailInput) {
			Utilizador cliente = _context.Utilizadores.Find(idCliente);
			cliente.Email = emailInput;
			_context.SaveChanges();
		}

		static public byte[] hashFunction(string input){
			var sha384 = new SHA384CryptoServiceProvider();
			return sha384.ComputeHash(Encoding.UTF8.GetBytes(input));
		}

		public ViewResult EmailInexistente(){
			ViewBag.Title = "Email Inexistente";
			ViewBag.Msg = "O email inserido não existe.";
			return View("~/Views/Login/EmailInexistente.cshtml");
		}

		public ViewResult PasswordInvalida(){
    		ViewBag.Title = "Password Inválida";
    		ViewBag.Msg = "A password inserida é inválida.";	
    		return View("~/Views/Login/PasswordInvalida.cshtml");
		}
    }
}