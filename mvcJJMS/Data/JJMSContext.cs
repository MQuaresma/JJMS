using Microsoft.EntityFrameworkCore;
using System.Linq;
using mvcJJMS.Models;
using FILE = System.String;
using Date = System.String;
using Time = System.String;

namespace mvcJJMS.Data{
    /// <summary>
    /// Represents a connection to the database with the associated connection string
    /// </summary>
    public class JJMSContext : DbContext{
        
        public JJMSContext(DbContextOptions<JJMSContext> options) : base(options){
        
        }
        
        /// <summary>
        /// Database table containing client info
        /// </summary>
        public DbSet<Cliente> Clientes { get; set; }
		
        /// <summary>
        /// Database table containing employee info
        /// </summary>
        public DbSet<Funcionario> Funcionarios { get; set; }
		
        /// <summary>
        /// Database table containing provider info
        /// </summary>
        public DbSet<Fornecedor> Fornecedores { get; set; }
        
        /// <summary>
        /// Database table containing order info
        /// </summary>
        public DbSet<Encomenda> Encomendas { get; set; }
        
        /// <summary>
        /// Database table containing credit card info
        /// </summary>
        public DbSet<CartaoCredito> Cartoes { get; set; }
        
        /// <summary>
        /// Database table containing user info, both employee and client
        /// </summary>
        public DbSet<Utilizador> Utilizadores { get; set; }
        

        /// <summary>
        /// Registers class attributes as foreign keys and their 
        /// </summary>
        /// <param name="modelBuilder"></param>
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

            modelBuilder.Entity<Encomenda>()
                        .HasOne(m=>m.CartaoCredito)
                        .WithMany(m=>m.Encomendas)
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
        }

        /// <summary>
        /// Wrapper for creating a new Fornecedor object
        /// </summary>
        /// <param name="nome">Name of the provider</param>
        /// <param name="morada">Address of the provider</param>
        /// <returns>Fornecedor object</returns>
        public Fornecedor newFornecedor(string nome, string morada){
            return new Fornecedor{nome=nome,morada=morada};
        }

        /// <summary>
        /// Wrapper for creating a new Cliente object
        /// </summary>
        /// <param name="nome">Username of the client</param>
        /// <param name="passwordH">Client password</param>
        /// <param name="email">Client email</param>
        /// <param name="morada">Client address</param>
        /// <param name="telefone">Client phone number</param>
        /// <returns>Cliente object</returns>
        public Cliente newCliente(string nome, byte[] passwordH, string email, string morada, string telefone){
            return new Cliente{Nome=nome,Password=passwordH,Email=email,Morada=morada,Telefone=telefone,Bloqueado=false};
        }

        /// <summary>
        /// Wrapper for creating a new Funcionario object
        /// </summary>
        /// <param name="nome">Username of the employee</param>
        /// <param name="passwordH">Employee password</param>
        /// <param name="email">Employee email</param>
        /// <param name="zonaTrabalho">Identifier of the work zone that the employee is assigned</param>
        /// <returns>Funcionario object</returns>
        public Funcionario newFuncionario(string nome, byte[] passwordH, string email, int zonaTrabalho){
            return new Funcionario{Nome=nome,Password=passwordH,Email=email,ZonaTrabalho=zonaTrabalho,NroEnc=0,Avaliação=0,NumAvaliações=0};
        }


        /// <summary>
        /// Wrapper for creating a new CartaoCredito object
        /// </summary>
        /// <param name="numCartaoCredito">Credit card number</param>
        /// <param name="mes">Expiration month</param>
        /// <param name="ano">Expiration year</param>
        /// <param name="cvv">Card verification value</param>
        /// <param name="pais">Country of the credit card bank</param>
        /// <returns>CartaoCredito object</returns>
        public CartaoCredito newCartaoCredito(long numCartaoCredito, int mes, int ano, int cvv, string pais){
            return new CartaoCredito{CartaoCreditoID=numCartaoCredito,mes=mes,ano=ano,cvv=cvv,pais=pais};
        }

        /// <summary>
        /// Wrapper for creating a new Encomenda object
        /// </summary>
        /// <param name="estado">Current status on the delivery process</param>
        /// <param name="destino">Destination address</param>
        /// <param name="dia">Delivery day</param>
        /// <param name="hora">Delivery time</param>
        /// <param name="fornecedor">ID of the product provider</param>
        /// <param name="cliente">ID of the client that ordered the product</param>
        /// <param name="funcionario">ID of the employee currently responsible</param>
        /// <param name="cc">Credit card number associated with the order</param>
        /// <returns>Encomenda object</returns>
        public Encomenda newEncomenda(int estado, string destino, Date dia, Time hora, int fornecedor, int cliente, int funcionario, long cc){
            return new Encomenda{FornecedorID=fornecedor,ClienteID=cliente,FuncionarioID=funcionario,CartaoCreditoID=cc,
                                estado=estado,destino=destino,fatura=null,avaliação=0,custo=0,dia=dia,hora=hora};
        }
    }
}