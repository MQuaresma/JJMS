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
            _uController=uController;
            _eController=eController;
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
            ViewBag.Func = "Index";
            ViewBag.File = "MenuCliente";
            ViewBag.ButtonName = "OK";
            return View("~/Views/Shared/SimpleMsg.cshtml");
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
            ViewBag.password = System.Text.Encoding.UTF8.GetString(this._uController.GetUserPassword(idCliente));
            ViewBag.email = this._uController.GetUserEmail(idCliente);
            ViewBag.morada = this._cController.GetClienteMorada(idCliente);
            ViewBag.telefone = this._cController.GetClienteTelefone(idCliente);
            return View("~/Views/AlterarDados/Index.cshtml");
        }

        public ActionResult AlterarDadosAlterar(string nomeInput, string passwordInput, string emailInput, string moradaInput, string telefoneInput){
            int idCliente=_uController.getUtilizadorID();
            string nome = this._uController.GetUserNome(idCliente);
            byte[] password = this._uController.GetUserPassword(idCliente);
            string email = this._uController.GetUserEmail(idCliente);
            string morada = this._cController.GetClienteMorada(idCliente);
            string telefone = this._cController.GetClienteTelefone(idCliente);
            
            bool emailAssoc = false;
            if(!emailInput.Equals(email)) emailAssoc = this._uController.emailAssociado(emailInput);
            if(emailAssoc) return EmailJaAssociado();

            bool telVal = true;
            if(!telefoneInput.Equals(telefone)) telVal = this._cController.telefoneValido(telefoneInput);
            if(!telVal) return TelefoneInvalido();

            bool passVal = true;
            byte[] passwordInputHash = UtilizadorController.hashFunction(passwordInput);
            if(!passwordInputHash.SequenceEqual(password)) passVal = this._cController.passwordSegura(passwordInput);
            if(!passVal) return PasswordInsegura();

            if(!nomeInput.Equals(nome)) this._uController.UpdateNome(idCliente,nomeInput); 
            if(!passwordInputHash.SequenceEqual(password)) this._uController.UpdatePassword(idCliente,passwordInput);
            if(!emailInput.Equals(email)) this._uController.UpdateEmail(idCliente,emailInput);
            if(!moradaInput.Equals(morada)) this._cController.UpdateMorada(idCliente,moradaInput);
            if(!telefoneInput.Equals(telefone)) this._cController.UpdateTelefone(idCliente,telefoneInput);

            return AlteradoComSucesso();           
        }

        public ViewResult AlteradoComSucesso(){
            ViewBag.Title = "Alterado com Sucesso";
            ViewBag.Msg = "Dados alterados com sucesso.";
            ViewBag.Func = "Index";
            ViewBag.File = "MenuCliente";
            ViewBag.ButtonName = "OK";
            return View("~/Views/Shared/SimpleMsg.cshtml");
        }

        public ViewResult EmailJaAssociado(){
            ViewBag.Title = "Email já associado";
            ViewBag.Msg = "Email já associado a outro cliente.";
            ViewBag.Func = "Index";
            ViewBag.File = "MenuCliente";
            ViewBag.ButtonName = "OK";
            return View("~/Views/Shared/SimpleMsg.cshtml");
        }

        public ViewResult TelefoneInvalido(){
            ViewBag.Title = "Telefone Inválido";
            ViewBag.Msg = "Telefone inserido não é válido.";
            ViewBag.Func = "Index";
            ViewBag.File = "MenuCliente";
            ViewBag.ButtonName = "OK";
            return View("~/Views/Shared/SimpleMsg.cshtml");
        }

        public ViewResult PasswordInsegura(){
            ViewBag.Title = "Password Insegura";
            ViewBag.Msg = "A password não cumpre requisitos mínimos de segurança:"; 
			ViewBag.Item1 = "* 8 ou mais carateres";
			ViewBag.Item2 = "* possuir números, letras e símbolos";
			ViewBag.Func = "Index";
            ViewBag.File = "MenuCliente";
            ViewBag.ButtonName = "Ok";
            return View("~/Views/Shared/PasswordInsegura.cshtml");
        }

        public ViewResult CancelarAvaliar(){
            ViewBag.Title = "Cancelar";
            ViewBag.Msg = "Operação cancelada"; 
            ViewBag.Func = "Index";
            ViewBag.File = "MenuCliente";
            ViewBag.ButtonName = "OK";
            return View("~/Views/Shared/SimpleMsg.cshtml");
        }

        public ActionResult checkEncomenda(int idEncomenda){
            if(!this._eController.existeEncomenda(idEncomenda)) return CodigoInexistente();
            else if(this._eController.getEstadoEncomendaI(idEncomenda)!=4) return EncomendaPorEntregar();
            return InserirClassificacoes(idEncomenda);
        }

        public ViewResult CodigoInexistente(){
            ViewBag.Title = "Codigo Inexistente";
            ViewBag.Msg = "Não existe encomenda com o código inserido."; 
            ViewBag.Func = "Index";
            ViewBag.File = "MenuCliente";
            ViewBag.ButtonName = "Ok";
            return View("~/Views/Shared/SimpleMsg.cshtml");
        }

        public ViewResult EncomendaPorEntregar(){
            ViewBag.Title = "Encomenda por entregar";
            ViewBag.Msg = "A encomenda ainda não foi entregue, sendo assim, ainda não pode ser avaliada."; 
            ViewBag.Func = "Index";
            ViewBag.File = "MenuCliente";
            ViewBag.ButtonName = "OK";
            return View("~/Views/Shared/SimpleMsg.cshtml");
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
            ViewBag.Func = "Index";
            ViewBag.File = "MenuCliente";
            ViewBag.ButtonName = "OK";
            return View("~/Views/Shared/SimpleMsg.cshtml");
        }

        public ViewResult Sucesso(){
            ViewBag.Title = "Sucesso";
            ViewBag.Msg = "Avaliação efetuda com sucesso"; 
            ViewBag.Func = "Index";
            ViewBag.File = "MenuCliente";
            ViewBag.ButtonName = "OK";
            return View("~/Views/Shared/SimpleMsg.cshtml");
        }

        public ViewResult ConsultarHistorico(){
            ViewBag.Title="Consultar Histórico";
            return View();
        }

        public ViewResult RequisitarEncomenda(){
            int idU = _uController.getUtilizadorID();
            bool bloq = _cController.EstaBloqueado(idU);
            if (bloq) return ClienteBloqueado();
            else{
                ViewBag.Title="Requisitar Encomenda";
                return View("~/Views/RequisitarEncomenda/Index.cshtml"); 
            }
        }

        public ViewResult ClienteBloqueado(){
            ViewBag.Title="Cliente Bloqueado";
            ViewBag.Msg = "Está bloqueado por falta de pagamento de uma encomenda anteriormente realizada";
            ViewBag.Func = "Index";
            ViewBag.File = "MenuCliente";
            ViewBag.ButtonName = "OK";
            return View("~/Views/Shared/SimpleMsg.cshtml");
        }
    }
}
