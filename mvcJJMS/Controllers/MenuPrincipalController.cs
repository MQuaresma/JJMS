using Microsoft.AspNetCore.Mvc;
using mvcJJMS.Models;
using System.Text.Encodings.Web;

namespace mvcJJMS.Controllers{
    public class MenuPrincipalController : Controller{
        public ViewResult Index(){
            ViewBag.Title="Menu Principal";
            ViewBag.ListElem1="Login";
            ViewBag.ListElem2="Registar"; 
            ViewBag.To="MenuPrincipal";
            SysFacade.iniciar("Avenida da Liberdade nº36, Braga"); 
            return View(); 
        }

        public ViewResult Login(){
            ViewBag.Title="Autenticar";
            return View(); 
        }
        
        [HttpPost]
        public ActionResult RealizarLogin(string email, string password){
            string action="Login";
            int isAuth = SysFacade.Login(email,password); 
            
            if(isAuth==0){
                action="Sucesso";
            }else if(isAuth==1){
                action="EmailInexistente";
            }else if(isAuth==2){
                action="PasswordInvalida";
            }
            return RedirectToAction(action); 
        }

        public ViewResult Sucesso(){
            ViewBag.Title = "Sucesso";
            ViewBag.Msg = "Login bem sucedido!";
            return View();
        }

        public ViewResult EmailInexistente(){
            ViewBag.Title = "Email Inexistente";
            ViewBag.Msg = "O email inserido não existe.";
            return View();
        }

        public ViewResult PasswordInvalida(){
            ViewBag.Title = "Password Inválida";
            ViewBag.Msg = "A password inserida é inválida.";
            return View();
        }

        public ViewResult Registar(){
            ViewBag.Title="Registar"; 
            return View(); 
        }

        public ViewResult RealizarRegisto(string user,string password, string email, string morada, string telefone){
            SysFacade.Registar(user,password,email,morada,telefone);
        }
    }
}