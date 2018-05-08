using System.Data.Entity;

namespace mvcJJMS.Models{
	public class Utilizador {
		private int id;
		private string email;
		private string password;
		private string nome;
	}

	public class UtilizadorDBContext : DbContext{
		public DbSet<Utilizador> Utilizadores { get; set; }
    }
	
}