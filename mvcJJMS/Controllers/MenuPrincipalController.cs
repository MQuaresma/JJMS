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

        public ViewResult Registar(){
            ViewBag.Title = "Registar"; 
            return View();
        }

        public ViewResult Registar_Cancelar(){
            ViewBag.Title = "Cancelar";
            ViewBag.Msg = "Operação cancelada"; 
            return View(); 
        }

        public ViewResult Registar_EmailEmUso(){
            ViewBag.Title = "Email em uso";
            ViewBag.Msg = "O email inserido já se encontra associado a outro cliente"; 
            return View(); 
        }

        public ViewResult Registar_PasswordInsegura(){
            ViewBag.Title = "Password Insegura";
            ViewBag.Msg = "A password não cumpre requisitos mínimos de segurança"; 
            ViewBag.Item1 = "* 8 ou mais carateres";
            ViewBag.Item2 = "* possuir números, letras e símbolos";
            return View(); 
        }

        public ViewResult Registar_EmailInvalido(){
            ViewBag.Title = "Email inválido";
            ViewBag.Msg = "O email inserido não é válido"; 
            return View(); 
        }

        public ViewResult Registar_Sucesso(){
            ViewBag.Title = "Sucesso";
            ViewBag.Msg = "Registo efetuado com sucesso"; 
            return View(); 
        }

        public ViewResult Registar_TelefoneInvalido(){
            ViewBag.Title = "Telefone inválido";
            ViewBag.Msg = "O telefone inserido não é válido"; 
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
        public ViewResult Tracking_da_Encomenda(){
            ViewBag.Title="Tracking da Encomenda";
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
        
    }
}