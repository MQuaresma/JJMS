using System.Collections.Generic;

namespace mvcJJMS.Models{
	public class Cliente : Utilizador  {
		public string morada{get;set;}	
		public string telefone{get;set;}
		public bool bloqueado{get;set;}

		public ICollection<Encomenda> Encomendas{get;set;}

		public bool TemEncomenda(int idEncomenda) {
			throw new System.Exception("Not implemented");
		}
		public void Bloqueia() {
			this.bloqueado = true;
		}

		public int getUtilizadorID(){
			return this.UtilizadorID;
		}
	}
}