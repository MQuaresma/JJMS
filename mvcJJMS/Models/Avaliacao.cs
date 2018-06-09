namespace mvcJJMS.Models{
	public class Avaliacao {
		private float avaliação{get;set;}
		private int numAvaliações{get;set;}

		public void AdicionaAvaliacao(int classificacao) {
			this.avaliação=((this.avaliação*this.numAvaliações++)+classificacao)/this.numAvaliações;
		}
	}
}