using System.Data.Entity;

namespace mvcJJMS.Models{
	public class Funcionario : Utilizador  {
		private int zonaTrabalho;
		private int nroEnc;

		private Avaliacao avaliacao;

		public void AtualizaAvaliacao(int classFuncionario) {
			throw new System.Exception("Not implemented");
		}
	}

	public class FuncionarioDBContext : DbContext{
		public DbSet<Funcionario> Funcionarios { get; set; }
    }
}