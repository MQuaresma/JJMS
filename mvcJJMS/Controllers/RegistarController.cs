using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Mail;

namespace mvcJJMS.Controllers
{
    public class RegistarController : Controller
    {
        public ActionResult Registar(string user,string password, string email, string morada, string telefone){
			
            if(SysFacadeController.EmailAssociado(email)){
                return RedirectToAction("EmailEmUso", "Registar");
            }
            if(TelefoneValido(telefone)==false){
                return RedirectToAction("TelefoneInvalido", "Registar");
            }
            if(PasswordSegura(password)==false){
                return RedirectToAction("PasswordInsegura", "Registar");
            }
            if(EmailValido(email)==false){
                return RedirectToAction("EmailInvalido", "Registar");
            }

            SysFacadeController.Registar(user,password,email,morada,telefone);
            return RedirectToAction("Sucesso", "Registar");
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

        static public bool EmailValido( string email){
			MailAddress address;
			try{
				address = new MailAddress(email);
			}
			catch (FormatException){
				return false;
			}
			return true;
		}

        public ViewResult Index(){
            ViewBag.Title = "Registar"; 
            return View();
        }

        public ViewResult Cancelar(){
            ViewBag.Title = "Cancelar";
            ViewBag.Msg = "Operação cancelada"; 
            return View(); 
        }

        public ViewResult EmailEmUso(){
            ViewBag.Title = "Email em uso";
            ViewBag.Msg = "O email inserido já se encontra associado a outro cliente"; 
            return View(); 
        }

        public ViewResult PasswordInsegura(){
            ViewBag.Title = "Password Insegura";
            ViewBag.Msg = "A password não cumpre requisitos mínimos de segurança"; 
            ViewBag.Item1 = "* 8 ou mais carateres";
            ViewBag.Item2 = "* possuir números, letras e símbolos";
            return View(); 
        }

        public ViewResult EmailInvalido(){
            ViewBag.Title = "Email inválido";
            ViewBag.Msg = "O email inserido não é válido"; 
            return View(); 
        }

        public ViewResult Sucesso(){
            ViewBag.Title = "Sucesso";
            ViewBag.Msg = "Registo efetuado com sucesso"; 
            return View(); 
        }

        public ViewResult TelefoneInvalido(){
            ViewBag.Title = "Telefone inválido";
            ViewBag.Msg = "O telefone inserido não é válido"; 
            return View(); 
        }
    }
}