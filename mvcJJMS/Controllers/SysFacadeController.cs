using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System;
using mvcJJMS.Data;
using mvcJJMS.Models;
using FILE = System.String;
using Date = System.String;
using Time = System.String;

namespace mvcJJMS.Controllers{
	public class SysFacadeController : Controller {
		static private string moradaCD;
		static private int utilizadorID;
		static private JJMSContext _context;

		public SysFacadeController(JJMSContext context){
			SysFacadeController._context=context;
		}

        static public void iniciar(string mCD){
			SysFacadeController.moradaCD=mCD;
		}

		static public bool ExisteEncomenda(int idEncomenda) {
			return SysFacadeController._context.Encomendas.Find(idEncomenda)!=null;
		}

		static public bool EmailAssociado( string email) {
			return SysFacadeController._context
									  .Utilizadores
									  .Where(ut => ut.Email.Equals(email))
									  .FirstOrDefault() != default(Utilizador);
		}

		static public string GetUserNome( int idCliente) {
			Cliente cli=SysFacadeController._context.Clientes.Find(idCliente);
			if(cli!=null) return cli.Nome;
			else return null;
		}

		static public string GetUserPassword( int idCliente) {
			throw new System.Exception("Not implemented");
		}

		static public string GetUserEmail( int idCliente) {
			throw new System.Exception("Not implemented");
		}

		static public string GetClienteMorada( int idCliente) {
			throw new System.Exception("Not implemented");
		}

		static public string GetClienteTelefone( int idCliente) {
			throw new System.Exception("Not implemented");
		}

		static public void UpdateNome( int idCliente,  string nomeInput) {
			throw new System.Exception("Not implemented");
		}

		static public void UpdatePassword( int idCliente,  string passwordInput) {
			throw new System.Exception("Not implemented");
		}

		static public void UpdateEmail( int idCliente,  string emailInput) {
			throw new System.Exception("Not implemented");
		}

		static public void UpdateMorada( int idCliente,  string moradaInput) {
			throw new System.Exception("Not implemented");
		}

		static public void UpdateTelefone( int idCliente,  string telefoneInput) {
			throw new System.Exception("Not implemented");
		}

		static public void UpdateCustoEnc( int idEncomenda,  float custoInput) {
			throw new System.Exception("Not implemented");
		}

		static public void UpdateEstadoEnc( int idEncomenda) {
			throw new System.Exception("Not implemented");
		}

		static public void Bloquear( int idCliente) {
			throw new System.Exception("Not implemented");
		}

		static public void TransfereMontante( int idCliente,  int idEncomenda) {
			throw new System.Exception("Not implemented");
		}

		static public void GerarFatura( int idCliente,  int idEncomenda) {
			throw new System.Exception("Not implemented");
		}

		static public int GetFuncionarioResp( int idEncomenda) {
			throw new System.Exception("Not implemented");
		}

		static public int Login(string email, string password) {
			List<Utilizador> uts = SysFacadeController._context.Utilizadores.ToList();
			bool found = false;
			int ret=2;
			string pass = hashFunction(password).ToString();

			for(int i=0; i< uts.Count && !found; i++){
				string e = uts[i].Email;
				if (email.Equals(e)){
					found = true;
					ret=3;
					string p = uts[i].Password.ToString();
					if (pass.Equals(p)){
						if (uts[i] is Cliente) ret=0;
						else ret = 1;
						SysFacadeController.utilizadorID = uts[i].UtilizadorID;
					}
				}
			}
			return ret;
		}

		static public int IdForn( string nomeForn) {
			throw new System.Exception("Not implemented");
		}

		static public bool CartaoValido( int numCartCredito,  int mes,  int ano,  int cvv,  string pais) {
			throw new System.Exception("Not implemented");
		}

		static public int DelegarFuncionario( int idEncomenda,  string destino) {
			throw new System.Exception("Not implemented");
		}

		static public void SetEncomenda( int idCliente,  string nomeForn,  string morada,  Date dia,  Time hora,  int numCartCredito,  int mes,  int ano,  int cvv,  string pais) {
			throw new System.Exception("Not implemented");
		}

		static public int CalcularRota( string origem,  string destino) {
			throw new System.Exception("Not implemented");
		}

		static public string GetDestinoEnc( int idEncomenda) {
			throw new System.Exception("Not implemented");
		}

		static public string GetMoradaForn(int idForn) {
			Fornecedor forn = SysFacadeController._context.Fornecedores.Find(idForn);
			return forn.morada;
		}

		static public int GetEstadoEnc( int idEncomenda) {
			throw new System.Exception("Not implemented");
		}

		static public void Registar(string nome, string password, string email,string morada, string telefone) {
			Cliente nCliente = _context.newCliente(nome,SysFacadeController.hashFunction(password),email,morada,telefone);
			_context.Clientes.Add(nCliente);
            _context.SaveChanges();
		}
		
		static public string GetLocalizacaoEncomenda( int idEncomenda) {
			Encomenda encomenda = SysFacadeController._context.Encomendas.Find(idEncomenda);
			int estado = encomenda.estado;
			switch (estado){
				case 1:
					int forn = encomenda.GetIdFornecedor();
					return GetMoradaForn(forn);
				case 2:
					return GetMoradaCD();
				case 3:
					return "Não especificado";
				default:
					return encomenda.destino;
			}
		}

		static public string GetEstadoEncomenda( int idEncomenda) {
			Encomenda enc = SysFacadeController._context.Encomendas.Find(idEncomenda);
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

		static public FILE GetFatura( int idCliente,  int idEncomenda) {
			throw new System.Exception("Not implemented");
		}

		static public List<Encomenda> GetHistoricoEnc( int idCliente) {
			throw new System.Exception("Not implemented");
		}

		static public bool EncomendaEntregue( int idEncomenda) {
			throw new System.Exception("Not implemented");
		}

		static public void Avalia( int idEncomenda,  int classServicoEntrega,  int classEstadoEncomenda) {
			throw new System.Exception("Not implemented");
		}

		static public bool ExisteEncomendaCliente( int idCliente,  int idEncomenda) {
			throw new System.Exception("Not implemented");
		}

		static public string GetMoradaCD() {
			return SysFacadeController.moradaCD;
		}

		static public bool Luhn_check( int num_cart_cred) {
			throw new System.Exception("Not implemented");
		}

		static public void EnviarEmail( int idFunc,  int idEncomenda) {
			throw new System.Exception("Not implemented");
		}

		static public int GetZona( string morada) {
			throw new System.Exception("Not implemented");
		}

		static public bool EstaBloqueado( int idCliente) {
			throw new System.Exception("Not implemented");
		}

		static public byte[] hashFunction(string input){
			var sha384 = new SHA384CryptoServiceProvider();
			return sha384.ComputeHash(Encoding.UTF8.GetBytes(input));
		}
	}
}