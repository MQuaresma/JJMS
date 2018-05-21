using Microsoft.AspNetCore.Mvc;

namespace mvcJJMS.Controllers
{
    public class TrackingEncomendaController : Controller
    {
        public ActionResult TrackingEncomenda(int idEncomenda) {
			if (SysFacadeController.ExisteEncomenda(idEncomenda) == false) 
				return RedirectToAction("CodigoInexistente", "TrackingEncomenda");
			string localizacao = SysFacadeController.GetLocalizacaoEncomenda(idEncomenda);
			string estado = SysFacadeController.GetEstadoEncomenda(idEncomenda);
			return RedirectToAction("InformacaoEncomenda", "TrackingEncomenda", new {encomenda = idEncomenda, localizacao = localizacao, estado = estado});
		}
        
        public ViewResult Index(){
            ViewBag.Title="Tracking Encomenda";
            ViewBag.Msg="Insira o código da sua encomenda:";
            return View(); 
        }
        public ViewResult Cancelar(){
            ViewBag.Title = "Cancelar";
            ViewBag.Msg = "Operação cancelada"; 
            return View(); 
        }

        public ViewResult CodigoInexistente(){
            ViewBag.Title = "Código Inexistente";
            ViewBag.Msg = "Não existe a encomenda com o código inserido"; 
            return View(); 
        }

        public ViewResult InformacaoEncomenda(int encomenda, string localizacao, string estado){
            ViewBag.Title = "Email em uso";
            ViewBag.Msg = "Encomenda " + encomenda;
            ViewBag.Item1 = "Localização : " + localizacao;
            ViewBag.Item2 = "Estado : " + estado;
            return View(); 
        }
    }
}