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
		
		private int FornecedorID{get;set;}
		private int ClienteID{get;set;}
		private int FuncionarioID{get;set;}
		private int CartaoCreditoID{get;set;}

		private Cliente Cliente{get;set;}
		private Funcionario Funcionario{get;set;}
		private Fornecedor Fornecedor{get;set;}
		private CartaoCredito CartaoCredito;

		public void GerarFatura(Cliente cliente) {
			throw new System.Exception("Not implemented");
		}
	}
}