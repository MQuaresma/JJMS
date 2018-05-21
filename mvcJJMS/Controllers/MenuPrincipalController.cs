using Microsoft.AspNetCore.Mvc;

namespace mvcJJMS.Controllers{
    public class MenuPrincipalController : Controller{
        public ViewResult Index(){
            ViewBag.Title="Menu Principal";
            ViewBag.ListElem1="Login";
            ViewBag.ListElem2="Registar"; 
            ViewBag.To="MenuPrincipal";
            SysFacadeController.iniciar("Avenida da Liberdade nº36, Braga"); 
            return View(); 
        }

		public ActionResult Login(){
			ViewBag.Title = "Autenticar";
            return View(); 
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
        
        public ViewResult MenuCliente(){
            ViewBag.Title="Menu Cliente";
            ViewBag.ListElem1="Requisitar Encomenda";
            ViewBag.ListElem2="Consultar Histórico";
            ViewBag.ListElem3="Tracking da Encomenda";
            ViewBag.ListElem4="Avaliar Serviço";
            ViewBag.ListElem5="Alterar Dados";
            ViewBag.ListElem6="Logout";
            ViewBag.To="MenuPrincipal";
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

        public ViewResult MenuFuncionario(){
            ViewBag.Title = "Menu Funcionário";
            ViewBag.ListElem1 = "Consultar Rota";
            ViewBag.ListElem1View = "ConsultarRota";
            ViewBag.ListElem2 = "Atualizar Estado";
            ViewBag.ListElem2View = "AtualizarEstado";
            ViewBag.ListElem3 = "Logout";
            ViewBag.ListElem3View = "Index";
            ViewBag.To="MenuPrincipal";
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
    }
}