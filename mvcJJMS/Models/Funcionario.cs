using System.Collections.Generic;

namespace mvcJJMS.Models{
	public class Funcionario : Utilizador  {
		public int ZonaTrabalho{get;set;}
		public int NroEnc{get;set;}
		private Avaliacao avaliacao;
		public ICollection<Encomenda> Encomendas { get; set; }
		
		public void AtualizaAvaliacao(int classFuncionario) {
			this.avaliacao.AdicionaAvaliacao(classFuncionario);
		}
	}
}