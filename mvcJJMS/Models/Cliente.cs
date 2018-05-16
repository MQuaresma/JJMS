using System.Collections.Generic;

namespace mvcJJMS.Models{
	public class Cliente : Utilizador  {
		public int ClienteID{get;set;}
		public string morada{get;set;}
		public string telefone{get;set;}
		public bool bloqueado{get;set;}

		private ICollection<Encomenda> encomendas{get;set;}

		public bool TemEncomenda(int idEncomenda) {
			throw new System.Exception("Not implemented");
		}
		public void Bloqueia() {
			throw new System.Exception("Not implemented");
		}
	}
}