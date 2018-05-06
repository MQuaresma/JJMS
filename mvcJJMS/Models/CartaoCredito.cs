namespace mvcJJMS.Models{
	public class CartaoCredito {
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