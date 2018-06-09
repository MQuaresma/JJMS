using System.Collections.Generic;

namespace mvcJJMS.Models{
	public class Cliente : Utilizador  {
		public int ClienteID{get;set;}
		public string morada{get;set;}	
		public string telefone{get;set;}
		public bool bloqueado{get;set;}

		public ICollection<Encomenda> Encomendas{get;set;}

		public bool TemEncomenda(int idEncomenda) {
			throw new System.Exception("Not implemented");
		}
		public void Bloqueia() {
			throw new System.Exception("Not implemented");
		}

		public int getUtilizadorID(){
			return this.UtilizadorID;
		}
	}
}