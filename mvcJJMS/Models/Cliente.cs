using System.Collections.Generic;
using System.Data.Entity;

namespace mvcJJMS.Models{
	public class Cliente : Utilizador  {
		private int ClienteID{get;set;}
		private String morada{get;set;}
		private String telefone{get;set;}
		private bool bloqueado{get;set;}

		private ICollection<Encomenda> encomendas{get;set};

		public bool TemEncomenda(int idEncomenda) {
			throw new System.Exception("Not implemented");
		}
		public void Bloqueia() {
			throw new System.Exception("Not implemented");
		}
	}
}