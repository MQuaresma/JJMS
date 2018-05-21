using Microsoft.AspNetCore.Mvc;
using mvcJJMS.Data;
using mvcJJMS.Models;

namespace mvcJJMS.Controllers
{
    public class RegistarController : Controller
    {
        static private JJMSContext _context;

		public RegistarController(JJMSContext context){
			RegistarController._context=context;
		}

        public ActionResult RealizarRegisto(string user,string password, string email, string morada, string telefone){
			int registar = SysFacadeController.Registar(password,email,telefone);
			switch (registar){
				case 1:
					Cliente nCliente = _context.newCliente(user,SysFacadeController.hashFunction(password),email,morada,telefone);
					_context.Clientes.Add(nCliente);
            		_context.SaveChanges();
					return RedirectToAction("Sucesso", "Registar");
				case 2:
					return RedirectToAction("TelefoneInvalido", "Registar");
				case 3:
					return RedirectToAction("PasswordInsegura", "Registar");
				case 4:
					return RedirectToAction("EmailInvalido", "Registar");
				default:
					return RedirectToAction("EmailEmUso", "Registar");
			}
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