using FILE = System.String;
using Date = System.String;
using Time = System.String;

namespace mvcJJMS.Models{
	public class Encomenda {
		public int EncomendaID{get;set;}
		private int estado{get;set;}
		private string destino{get;set;}
		private FILE fatura{get;set;}
		private int avaliação{get;set;}
		private float custo{get;set;}
		private Date dia{get;set;}
		private Time hora{get;set;}
		
		private int FornecedorID{get;set;}
		private int ClienteID{get;set;}
		private int FuncionarioID{get;set;}
		private int CartaoCreditoID{get;set;}

		public Cliente Cliente{get;set;}
		public Funcionario Funcionario{get;set;}
		public Fornecedor Fornecedor{get;set;}
		private CartaoCredito CartaoCredito;

		public void GerarFatura(Cliente cliente) {
			throw new System.Exception("Not implemented");
		}
	}
}