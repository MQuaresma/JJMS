using System.Collections.Generic;
using System.Linq;

namespace mvcJJMS.Models{
	public class Cliente : Utilizador  {
		public string Morada{get;set;}	
		public string Telefone{get;set;}
		public bool Bloqueado{get;set;}
		public ICollection<Encomenda> Encomendas { get; set; }

		public bool TemEncomenda(int idEncomenda) {
			return this.Encomendas.Where(e=>e.EncomendaID==idEncomenda).FirstOrDefault()!=default(Encomenda);
		}
		public void Bloqueia() {
			this.Bloqueado = true;
		}

		public int getUtilizadorID(){
			return this.UtilizadorID;
		}
	}
}