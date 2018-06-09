using Microsoft.AspNetCore.Mvc;
using mvcJJMS.Data;
using mvcJJMS.Models;
using System.Collections.Generic;

namespace mvcJJMS.Controllers{
    public class MenuClienteController : Controller{
        private readonly JJMSContext _context;
        private readonly EncomendaController _eController;

        public MenuClienteController(JJMSContext context, EncomendaController eController){
			_context=context;
            _eController=eController;
		}

        public ViewResult Index(){
            ViewBag.Title="Menu Cliente";
            ViewBag.ListElem1="Requisitar Encomenda";
            ViewBag.ListElem2="Consultar Histórico";
            ViewBag.ListElem2To="SysFacade";
            ViewBag.ListElem3="Tracking da Encomenda";
            ViewBag.ListElem4="Avaliar Serviço";
            ViewBag.ListElem5="Alterar Dados";
            ViewBag.ListElem6="Logout";
            ViewBag.To="MenuCliente";
            return View("~/Views/MenuCliente/Index.cshtml"); 
        }

        public ViewResult TrackingEncomenda(){
            ViewBag.Title="Tracking Encomenda";
            ViewBag.Msg="Insira o código da sua encomenda:";
            return View("~/Views/TrackingEncomenda/Index.cshtml");
        }

        public ViewResult CancelarTracking(){
            ViewBag.Title = "Cancelar";
            ViewBag.Msg = "Operação cancelada"; 
            return View("~/Views/TrackingEncomenda/Cancelar.cshtml"); 
        }

        /// <summary>
		/// Performs a check on the validity of the provided order id
		/// </summary>
        public ViewResult AvaliarServico(){
            ViewBag.Title="Avaliar Serviço";
            return View("~/Views/Avaliar_Servico/Index.cshtml");
        }

        public ViewResult CancelarAvaliar(){
            ViewBag.Title = "Cancelar";
            ViewBag.Msg = "Operação cancelada"; 
            return View("~/Views/Avaliar_Servico/Cancelar.cshtml"); 
        }

        public ActionResult checkEncomenda(int idEncomenda){
            if(!this._eController.existeEncomenda(idEncomenda)) return CodigoInexistente();
            else if(this._eController.getEstaoEncomendaI(idEncomenda)!=4) return EncomendaPorEntregar();
            return InserirClassificacoes(idEncomenda);

        }

        public ViewResult CodigoInexistente(){
            ViewBag.Title = "Codigo Inexistente";
            ViewBag.Msg = "Não existe encomenda com o código inserido."; 
            return View("~/Views/Avaliar_Servico/CodigoInexistente.cshtml"); 
        }

        public ViewResult EncomendaPorEntregar(){
            ViewBag.Title = "Encomenda por entregar";
            ViewBag.Msg = "A encomenda ainda não foi entregue, sendo não pode ser avaliada."; 
            return View("~/Views/Avaliar_Servico/EncomendaPorEntregar.cshtml");
        }

        public ViewResult InserirClassificacoes(int idEncomenda){
            ViewBag.Title="Inserir Classificações";
            ViewBag.idEncomenda=idEncomenda.ToString();
            return View("~/Views/Avaliar_Servico/InserirClassificacoes.cshtml");
        }

        public ActionResult AvaliaS(string idEncomendaS, int classServicoEntrega,  int classEstadoEncomenda){
            if(!classificacoesValias(classServicoEntrega, classEstadoEncomenda))
                return ClassificaoesInvalidas();
            
            // Remove trailling forward slash
            idEncomendaS=idEncomendaS.Remove(idEncomendaS.Length-1);
            int idEncomenda=int.Parse(idEncomendaS);
            avalia(idEncomenda,classServicoEntrega,classEstadoEncomenda);
            
            return Sucesso();
        }

        public bool classificacoesValias(int classServicoEntrega,  int classEstadoEncomenda){
            return (classServicoEntrega >= 0 && classServicoEntrega <= 10 && classEstadoEncomenda >= 0 && classEstadoEncomenda <= 5);
        }

        public void avalia( int idEncomendaS,  int classServicoEntrega,  int classEstadoEncomenda) {
            //TODO: implement code to save new rating
		}

        public ViewResult ClassificaoesInvalidas(){
            ViewBag.Title = "Classificações Inválidas";
            ViewBag.Msg = "Foram inseridos valores incorretos, não respeitando a gama de valores estabelecida."; 
            return View("~/Views/Avaliar_Servico/ClassificacoesInvalidas.cshtml");
        }

        public ViewResult Sucesso(){
            ViewBag.Title = "Sucesso";
            ViewBag.Msg = "Avaliação efetuda com sucesso"; 
            return View("~/Views/Avaliar_Servico/Sucesso.cshtml");
        }

    }
}
