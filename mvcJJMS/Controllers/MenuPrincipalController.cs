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
            int login = SysFacade.Login(email,password); 
            
            if(login==0){
                action="Sucesso";
            }else if(login==1){
                action="EmailInexistente";
            }else if(login==2){
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

        public ActionResult RealizarRegisto(string user,string password, string email, string morada, string telefone){
            string action="Registar";
            int result=SysFacade.Registar(user,password,email,morada,telefone);

            switch(result){
                case 1:             //username repetido

                    break;
                case 2:             //email repetido

                    break;
                case 3:             //email inválido

                    break;
                case 4:             //telefone inválido

                    break;
                case 5:             //password insegura

                    break;
                case 0:             //sucesso
                    action="sucesso";
                    break;   
            }

            return RedirectToAction(action);
        }

    }
}