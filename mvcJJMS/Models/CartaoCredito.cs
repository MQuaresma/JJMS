using System.Data.Entity;

namespace mvcJJMS.Models{
	public class CartaoCredito {
		[DatabaseGenerated(DatabaseGeneratedOption.None)] //Prevent the database from automatically generating the primary key for this entity
		private int num;
		private int mes;
		private int ano;
		private int cvv;
		private string pais;

		public bool Pagamento() {
			throw new System.Exception("Not implemented");
		}
	}
}