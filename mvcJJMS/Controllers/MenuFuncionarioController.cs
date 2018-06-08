using Microsoft.AspNetCore.Mvc;
using mvcJJMS.Data;
using mvcJJMS.Models;
using System.Collections.Generic;

namespace mvcJJMS.Controllers{
    public class MenuFuncionarioController : Controller{
        private readonly JJMSContext _context;
        public MenuFuncionarioController(JJMSContext context){
			_context=context;
		}

        public ViewResult Index(){
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

        public int CalcularRota( string origem,  string destino) {
			throw new System.Exception("Not implemented");
		}
    }
}
