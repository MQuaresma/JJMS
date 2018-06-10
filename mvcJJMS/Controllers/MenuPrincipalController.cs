using Microsoft.AspNetCore.Mvc;

namespace mvcJJMS.Controllers{
    public class MenuPrincipalController : Controller{
        public ViewResult Index(){
            ViewBag.Title="Menu Principal";
            ViewBag.ListElem1="Login";
            ViewBag.ListElem2="Registar"; 
            return View(); 
        }

        /// <summary>
        /// Wrapper for the Login method on the UtilizadorController class
        /// </summary>
        /// <returns>View of the login page</returns>
        public ViewResult Login(){
            ViewBag.Title="Login";
            return View("~/Views/Login/Index.cshtml");
        }

        /// <summary>
        /// Wrapper for the Registar method on the ClienteController class
        /// </summary>
        /// <returns>View of the registration page</returns>
        public ViewResult Registar(){
            ViewBag.Title="Registar Utilizador";
            return View("~/Views/Registar/Index.cshtml");
        }
    }
}
