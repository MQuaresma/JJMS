using System.ComponentModel.DataAnnotations.Schema;
using FILE = System.String;
using Date = System.String;
using Time = System.String;

namespace mvcJJMS.Models{
	/// <summary>
	/// Represents a single order associated with a client and provider
	/// </summary>
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

		/// <summary>
		/// Generates order details for certain client
		/// </summary>
		/// <param name="cliente">Unique identifier for a client</param>
		public void gerarFatura(Cliente cliente) {
			throw new System.Exception("Not implemented");
		}

		/// <summary>
		/// Set employee associated with the order
		/// </summary>
		/// <param name="funcionario">Unique identifier for a employee</param>
		public void setFuncionarioID(int funcionario){
			this.FuncionarioID = funcionario;
		}

		/// <summary>
		/// Retrieves employee associated with the order
		/// </summary>
		/// <returns>Employee unique identifier</returns>
		public int getFuncionarioID(){
			return this.FuncionarioID;
		}

		/// <summary>
		/// Set provider associated with the order
		/// </summary>
		/// <param name="fornecedor">Unique identifier for a provider</param>
		public void setFornecedorID(int fornecedor){
			this.FornecedorID = fornecedor;
		}

		/// <summary>
		/// Retrieves provider associated with the order
		/// </summary>
		/// <returns>Provider unique identifier</returns>
		public int getFornecedorID(){
			return this.FornecedorID;
		}

		/// <summary>
		/// Set client associated with the order
		/// </summary>
		/// <param name="cliente">Unique identifier for a client</param>
		public void setClienteID(int cliente){
			this.ClienteID = cliente;
		}

		/// <summary>
		/// Retrieves client associated with the order
		/// </summary>
		/// <returns>Client unique identifier</returns>
		public int getClienteID(){
			return this.ClienteID;
		}

		/// <summary>
		/// Set credit card associated with the order
		/// </summary>
		/// <param name="cc">Credit Card to associate with the order</param>
		public void setCartaoCredito(CartaoCredito cc){
			this.CartaoCredito = cc;
		}

		/// <summary>
		/// Retrieves credit card unique indentifier associated with the order
		/// </summary>
		/// <returns>Credit card unique identifier</returns>
		public long getCartaoCreditoID(){
			return this.CartaoCreditoID;
		}

		/// <summary>
		/// Set evaluation of the order
		/// </summary>
		/// <param name="classEstadoEncomenda">Order status rating</param>
		public void setAvaliacao(int classEstadoEncomenda){
			this.avaliação=classEstadoEncomenda;
		}
	}
}