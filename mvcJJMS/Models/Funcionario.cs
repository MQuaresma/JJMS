using System.Collections.Generic;

namespace mvcJJMS.Models{
	public class Funcionario : Utilizador  {
		public int FuncionarioID{get;set;}
		private int zonaTrabalho{get;set;}
		private int nroEnc{get;set;}

		private Avaliacao avaliacao;
		private ICollection<Encomenda> encomendas{get;set;}

		public void AtualizaAvaliacao(int classFuncionario) {
			throw new System.Exception("Not implemented");
		}
	}
}