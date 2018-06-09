using Microsoft.AspNetCore.Mvc;
using mvcJJMS.Data;
using mvcJJMS.Models;
using System.Collections.Generic;
using System.Linq;

namespace mvcJJMS.Controllers{
    public class MenuClienteController : Controller{
        private readonly JJMSContext _context;
        private readonly EncomendaController _eController;
        private readonly UtilizadorController _uController;
        private readonly ClienteController _cController;
        private readonly FuncionarioController _fController;

        public MenuClienteController(JJMSContext context, EncomendaController eController, UtilizadorController uController, ClienteController cController, FuncionarioController fController){
			_context=context;
            _eController=eController;
            _uController=uController;
            _cController=cController;
            _fController=fController;
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
            return View("~/Views/AvaliarServico/Index.cshtml");
        }

        public ViewResult AlterarDados(){
            ViewBag.Title = "Alterar Dados";
            int idCliente=_uController.getUtilizadorID();
            ViewBag.nome = this._uController.GetUserNome(idCliente);
            ViewBag.password = this._uController.GetUserPassword(idCliente);
            ViewBag.email = this._uController.GetUserEmail(idCliente);
            int idCliente2 = _context.Clientes.Where(e=>e.getUtilizadorID()==idCliente).FirstOrDefault().ClienteID;
            ViewBag.morada = this._cController.GetClienteMorada(idCliente2);
            ViewBag.telefone = this._cController.GetClienteTelefone(idCliente2);
            return View("~/Views/AlterarDados/Index.cshtml");
        }

        public ActionResult AlterarDadosAlterar(string nomeInput, string passwordInput, string emailInput, string moradaInput, string telefoneInput){
            int idCliente=_uController.getUtilizadorID();
            string nome = this._uController.GetUserNome(idCliente);
            string password = this._uController.GetUserPassword(idCliente);
            string email = this._uController.GetUserEmail(idCliente);
            int idCliente2 = _context.Clientes.Where(e=>e.getUtilizadorID()==idCliente).FirstOrDefault().ClienteID;
            string morada = this._cController.GetClienteMorada(idCliente2);
            string telefone = this._cController.GetClienteTelefone(idCliente2);
            
            bool emailAssoc = false;
            if(!emailInput.Equals(email)) emailAssoc = this._uController.emailAssociado(emailInput);
            if(emailAssoc) return EmailJaAssociado();

            bool telVal = true;
            if(!telefoneInput.Equals(telefone)) telVal = this._cController.telefoneValido(telefoneInput);
            if(!telVal) return TelefoneInvalido();

            bool passVal = true;
            if(!passwordInput.Equals(password)) passVal = this._cController.passwordSegura(passwordInput);
            if(!passVal) return PasswordInsegura();

            if(!nomeInput.Equals(nome)) this._uController.UpdateNome(idCliente,nomeInput); 
            if(!passwordInput.Equals(password)) this._uController.UpdatePassword(idCliente,passwordInput);
            if(!emailInput.Equals(email)) this._uController.UpdateEmail(idCliente,emailInput);
            if(!moradaInput.Equals(morada)) this._cController.UpdateMorada(idCliente2,moradaInput);
            if(!telefoneInput.Equals(telefone)) this._cController.UpdateTelefone(idCliente2,telefoneInput);

            return AlteradoComSucesso();           
        }

        public ViewResult AlteradoComSucesso(){
            ViewBag.Title = "Alterado com Sucesso";
            ViewBag.Msg = "Dados alterados com sucesso.";
            return View("~/Views/AlterarDados/AlteradoComSucesso.cshtml");
        }

        public ViewResult EmailJaAssociado(){
            ViewBag.Title = "Email já associado";
            ViewBag.Msg = "Email já associado a outro cliente.";
            return View("~/Views/AlterarDados/EmailJaAssociado.cshtml");
        }

        public ViewResult TelefoneInvalido(){
            ViewBag.Title = "Telefone Inválido";
            ViewBag.Msg = "Telefone inserido não é válido.";
            return View("~/Views/AlterarDados/TelefoneInvalido.cshtml");
        }

        public ViewResult PasswordInsegura(){
            ViewBag.Title = "Password Insegura";
            return View("~/Views/AlterarDados/PasswordInsegura.cshtml");
        }

        public ViewResult CancelarAvaliar(){
            ViewBag.Title = "Cancelar";
            ViewBag.Msg = "Operação cancelada"; 
            return View("~/Views/AvaliarServico/Cancelar.cshtml"); 
        }

        public ActionResult checkEncomenda(int idEncomenda){
            if(!this._eController.existeEncomenda(idEncomenda)) return CodigoInexistente();
            else if(this._eController.getEstadoEncomendaI(idEncomenda)!=4) return EncomendaPorEntregar();
            return InserirClassificacoes(idEncomenda);
        }

        public ViewResult CodigoInexistente(){
            ViewBag.Title = "Codigo Inexistente";
            ViewBag.Msg = "Não existe encomenda com o código inserido."; 
            return View("~/Views/AvaliarServico/CodigoInexistente.cshtml"); 
        }

        public ViewResult EncomendaPorEntregar(){
            ViewBag.Title = "Encomenda por entregar";
            ViewBag.Msg = "A encomenda ainda não foi entregue, sendo não pode ser avaliada."; 
            return View("~/Views/AvaliarServico/EncomendaPorEntregar.cshtml");
        }

        public ViewResult InserirClassificacoes(int idEncomenda){
            ViewBag.Title="Inserir Classificações";
            ViewBag.idEncomenda=idEncomenda.ToString();
            return View("~/Views/AvaliarServico/InserirClassificacoes.cshtml");
        }

        public ActionResult AvaliaS(string idEncomendaS, int classServicoEntrega,  int classEstadoEncomenda){
            if(!classificacoesValidas(classServicoEntrega, classEstadoEncomenda))
                return ClassificaoesInvalidas();
            
            // Remove trailling forward slash
            idEncomendaS=idEncomendaS.Remove(idEncomendaS.Length-1);
            int idEncomenda=int.Parse(idEncomendaS);
            avalia(idEncomenda,classServicoEntrega,classEstadoEncomenda);
            
            return Sucesso();
        }

        public bool classificacoesValidas(int classServicoEntrega,  int classEstadoEncomenda){
            return (classServicoEntrega >= 0 && classServicoEntrega <= 10 && classEstadoEncomenda >= 0 && classEstadoEncomenda <= 5);
        }

        public void avalia( int idEncomenda,  int classServicoEntrega,  int classEstadoEncomenda) {
            Encomenda enc=_eController.getEncomenda(idEncomenda);
            int idFun=enc.getFuncionarioID();
            Funcionario funcionario= _fController.getFuncionario(idFun);
            enc.setAvaliacao(classEstadoEncomenda);
            funcionario.AtualizaAvaliacao(classServicoEntrega);
		}

        public ViewResult ClassificaoesInvalidas(){
            ViewBag.Title = "Classificações Inválidas";
            ViewBag.Msg = "Foram inseridos valores incorretos, não respeitando a gama de valores estabelecida."; 
            return View("~/Views/AvaliarServico/ClassificacoesInvalidas.cshtml");
        }

        public ViewResult Sucesso(){
            ViewBag.Title = "Sucesso";
            ViewBag.Msg = "Avaliação efetuda com sucesso"; 
            return View("~/Views/AvaliarServico/Sucesso.cshtml");
        }

        public ViewResult ConsultarHistorico(){
            ViewBag.Title="Consultar Histórico";
            return View();
        }

        public ViewResult RequisitarEncomenda(){
            ViewBag.Title="Requisitar Encomenda";
            return View("~/Views/RequisitarEncomenda/Index.cshtml"); 
        }
    }
}
