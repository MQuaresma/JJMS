using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using mvcJJMS.Data;
using mvcJJMS.Models;
using FILE = System.String;
using Date = System.String;
using Time = System.String;

namespace mvcJJMS.Controllers{
	public class SysFacadeController {
		static private string moradaCD;
		static private int numFornecedores;
		static private int numUtilizadores;
		static private int numEncomendas;
		static private JJMSContext _context;

		public SysFacadeController(JJMSContext context){
			SysFacadeController._context=context;
		}

        static public void iniciar(string mCD){
			SysFacadeController.moradaCD=mCD;
			//TODO: buscar os restos dos valores à BD
			utilizadores = new Dictionary<int,Utilizador>();
		}

		static public bool ExisteEncomenda( int idEncomenda) {
			throw new System.Exception("Not implemented");
		}

		static public bool EmailAssociado( string email) {
			throw new System.Exception("Not implemented");
		}

		static public string GetUserNome( int idCliente) {
			throw new System.Exception("Not implemented");
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

		static public int Login(string email, string pass) {
			List<Utilizador> uts = _context.Utilizadores.ToList();
			List<Utilizador>.Enumerator it = uts.GetEnumerator();
			bool found = false;
			int ret = 1;
			string password = hashFunction(pass);

			while(it.MoveNext() && !found){
				Utilizador u = it.Current;
				string e = u.Email;
				
				if(email.Equals(e)){
					found = true;
					ret = 2;
					string p = u.Password;
					
					if(password.Equals(p)) ret = 0;
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

		static public string GetMoradaForn( int idEncomenda) {
			throw new System.Exception("Not implemented");
		}

		static public int GetEstadoEnc( int idEncomenda) {
			throw new System.Exception("Not implemented");
		}

        public ActionResult RealizarRegisto(string user,string password, string email, string morada, string telefone){
            Utilizador nUser=_context.newUtilizador(user, password, email);
            _context.Utilizadores.Add(nUser);
            _context.SaveChanges();
            return RedirectToAction("Index", "MenuPrincipal");
        }
		
		static public int Registar( string nome,  string password,  string email,  string morada,  string telefone) {
			
			
			return 1;
		}
		
		static public string GetLocalizacaoEncomenda( int idEncomenda) {
			throw new System.Exception("Not implemented");
		}

		static public string GetEstadoEncomenda( int idEncomenda) {
			throw new System.Exception("Not implemented");
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

		static private string hashFunction(string input){
			var sha384 = new SHA384CryptoServiceProvider();
			return sha384.ComputeHash(Encoding.UTF8.GetBytes(input)).ToString();
		}
	}
}