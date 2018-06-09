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
		
		private int FornecedorID;
		private int ClienteID;
		private int FuncionarioID;
		private int CartaoCreditoID;

		private Cliente Cliente{get;set;}
		private Funcionario Funcionario{get;set;}
		private Fornecedor Fornecedor{get;set;}
		private CartaoCredito CartaoCredito;

		public void GerarFatura(Cliente cliente) {
			throw new System.Exception("Not implemented");
		}

		public void setClienteID(int cliente){
			this.ClienteID = cliente;
		}

		public void setFuncionarioID(int funcionario){
			this.FuncionarioID = funcionario;
		}

		public void setFornecedorID(int fornecedor){
			this.FornecedorID = fornecedor;
		}

		public int getFornecedorID(){
			return this.FornecedorID;
		}

		public int getClienteID(){
			return this.ClienteID;
		}

		public int getCartaoCreditoID(){
			return this.CartaoCreditoID;
		}

		public void setPrivate(int fornecedor, int cliente, int funcionario){
			this.FornecedorID = fornecedor;
            this.ClienteID = cliente;
            this.FuncionarioID = funcionario;
		}
		public int getFuncionarioID(){
			return this.FuncionarioID;
		}

		public void setAvaliacao(int classEstadoEncomenda){
			this.avaliação=classEstadoEncomenda;
		}
	}
}