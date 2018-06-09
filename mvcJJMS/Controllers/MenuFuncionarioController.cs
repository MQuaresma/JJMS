using Microsoft.AspNetCore.Mvc;
using mvcJJMS.Data;
using mvcJJMS.Models;
using System.Collections.Generic;
using System.Globalization;
using System;

namespace mvcJJMS.Controllers{
    public class MenuFuncionarioController : Controller{
        private readonly JJMSContext _context;
        private readonly EncomendaController _eController;
        private readonly FuncionarioController _fController;
        private readonly ClienteController _cController;
        public MenuFuncionarioController(JJMSContext context, EncomendaController eController, FuncionarioController fController, ClienteController cController){
			this._context=context;
            this._eController=eController;
            this._fController=fController;
            this._cController=cController;
		}

        public ViewResult Index(){
            ViewBag.Title = "Menu Funcionário";
            ViewBag.ListElem1 = "Consultar Rota";
            ViewBag.ListElem1View = "ConsultarRota";
            ViewBag.ListElem2 = "Atualizar Estado";
            ViewBag.ListElem2View = "AtualizarEstado";
            ViewBag.ListElem3 = "Logout";
            ViewBag.ListElem3View = "Index";
            ViewBag.MF = "MenuFuncionario";
            ViewBag.To="MenuPrincipal";
            return View(); 
        }

        public int CalcularRota( string origem,  string destino) {
			throw new System.Exception("Not implemented");
		}

        public ViewResult ConsultarRota(){
            ViewBag.Title="Consultar Rota";
            return View(); 
        }
        public ViewResult AtualizarEstado(){
            ViewBag.Title="Código da Encomenda";
            return View("~/Views/AtualizarEstado/Index.cshtml"); 
        }

        public ViewResult AtualizarEstadoCusto(string idEncomenda){
            ViewBag.Title="Custo Transporte";
            ViewBag.idEncomenda = idEncomenda;

            bool existeEnc = this._eController.existeEncomenda(Convert.ToInt32(idEncomenda));
            bool encEnt = this._eController.EncomendaEntregue(Convert.ToInt32(idEncomenda));
            if(!existeEnc || encEnt) return NaoExisteEncomenda();

            return View("~/Views/AtualizarEstado/Custo.cshtml");
        }

        public ActionResult AtualizarEstadoRes(string idEncomenda, string custoInput){
            
            bool custoVal = custoValido(custoInput);
            if(!custoVal) return CustoInvalido();

            int idEnc = Convert.ToInt32(idEncomenda);

            this._eController.UpdateCustoEnc(idEnc,float.Parse(custoInput, CultureInfo.InvariantCulture.NumberFormat));
            this._eController.UpdateEstadoEnc(idEnc);
            int estado = this._eController.getEstadoEncomendaI(idEnc);
            
            if(estado==2) this._fController.DelegarFuncionario(idEnc,this._eController.GetDestinoEnc(idEnc)); 
            else if(estado==4) this._cController.PagarServiço(_context.Encomendas.Find(idEnc).getClienteID(),idEnc);

            return AtualizadoComSucesso();
        }

        bool custoValido(string custo){
            int i = 0;
            bool ret;

            while(i<custo.Length && (Char.IsDigit(custo[i]) || custo[i]=='.')) i++;

            if(i==custo.Length) ret = true;
            else ret = false;
            return ret;
        }

        public ViewResult AtualizadoComSucesso(){
            ViewBag.Title="Atualizado com sucesso";
            ViewBag.Msg = "Estado atualizao com sucesso.";
            return View("~/Views/AtualizarEstado/AtualizadoComSucesso.cshtml");
        }

        public ViewResult CustoInvalido(){
            ViewBag.Title="Custo Inválido";
            ViewBag.Msg = "Custo inserido inválido, valor negativo ou possui letras ou símbolos.";
            return View("~/Views/AtualizarEstado/CustoInvalido.cshtml");
        }

        public ViewResult NaoExisteEncomenda(){
            ViewBag.Title="Não existe encomenda";
            ViewBag.msg = "Não existe encomenda com o código fornecido ou já foi entregue.";
            return View("~/Views/AtualizarEstado/NãoExisteEncomenda.cshtml");
        }
    }
}
