using FILE = System.String;
using Date = System.String;
using Time = System.String;

namespace mvcJJMS.Models{
	public class Encomenda {
		public int EncomendaID{get;set;}
		public int estado{get;set;}
		public string destino{get;set;}
		public FILE fatura{get;set;}
		public int avaliação{get;set;}
		public float custo{get;set;}
		public Date dia{get;set;}
		public Time hora{get;set;}
		
		public int FornecedorID{get;set;}
		public int ClienteID{get;set;}
		public int FuncionarioID{get;set;}
		public int CartaoCreditoID{get;set;}

		public Cliente Cliente{get;set;}
		public Funcionario Funcionario{get;set;}
		public Fornecedor Fornecedor{get;set;}
		private CartaoCredito CartaoCredito;

		public void GerarFatura(Cliente cliente) {
			throw new System.Exception("Not implemented");
		}
	}
}