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

		/// <summary>
		/// return Funcionario(emplyee) with id idFuncionario
		/// </summary>
		/// <param name="idFuncionario"></param>
		/// <returns>return Funcionario</returns>
		public Funcionario getFuncionario(int idFuncionario){
			return _context.Funcionarios.Find(idFuncionario);
		}

		/// <summary>
		/// choose an employee to deliver the order with id idEncomenda with destino(destiny) adress
		/// </summary>
		/// <param name="idEncomenda"></param>
		/// <param name="destino"></param>
		/// <returns>return the id of Funcionario(employee) choosen</returns>
        public int DelegarFuncionario( int idEncomenda,  string destino) {
			int idResp = -1;
			int nEncResp = 0;
			int nRespT=Int32.MaxValue;
			int dZona = GetZona(destino);
			List<Utilizador> utilizadores = _context.Utilizadores.ToList();

			foreach(Utilizador utilizador in utilizadores){
				if(utilizador is Funcionario){
					Funcionario func = (Funcionario)utilizador;
					int fZona = func.ZonaTrabalho;
					nEncResp = func.NroEnc;

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

		/// <summary>
		/// sends a email to the choosen employee that will deliver the order with id idEncomenda
		/// </summary>
		/// <param name="idFunc"></param>
		/// <param name="idEncomenda"></param>
        public void EnviarEmail( int idFunc, int idEncomenda) {
			string emailFunc = this._uController.GetUserEmail(idFunc);
			string st = this._eController.getEstadoEncomendaS(idEncomenda);
			string origem = this._eController.getLocalizacaoEncomenda(idEncomenda);
			int idForn = _context.Encomendas.Find(idEncomenda).getFornecedorID();
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

		/// <summary>
		/// choose the action zone for an employee(Not implemented yet)
		/// </summary>
		/// <param name="morada"></param>
		/// <returns>returns the code of the area</returns>
        public int GetZona( string morada) {
			return 1; //dummy (not implemented)
		}
    }
}
