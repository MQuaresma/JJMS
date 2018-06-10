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
        private readonly FornecedorController _fornController;
        private readonly UtilizadorController _uController;
        public MenuFuncionarioController(JJMSContext context, EncomendaController eController, FuncionarioController fController, ClienteController cController, FornecedorController fornController, UtilizadorController uController){
			this._context=context;
            this._eController=eController;
            this._fController=fController;
            this._cController=cController;
            this._fornController=fornController;
            this._uController=uController;
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

        public ViewResult CalcularRota( string origem,  string destino) {
			throw new System.Exception("Not implemented");
		}

        public ViewResult CalcularRota(int idEncomenda, string origem){
            int stEnc = this._eController.getEstadoEncomendaI(idEncomenda);
            string dest=null;

            if(stEnc==1){
                int idForn = this._eController.getIdForn(idEncomenda);
                dest = this._fornController.GetMoradaForn(idForn);
            }else if(stEnc==2) dest = this._eController.getMoradaCD();
            else if(stEnc==3) dest = this._eController.GetDestinoEnc(idEncomenda);
            
            return CalcularRota(origem,dest);
        }

        public ViewResult ConsultarRota(){
            ViewBag.Title="Consultar Rota";
            return View("~/View/ConsultarRota/Index.cshtml"); 
        }

        public ActionResult ConsultarRotaRes(string idEncS, string origem){
            int idEnc = Convert.ToInt32(idEncS);
            bool existeEnc = this._eController.existeEncomenda(idEnc);
            bool ent = this._eController.EncomendaEntregue(idEnc);
            
            if(!existeEnc || ent) return EncomendaInexistente();

            int idF = this._eController.GetFuncionarioResp(idEnc);
            if(idF==-1 || this._uController.getUtilizadorID()!=idF) return EncomendaInvalida();

            return CalcularRota(idEnc,origem);
        }

        public ViewResult EncomendaInexistente(){
            ViewBag.Title="Encomenda Inexistente";
            ViewBag.Msg = "Encomenda não existe.";
            ViewBag.Func = "Index";
            ViewBag.File = "MenuFuncionario";
            ViewBag.ButtonName = "OK";
            return View("~/Views/Shared/SimpleMsg.cshtml");
        }

        public ViewResult EncomendaInvalida(){
            ViewBag.Title="Encomenda Inválida";
            ViewBag.Msg = "Encomenda delegada a outro funcionário.";
            ViewBag.Func = "Index";
            ViewBag.File = "MenuFuncionario";
            ViewBag.ButtonName = "OK";
            return View("~/Views/Shared/SimpleMsg.cshtml");
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
            
            if(estado==2) DelegarFuncionario(idEnc); 
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
            ViewBag.Func = "Index";
            ViewBag.File = "MenuFuncionario";
            ViewBag.ButtonName = "OK";
            return View("~/Views/Shared/SimpleMsg.cshtml");
        }

        public ViewResult CustoInvalido(){
            ViewBag.Title="Custo Inválido";
            ViewBag.Msg = "Custo inserido inválido, valor negativo ou possui letras ou símbolos.";
            ViewBag.Func = "Index";
            ViewBag.File = "MenuFuncionario";
            ViewBag.ButtonName = "OK";
            return View("~/Views/Shared/SimpleMsg.cshtml");
        }

        public ViewResult NaoExisteEncomenda(){
            ViewBag.Title="Não existe encomenda";
            ViewBag.msg = "Não existe encomenda com o código fornecido ou já foi entregue.";
            ViewBag.Func = "Index";
            ViewBag.File = "MenuFuncionario";
            ViewBag.ButtonName = "OK";
            return View("~/Views/Shared/SimpleMsg.cshtml");
        }

        public void DelegarFuncionario(int idEncomenda){
            int estado = this._eController.getEstadoEncomendaI(idEncomenda);
            int idForn = this._eController.getIdForn(idEncomenda);
            string destino = null;

            if(estado==1) destino = this._fornController.GetMoradaForn(idForn);
            else if(estado==2) destino = this._eController.GetDestinoEnc(idEncomenda);

            int idFunc = this._fController.DelegarFuncionario(idEncomenda,destino);
            this._fController.EnviarEmail(idFunc,idEncomenda);
        }
    }
}
