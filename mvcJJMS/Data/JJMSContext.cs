using mvcJJMS.Models;
using Microsoft.EntityFrameworkCore;

namespace mvcJJMS.Data{
    public class JJMSContext : DbContext{
        
        public JJMSContext(DbContextOptions<JJMSContext> options) : base(options){
        
        }
        
        //Each DbSet corresponds to a table in the database
        public DbSet<Utilizador> Utilizadores { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
		public DbSet<Funcionario> Funcionarios { get; set; }
		public DbSet<Fornecedor> Fornecedores { get; set; }
        public DbSet<Encomenda> Encomendas { get; set; }
        public DbSet<CartaoCredito> Cartoes { get; set; }
    
        public Fornecedor newFornecedor(string nome, string morada){
            Fornecedor nFornecedor=new Fornecedor();
            nFornecedor.setNome(nome);
            nFornecedor.setMorada(morada);
            return nFornecedor;
        }
    }
}