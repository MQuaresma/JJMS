using Microsoft.AspNetCore.Mvc;

namespace mvcJJMS.Controllers{
    public class MenuPrincipalController : Controller{
        public ViewResult Index(){
            ViewBag.Title="Menu Principal";
            ViewBag.ListElem1="Login";
            ViewBag.ListElem2="Registar"; 
            return View(); 
        }

        public ViewResult Login(){
            ViewBag.Title="Login";
            return View("~/Views/Login/Index.cshtml");
        }

        public ViewResult Registar(){
            ViewBag.Title="Registar Utilizador";
            return View("~/Views/Registar/Index.cshtml");
        }
    }
}
