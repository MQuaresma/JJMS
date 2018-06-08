using Microsoft.AspNetCore.Mvc;

namespace mvcJJMS.Controllers{
    public class MenuPrincipalController : Controller{
        public ViewResult Index(){
            ViewBag.Title="Menu Principal";
            ViewBag.ListElem1="Login";
            ViewBag.ListElem2="Registar"; 
            return View(); 
        }

        public ViewResult Requisitar_Encomenda(){
            ViewBag.Title="Requisitar Encomenda";
            return View(); 
        }
        public ViewResult Consultar_Historico(){
            ViewBag.Title="Consultar Histórico";
            return View();
        }
        public ViewResult Avaliar_Servico(){
            ViewBag.Title="Avaliar Serviço";
            return View(); 
        }
        public ViewResult Alterar_Dados(){
            ViewBag.Title="Alterar Dados";
            return View(); 
        }

        public ViewResult ConsultarRota(){
            ViewBag.Title="Consultar Rota";
            return View(); 
        }
        public ViewResult AtualizarEstado(){
            ViewBag.Title="Atualizar Estado";
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
