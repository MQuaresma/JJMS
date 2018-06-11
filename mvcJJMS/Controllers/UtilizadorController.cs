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

		/// <summary>
		/// Logs a user into the system
		/// </summary>
		/// <param name="email">User email</param>
		/// <param name="password">User password</param>
		/// <returns>Redirects to a Menu or an Error view</returns>
        public ActionResult Autenticar(string email, string password) {
			int login=this.Login(email,password);

			switch(login){
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

		/// <summary>
		/// Checks if the given credentials are valid
		/// </summary>
		/// <param name="email">User email</param>
		/// <param name="password">User password</param>
		/// <returns>0 if the User is a client, 1 if the User is an employee, 
		/// 		 2 if the email is invalid, 3 if the password is invalid</returns>
		public int Login(string email, string password){
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
			return ret;
		}

		/// <summary>
		/// Returns the ID of the user currently logged in
		/// </summary>
		/// <returns>Unique identifier of the user</returns>
		public int getUtilizadorID(){
			return utilizadorID;
		}

		/// <summary>
		/// Checks whether there's a registered user with the given email
		/// </summary>
		/// <param name="email">Email address to check for</param>
		/// <returns>TRUE if there is a user registered with the email else FALSE</returns>
		public bool emailAssociado( string email) {
			return (_context.Utilizadores.Where(ut => ut.Email.Equals(email)).FirstOrDefault() != default(Utilizador));
		}

		/// <summary>
		/// Retrieves the username associated with the given user ID
		/// </summary>
		/// <param name="idUser">Unique identifier of the user</param>
		/// <returns>Username associated with the identifier</returns>
        public string GetUserNome( int idUser) {
			Utilizador user=_context.Utilizadores.Find(idUser);
			return user.Nome;
		}

		/// <summary>
		///	Retrieves the password associated with the given user ID
		/// </summary>
		/// <param name="idUser">Unique identifier of the user</param>
		/// <returns>Hashed byte array of user password</returns>
        public byte[] GetUserPassword( int idUser) {
			Utilizador user =_context.Utilizadores.Find(idUser);
			return user.Password;
		}

		/// <summary>
		/// Retrieves the email associated with the given user ID
		/// </summary>
		/// <param name="idUser">Unique identifier of the user</param>
		/// <returns>Email associated with the identifier</returns>
		public string GetUserEmail( int idUser) {
			Utilizador user =_context.Utilizadores.Find(idUser);
			return user.Email;
		}

		/// <summary>
		/// Updates the username associated with the given user ID
		/// </summary>
		/// <param name="idUser">Unique identifier of the user</param>
		/// <param name="nomeInput">New username</param>
		public void UpdateNome( int idUser,  string nomeInput) {
			Utilizador user = _context.Utilizadores.Find(idUser);
			user.Nome = nomeInput;
			_context.SaveChanges();
		}

		/// <summary>
		/// Updates the password associated with the given user ID
		/// </summary>
		/// <param name="idUser">Unique identifier of the user</param>
		/// <param name="passwordInput">New password</param>
		public void UpdatePassword( int idUser, string passwordInput) {
			Utilizador user = _context.Utilizadores.Find(idUser);
			user.Password = hashFunction(passwordInput);
			_context.SaveChanges();
		}

		/// <summary>
		/// Updates the email associated with the given user ID
		/// </summary>
		/// <param name="idUser">Unique identifier of the user</param>
		/// <param name="emailInput">New email</param>
		public void UpdateEmail( int idUser,  string emailInput) {
			Utilizador cliente = _context.Utilizadores.Find(idUser);
			cliente.Email = emailInput;
			_context.SaveChanges();
		}


		/// <summary>
		/// Performas a sha384 hash on the input string
		/// </summary>
		/// <param name="input">String to perform the hash on</param>
		/// <returns>SHA384 hash output</returns>
		static public byte[] hashFunction(string input){
			var sha384 = new SHA384CryptoServiceProvider();
			return sha384.ComputeHash(Encoding.UTF8.GetBytes(input));
		}

		public ViewResult EmailInexistente(){
			ViewBag.Title = "Email Inexistente";
			ViewBag.Msg = "O email inserido não existe.";
			ViewBag.Func = "Index";
            ViewBag.File = "MenuPrincipal";
            ViewBag.ButtonName = "Voltar";
            return View("~/Views/Shared/SimpleMsg.cshtml");
		}

		public ViewResult PasswordInvalida(){
    		ViewBag.Title = "Password Inválida";
    		ViewBag.Msg = "A password inserida é inválida.";	
			ViewBag.Func = "Index";
            ViewBag.File = "MenuPrincipal";
            ViewBag.ButtonName = "Voltar";
            return View("~/Views/Shared/SimpleMsg.cshtml");
		}
    }
}