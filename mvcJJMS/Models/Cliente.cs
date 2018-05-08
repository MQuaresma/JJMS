using System.Collections.Generic;
using System.Data.Entity;

namespace mvcJJMS.Models{
	public class Cliente : Utilizador  {
		private string morada;
		private string telefone;
		private bool bloqueado;

		private Dictionary<int,Encomenda> encomendas;

		public bool TemEncomenda(int idEncomenda) {
			throw new System.Exception("Not implemented");
		}
		public void Bloqueia() {
			throw new System.Exception("Not implemented");
		}
	}
	public class ClienteDBContext : DbContext{
		        public DbSet<Cliente> Clientes { get; set; }
    }
}