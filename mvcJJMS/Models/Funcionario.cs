using System.Collections.Generic;

namespace mvcJJMS.Models{
	public class Funcionario : Utilizador  {
		public int ZonaTrabalho{get;set;}
		public int NroEnc{get;set;}
		public float Avaliação{get;set;}
		public int NumAvaliações{get;set;}

		public ICollection<Encomenda> Encomendas { get; set; }

		/// <summary>
		/// updates the evalution of the employee, calculating the new average
		/// </summary>
		/// <param name="classificacao"></param>
		public void AtualizaAvaliacao(int classificacao) {
			this.Avaliação=((this.Avaliação*this.NumAvaliações++)+classificacao)/this.NumAvaliações;
		}
	}
}