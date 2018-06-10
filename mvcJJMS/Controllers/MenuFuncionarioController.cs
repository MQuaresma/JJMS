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

        /// <summary>
        /// Show the menu of Funcionario(employee)
        /// </summary>
        /// <returns>MenuFuncionario</returns>
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

        /// <summary>
        /// Calculates the route between origem(source) adress and destino(destiny) adress
        /// </summary>
        /// <param name="origem"></param>
        /// <param name="destino"></param>
        /// <returns>return route</returns>
        public ViewResult CalcularRota( string origem,  string destino) {
			throw new System.Exception("Not implemented");
		}

        /// <summary>
        /// Obtain route for an Encomenda(order) where Funcionario(employee) is at origem adress
        /// </summary>
        /// <param name="idEncomenda"></param>
        /// <param name="origem"></param>
        /// <returns>return route</returns>
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

        /// <summary>
        /// Allow Funcionario(employee) to consult a route for an Encomenda(order) with idEncS, verify if Encomenda(order) is a valid one and if is returns the route
        /// </summary>
        /// <param name="idEncS"></param>
        /// <param name="origem"></param>
        /// <returns>return route</returns>
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

        /// <summary>
        /// Checks if Encomenda(order) with idEncomenda is valid and return the correpondent view
        /// </summary>
        /// <param name="idEncomenda"></param>
        /// <returns>return the correspondent view</returns>
        public ViewResult AtualizarEstadoCusto(string idEncomenda){
            ViewBag.Title="Custo Transporte";
            ViewBag.idEncomenda = idEncomenda;

            bool existeEnc = this._eController.existeEncomenda(Convert.ToInt32(idEncomenda));
            bool encEnt = this._eController.EncomendaEntregue(Convert.ToInt32(idEncomenda));
            if(!existeEnc || encEnt) return NaoExisteEncomenda();

            return View("~/Views/AtualizarEstado/Custo.cshtml");
        }

        /// <summary>
        /// checks cost(custoInput) and updates state and total cost of order with id idEncomenda, 
        /// and depending the state delegate a employee or activate the payment of the service
        /// </summary>
        /// <param name="idEncomenda"></param>
        /// <param name="custoInput"></param>
        /// <returns>return the correspondent action</returns>
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

        /// <summary>
        /// checks if inserted cost is valid, if is not negative and not have letters neither symbols 
        /// </summary>
        /// <param name="custo"></param>
        /// <returns>true if valid, false if not</returns>
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

        /// <summary>
        /// Delegates an Funcionario(employee) for the Encomenda(order) with id idEncomenda
        /// </summary>
        /// <param name="idEncomenda"></param>
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
