using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace mvcJJMS.Models{
	/// <summary>
	/// Represents a Credit Card used to pay for the costs associated with an order
	/// </summary>
	public class CartaoCredito {
		[DatabaseGenerated(DatabaseGeneratedOption.None)] //Prevent the database from automatically generating the primary key for this entity
		public long CartaoCreditoID{get;set;}
		public int mes{get;set;}
		public int ano{get;set;}
		public int cvv{get;set;}
		public string pais{get;set;}
		public ICollection<Encomenda> Encomendas{get;set;}
		
		/// <summary>
		/// Payment realization with credit card
		/// </summary>
		/// <returns>TRUE if payment is correctly done, else FALSE</returns>
		public bool Pagamento() {
			throw new System.Exception("Not implemented");
		}
	}
}