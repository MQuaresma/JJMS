using Microsoft.EntityFrameworkCore;
using System.Linq;
using mvcJJMS.Models;
using FILE = System.String;
using Date = System.String;
using Time = System.String;

namespace mvcJJMS.Data{
    public class JJMSContext : DbContext{
        
        public JJMSContext(DbContextOptions<JJMSContext> options) : base(options){
        
        }
        
        //Each DbSet corresponds to a table in the database
        public DbSet<Cliente> Clientes { get; set; }
		public DbSet<Funcionario> Funcionarios { get; set; }
		public DbSet<Fornecedor> Fornecedores { get; set; }
        public DbSet<Encomenda> Encomendas { get; set; }
        public DbSet<CartaoCredito> Cartoes { get; set; }
        public DbSet<Utilizador> Utilizadores { get; set; }
        

        protected override void OnModelCreating(ModelBuilder modelBuilder){
            modelBuilder.Entity<Encomenda>()
                        .HasOne(m=>m.Cliente)
                        .WithMany(m=>m.Encomendas)
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                        
            modelBuilder.Entity<Encomenda>()
                        .HasOne(m=>m.Funcionario)
                        .WithMany(m=>m.Encomendas)
                        .OnDelete(DeleteBehavior.Restrict);

             modelBuilder.Entity<Encomenda>()
                        .HasOne(m=>m.Fornecedor)
                        .WithMany(m=>m.Encomendas)
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
        }

        public Fornecedor newFornecedor(string nome, string morada){
            return new Fornecedor{nome=nome,morada=morada};
        }

        public Cliente newCliente(string nome, byte[] passwordH, string email, string morada, string telefone){
            return new Cliente{Nome=nome,Password=passwordH,Email=email,Morada=morada,Telefone=telefone,Bloqueado=false};
        }

        public Funcionario newFuncionario(string nome, byte[] passwordH, string email, int zonaTrabalho){
            return new Funcionario{Nome=nome,Password=passwordH,Email=email,ZonaTrabalho=zonaTrabalho,NroEnc=0};
        }

        public CartaoCredito newCartaoCredito(int mes, int ano, int cvv, string pais){
            return new CartaoCredito{mes=mes,ano=ano,cvv=cvv,pais=pais};
        }

        public Encomenda newEncomenda(int estado, string destino, Date dia, Time hora, int fornecedor, int cliente, int funcionario){
            return new Encomenda{FornecedorID=fornecedor,ClienteID=cliente,FuncionarioID=funcionario,
                                estado=estado,destino=destino,fatura=null,avaliação=0,custo=0,dia=dia,hora=hora};
        }
    }
}