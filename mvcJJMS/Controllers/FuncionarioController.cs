using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Mail;
using mvcJJMS.Data;
using mvcJJMS.Models;
using System.Collections.Generic;
using System.Linq;

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
			int idResp = -1;
			int nEncResp = 0;
			int nRespT=Int32.MaxValue;
			int dZona = GetZona(destino);
			List<Utilizador> utilizadores = _context.Utilizadores.ToList();

			foreach(Utilizador utilizador in utilizadores){
				if(utilizador is Funcionario){
					Funcionario func = (Funcionario)utilizador;
					int fZona = func.zonaTrabalho;
					nEncResp = func.nroEnc;

					if(idResp==-1 || fZona==dZona && nRespT>nEncResp){
						idResp = func.UtilizadorID;
						nRespT = nEncResp;
					}
				}
			}

			Encomenda enc = _context.Encomendas.Find(idEncomenda);
			enc.setFuncionarioID(idResp);
			return idResp;
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
