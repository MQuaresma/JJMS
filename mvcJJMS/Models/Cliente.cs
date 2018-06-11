using System.Collections.Generic;
using System.Linq;

namespace mvcJJMS.Models{
	/// <summary>
	/// Represents a user of type Client
	/// </summary>
	public class Cliente : Utilizador  {
		public string Morada{get;set;}	
		public string Telefone{get;set;}
		public bool Bloqueado{get;set;}
		public ICollection<Encomenda> Encomendas { get; set; }

		/// <summary>
		/// Checks if client is associated with a given order
		/// </summary>
		/// <param name="idEncomenda">Unique identifier for a single order</param>
		/// <returns>TRUE if client is associated with order, else FALSE</returns>
		public bool TemEncomenda(int idEncomenda) {
			return this.Encomendas.Where(e=>e.EncomendaID==idEncomenda).FirstOrDefault()!=default(Encomenda);
		}

		/// <summary>
		/// Method used to block client
		/// </summary>
		public void Bloqueia() {
			this.Bloqueado = true;
		}
		
		/// <summary>
		/// Retrieves unique identifier of client
		/// </summary>
		/// <returns>Client unique identifier</returns>
		public int getUtilizadorID(){
			return this.UtilizadorID;
		}
	}
}