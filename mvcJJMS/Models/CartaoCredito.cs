namespace mvcJJMS.Models{
	public class CartaoCredito {
		//[DatabaseGenerated(DatabaseGeneratedOption.None)] //Prevent the database from automatically generating the primary key for this entity
		public int CartaoCreditoID{get;set;}
		private int mes{get;set;}
		private int ano{get;set;}
		private int cvv{get;set;}
		private string pais{get;set;}

		public bool Pagamento() {
			throw new System.Exception("Not implemented");
		}
	}
}