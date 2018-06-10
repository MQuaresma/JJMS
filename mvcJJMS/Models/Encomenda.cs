using System.ComponentModel.DataAnnotations.Schema;
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
		
		
		[ForeignKey("Fornecedor")]
		public int FornecedorID;
		public Fornecedor Fornecedor{get;set;}

		
		[ForeignKey("Cliente")]
		public int ClienteID;
		public Cliente Cliente{get;set;}
		
		
		[ForeignKey("Funcionario")]
		public int FuncionarioID;
		public Funcionario Funcionario{get;set;}
		
		
		[ForeignKey("CartaoCredito")]
		public long CartaoCreditoID;		
		public CartaoCredito CartaoCredito{get;set;}

		
		public void gerarFatura(Cliente cliente) {
			throw new System.Exception("Not implemented");
		}

		public void setFuncionarioID(int funcionario){
			this.FuncionarioID = funcionario;
		}

		public int getFuncionarioID(){
			return this.FuncionarioID;
		}

		public void setFornecedorID(int fornecedor){
			this.FornecedorID = fornecedor;
		}

		public int getFornecedorID(){
			return this.FornecedorID;
		}

		public void setClienteID(int cliente){
			this.ClienteID = cliente;
		}

		public int getClienteID(){
			return this.ClienteID;
		}

		public void setCartaoCredito(CartaoCredito cc){
			this.CartaoCredito = cc;
		}

		public long getCartaoCreditoID(){
			return this.CartaoCreditoID;
		}

		public void setAvaliacao(int classEstadoEncomenda){
			this.avaliação=classEstadoEncomenda;
		}

		public void setPrivate(int fornecedor, int cliente, int funcionario){
			this.FornecedorID = fornecedor;
            this.ClienteID = cliente;
            this.FuncionarioID = funcionario;
		}
	}
}