using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Mail;
using mvcJJMS.Data;
using mvcJJMS.Models;

using Date = System.String;
using Time = System.String;

namespace mvcJJMS.Controllers{
    public class EncomendaController : Controller{
        private readonly JJMSContext _context;
        private readonly String moradaCD="Avenida da Liberdade nº36, Braga";
		private readonly FornecedorController _fController;

		public EncomendaController(JJMSContext context, FornecedorController fController){
			_context=context;
			_fController=fController;
		}

        public ActionResult TrackingEncomenda(int idEncomenda) {
            if (existeEncomenda(idEncomenda) == false) 
                return CodigoInexistente();
            string localizacao = getLocalizacaoEncomenda(idEncomenda);
            string estado = getEstadoEncomendaS(idEncomenda);
            return InformacaoEncomenda(idEncomenda, localizacao, estado);
        }

		public bool existeEncomenda(int idEncomenda) {
			return _context.Encomendas.Find(idEncomenda)!=null;
        }

        public string getLocalizacaoEncomenda( int idEncomenda) {
			Encomenda encomenda = _context.Encomendas.Find(idEncomenda);
			int estado = encomenda.estado;
			switch (estado){
				case 1:
					int forn = encomenda.getIdFornecedor();
					return _fController.GetMoradaForn(forn);
				case 2:
					return this.moradaCD;
				case 3:
					return "Não especificado";
				default:
					return encomenda.destino;
			}
		}
		
		public int getEstadoEncomendaI( int idEncomenda) {
			Encomenda enc = _context.Encomendas.Find(idEncomenda);
			return  enc.estado;
		}

		/// <summary>
		/// Return the current status of the order in a String format
		/// </summary>
		public string getEstadoEncomendaS( int idEncomenda) {
			Encomenda enc = _context.Encomendas.Find(idEncomenda);
			int estado = enc.estado;
			switch (estado){
				case 1:
					return "com o fornecedor";
				case 2:
					return "no centro de distribuição";
				case 3:
					return "em trânsito";
				default:
					return "entregue";
			}
		}

        public void UpdateCustoEnc( int idEncomenda,  float custoInput) {
			throw new System.Exception("Not implemented");
		}

		public void UpdateEstadoEnc( int idEncomenda) {
			throw new System.Exception("Not implemented");
		}

        public int GetFuncionarioResp( int idEncomenda) {
			throw new System.Exception("Not implemented");
		}
        public void SetEncomenda(int idCliente,string nomeForn,string morada,Date dia,Time hora,int numCartCredito,int mes,int ano,int cvv,string pais) {
			throw new System.Exception("Not implemented");
		}

        public string GetDestinoEnc( int idEncomenda) {
			throw new System.Exception("Not implemented");
		}

        public int GetEstadoEnc( int idEncomenda) {
			throw new System.Exception("Not implemented");
		}

        public bool EncomendaEntregue( int idEncomenda) {
			throw new System.Exception("Not implemented");
		}

		public Encomenda getEncomenda(int idEncomenda){
			return _context.Encomendas.Find(idEncomenda);
		}

		public ViewResult CodigoInexistente(){
			ViewBag.Title = "Código Inexistente";
			ViewBag.Msg = "Não existe a encomenda com o código inserido"; 
			return View("~/Views/TrackingEncomenda/CodigoInexistente.cshtml"); 
		}

		public ViewResult InformacaoEncomenda(int encomenda, string localizacao, string estado){
			ViewBag.Title = "Informação da Encomenda";
			ViewBag.Msg = "Encomenda " + encomenda;
			ViewBag.Item1 = "Localização : " + localizacao;
			ViewBag.Item2 = "Estado : " + estado;
			return View("~/Views/TrackingEncomenda/InformacaoEncomenda.cshtml"); 
		}
    }
}