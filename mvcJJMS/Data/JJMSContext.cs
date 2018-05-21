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

        public Utilizador newUtilizador(string nome, byte[] passwordH, string email){
            Utilizador nUtilizador=new Utilizador();
            nUtilizador.Nome=nome;
            nUtilizador.Password=passwordH;
            nUtilizador.Email=email;
            return nUtilizador;
        }

        public Cliente newCliente(string nome, byte[] passwordH, string email, string morada, string telefone){
            Cliente nCliente = new Cliente();
            nCliente.Nome=nome;
            nCliente.Password=passwordH;
            nCliente.Email=email;
            nCliente.morada = morada;
            nCliente.telefone = telefone;
            nCliente.bloqueado = false;
            return nCliente;
        }

        public Funcionario newFuncionario(string nome, byte[] passwordH, string email, int zonaTrabalho){
            Funcionario nFuncionario = new Funcionario();
            nFuncionario.Nome=nome;
            nFuncionario.Password=passwordH;
            nFuncionario.Email=email;
            nFuncionario.zonaTrabalho = zonaTrabalho;
            nFuncionario.nroEnc = 0;
            return nFuncionario;
        }
    }
}