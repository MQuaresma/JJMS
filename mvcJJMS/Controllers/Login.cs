using Microsoft.AspNetCore.Mvc;
namespace mvcJJMS.Controllers

{
    public class Login : Controller
    {
        public ActionResult Index(){
			ViewBag.Title = "Autenticar";
            return View(); 
        }

        public ActionResult Autenticar(string email, string password){
            ActionResult ret;
            int login = SysFacadeController.Login(email,password);
            
            switch(login){
                case 0:
                    ret=RedirectToAction("MenuCliente", "Login");
                    break;
                case 1:
                    ret = RedirectToAction("MenuFuncionario", "Login");
                    break;
                case 2:
                    ret=RedirectToAction("EmailInexistente", "Login");
                    break;
                case 3:
                    ret = ret=RedirectToAction("PasswordInvalida","Login");
                    break;
                default:
                    ret = RedirectToAction("Index","Login");
                    break;
            }
            return ret;
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
    }
}