using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Mail;
using mvcJJMS.Data;
using mvcJJMS.Models;

namespace mvcJJMS.Controllers{
    public class FuncionarioController : Controller{
        private readonly JJMSContext _context;
		private readonly UtilizadorController _uController;
		private readonly EncomendaController _eController;
		private readonly FornecedorController _fController;

		public FuncionarioController(JJMSContext context, UtilizadorController uController, EncomendaController eController, FornecedorController fController){
			_context=context;
			_uController=uController;
			_eController=eController;
			_fController=fController;
		}
        
		public Funcionario getFuncionario(int idFuncionario){
			return _context.Funcionarios.Find(idFuncionario);
		}

        public int DelegarFuncionario( int idEncomenda,  string destino) {
			throw new System.Exception("Not implemented");
		}

        public void EnviarEmail( int idFunc, int idEncomenda) {
			string emailFunc = this._uController.GetUserEmail(idFunc);
			string st = this._eController.getEstadoEncomendaS(idEncomenda);
			string origem = this._eController.getLocalizacaoEncomenda(idEncomenda);
			int idForn = _context.Encomendas.Find(idEncomenda).GetIdFornecedor(); 
			string destino = null;

			if(st.Equals("com o fornecedor")) destino=this._fController.GetMoradaForn(idForn);
			else if(st.Equals("no centro de distribuição")) destino=this._eController.GetDestinoEnc(idEncomenda);

			MailMessage mail = new MailMessage("noreply@jjms.com",emailFunc);
			mail.Subject="Nova Encomenda"+idEncomenda;
			mail.Body="Id Encomenda:"+idEncomenda+"\nOrigem:"+origem+"\nDestino:"+destino;

			SmtpClient client = new SmtpClient();
			client.Port=25;
			client.Host="smtp.gmail.com";
			client.UseDefaultCredentials=false;
			client.Send(mail);
		}

        public int GetZona( string morada) {
			throw new System.Exception("Not implemented");
		}
    }
}
