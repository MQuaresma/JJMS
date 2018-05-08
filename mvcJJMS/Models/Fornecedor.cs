using System.Data.Entity;

namespace mvcJJMS.Models{
	public class Fornecedor {
		private int id;
		private string nome;
		private string morada;
	}

	public class FornecedorDBContext : DbContext{
		public DbSet<Fornecedor> Fornecedores { get; set; }
    }
}