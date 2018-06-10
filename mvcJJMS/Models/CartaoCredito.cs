namespace mvcJJMS.Models{
	public class CartaoCredito {
		//[DatabaseGenerated(DatabaseGeneratedOption.None)] //Prevent the database from automatically generating the primary key for this entity
		public long CartaoCreditoID{get;set;}
		public int mes{get;set;}
		public int ano{get;set;}
		public int cvv{get;set;}
		public string pais{get;set;}

		public bool Pagamento() {
			throw new System.Exception("Not implemented");
		}
	}
}