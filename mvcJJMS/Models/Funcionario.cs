using System.Collections.Generic;

namespace mvcJJMS.Models{
	public class Funcionario : Utilizador  {
		public int zonaTrabalho{get;set;}
		public int nroEnc{get;set;}

		private Avaliacao avaliacao;
		private ICollection<Encomenda> encomendas{get;set;}

		public void AtualizaAvaliacao(int classFuncionario) {
			this.avaliacao.AdicionaAvaliacao(classFuncionario);
		}
	}
}