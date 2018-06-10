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
		private readonly UtilizadorController _uController;
		private readonly CartaoController _caController;

		public EncomendaController(JJMSContext context, FornecedorController fController, UtilizadorController uController, CartaoController caController){
			_context=context;
			_fController=fController;
			_uController=uController;
			_caController=caController;
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
					int forn = encomenda.getFornecedorID();
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
			Encomenda enc = _context.Encomendas.Find(idEncomenda);
			enc.custo += custoInput;
			_context.SaveChanges();
		}

		public void UpdateEstadoEnc( int idEncomenda) {
			Encomenda enc = _context.Encomendas.Find(idEncomenda);
			if(enc.estado>0 && enc.estado<4) enc.estado++;
			_context.SaveChanges();
		}

        public int GetFuncionarioResp( int idEncomenda) {
			Encomenda enc = _context.Encomendas.Find(idEncomenda);
			return enc.getFuncionarioID();
		}
        public void SetEncomenda(int idCliente,string fornecedor,string morada,Date dia,Time hora,long numCartCredito,int mes,int ano,int cvv,string pais) {
			Encomenda nEncomenda = _context.newEncomenda(1,morada,dia,hora,_fController.IdForn(fornecedor),idCliente,1,numCartCredito);
			CartaoCredito nCartaoCredito = _context.newCartaoCredito(numCartCredito,mes,ano,cvv,pais);
			_context.Cartoes.Add(nCartaoCredito);
			_context.Encomendas.Add(nEncomenda);
			_context.SaveChanges();
		}

        public string GetDestinoEnc( int idEncomenda) {
			Encomenda enc = _context.Encomendas.Find(idEncomenda);
			return enc.destino;
		}

        public bool EncomendaEntregue( int idEncomenda) {
			Encomenda enc = _context.Encomendas.Find(idEncomenda);
			bool ret = false;
			int estado = enc.estado;

			if(estado==4) ret=true;
			return ret; 
		}

		public Encomenda getEncomenda(int idEncomenda){
			return _context.Encomendas.Find(idEncomenda);
		}

		public int getIdForn(int idEncomenda){
			Encomenda enc = _context.Encomendas.Find(idEncomenda);
			return enc.getFornecedorID();
		}

		public ViewResult CodigoInexistente(){
			ViewBag.Title = "Código Inexistente";
			ViewBag.Msg = "Não existe a encomenda com o código inserido"; 
			ViewBag.Func="Index";
			ViewBag.File="MenuCliente";
			ViewBag.ButtonName="OK";
			return View("~/Views/Shared/SimpleMsg.cshtml"); 
		}

		public ViewResult InformacaoEncomenda(int encomenda, string localizacao, string estado){
			ViewBag.Title = "Informação da Encomenda";
			ViewBag.Msg = "Encomenda " + encomenda;
			ViewBag.Item1 = "Localização : " + localizacao;
			ViewBag.Item2 = "Estado : " + estado;
			return View("~/Views/TrackingEncomenda/InformacaoEncomenda.cshtml"); 
		}

		public ActionResult RequisitarEncomenda(string fornecedor, string morada, Date dia, Time hora){
			int idForn = _fController.IdForn(fornecedor);
			if (idForn == -1) return FornecedorInvalido();
			return InserirDadosPagamento(fornecedor,morada,dia,hora);
		}

		public ViewResult InserirDadosPagamento(string fornecedor, string morada, Date dia, Time hora){
			ViewBag.Title = "Dados de Pagamento";
			ViewBag.fornecedor = fornecedor;
			ViewBag.morada = morada;
			ViewBag.dia = dia;
			ViewBag.hora = hora;
			return View("~/Views/RequisitarEncomenda/InserirDadosPagamento.cshtml"); 
		}

		public ViewResult ProcDadosPagamento(string fornecedor,string morada,Date dia,Time hora,string nccS,int mes,int ano,int cvv,string pais){
			Int64 ncc=Int64.Parse(nccS);
			bool cartao = _caController.CartaoValido(ncc,mes,ano,cvv,pais);
			if (cartao == false) return DadosPagamentoInvalidos();
			this.SetEncomenda(_uController.getUtilizadorID(),fornecedor,morada,dia,hora,ncc,mes,ano,cvv,pais);
			return Sucesso();
		}

		public ViewResult DadosPagamentoInvalidos(){
			ViewBag.Title = "Dados de Pagamento Inválidos";
			ViewBag.Msg = "Os dados de pagamento inseridos são inválidos";
			ViewBag.Func = "Index";
            ViewBag.File = "MenuCliente";
            ViewBag.ButtonName = "Cancelar";
            return View("~/Views/Shared/SimpleMsg.cshtml");
		}


		public ViewResult FornecedorInvalido(){
			ViewBag.Title = "Fornecedor Inválido";
			ViewBag.Msg = "O fornecedor inserido não está associado à JJMS."; 
			ViewBag.Func = "Index";
            ViewBag.File = "MenuCliente";
            ViewBag.ButtonName = "Cancelar";
            return View("~/Views/Shared/SimpleMsg.cshtml");
		}

		public ViewResult Sucesso(){
			ViewBag.Title = "Sucesso";
			ViewBag.Msg = "A encomenda foi requisitada com sucesso!"; 
			ViewBag.Func = "Index";
            ViewBag.File = "MenuCliente";
            ViewBag.ButtonName = "Ok";
            return View("~/Views/Shared/SimpleMsg.cshtml");
		}
    }
}