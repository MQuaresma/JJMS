using System.Data.Entity;
using FILE = System.String;
using Date = System.String;
using Time = System.String;

namespace mvcJJMS.Models{
	public class Encomenda {
		private int id;
		private int estado;
		private string destino;
		private FILE fatura;
		private int avaliação;
		private float custo;
		private Date dia;
		private Time hora;
		private int idFornecedor;
		private int funcResp;
		private CartaoCredito cartão;

		public void GerarFatura(Cliente cliente) {
			throw new System.Exception("Not implemented");
		}
	}

	public class EncomendaDBContext : DbContext{
		public DbSet<Encomenda> Encomendas { get; set; }
    }
}