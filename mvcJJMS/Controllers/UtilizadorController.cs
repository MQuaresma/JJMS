using Microsoft.AspNetCore.Mvc;
using mvcJJMS.Data;
using mvcJJMS.Models;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace mvcJJMS.Controllers{
    public class UtilizadorController : Controller{

        private readonly JJMSContext _context;
		private int utilizadorID;

		public UtilizadorController(JJMSContext context){
			_context=context;
			this.utilizadorID=-1;
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
						this.utilizadorID = uts[i].UtilizadorID;
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

		public bool emailAssociado( string email) {
			return (_context.Utilizadores.Where(ut => ut.Email.Equals(email)).FirstOrDefault() != default(Utilizador));
		}

        public string GetUserNome( int idCliente) {
			Cliente cli=_context.Clientes.Find(idCliente);
			if(cli!=null) return cli.Nome;
			else return null;
		}

		public int getUtilizadorId(){
			return this.utilizadorID;
		}

        public string GetUserPassword( int idCliente) {
			throw new System.Exception("Not implemented");
		}

		public string GetUserEmail( int idCliente) {
			throw new System.Exception("Not implemented");
		}

		public void UpdateNome( int idCliente,  string nomeInput) {
			throw new System.Exception("Not implemented");
		}

		public void UpdatePassword( int idCliente,  string passwordInput) {
			throw new System.Exception("Not implemented");
		}

		public void UpdateEmail( int idCliente,  string emailInput) {
			throw new System.Exception("Not implemented");
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
