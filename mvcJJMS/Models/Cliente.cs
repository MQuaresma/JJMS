using System.Collections.Generic;

namespace mvcJJMS.Models{
	public class Cliente : Utilizador  {
		private int ClienteID{get;set;}
		private string morada{get;set;}
		private string telefone{get;set;}
		private bool bloqueado{get;set;}

		private ICollection<Encomenda> encomendas{get;set;}

		public bool TemEncomenda(int idEncomenda) {
			throw new System.Exception("Not implemented");
		}
		public void Bloqueia() {
			throw new System.Exception("Not implemented");
		}
	}
}