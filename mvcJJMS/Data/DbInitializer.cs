using mvcJJMS.Models;
using mvcJJMS.Controllers;
using System.Linq;

namespace mvcJJMS.Data{
    
    /// <summary>
    /// Seeds the database if it is found to be empty
    /// </summary>
    public static class DbInitializer{
        /// <summary>
        /// Seeds the database the given database
        /// </summary>
        /// <param name="context">Connection to the database</param>
        public static void Initialize(JJMSContext context){
            context.Database.EnsureCreated();

            // Check if Db has been seeded
            if (context.Utilizadores.Any()){
                return;
            }

            var fornecedores = new Fornecedor[]{
                context.newFornecedor("EmpresaTOP","Rua das Quintas"),
                context.newFornecedor("SuperHiper","Rua da Rotunda"),
                context.newFornecedor("HiperPreço","Rua dos Armazéns"),
                context.newFornecedor("MaxCool","Quinta da Praça"),
                context.newFornecedor("TOP-Cool","Praça do Jardim"),
            };
            foreach(Fornecedor f in fornecedores) context.Fornecedores.Add(f);
            context.SaveChanges();
            
            var funcionarios = new Funcionario[]{
                context.newFuncionario("António", UtilizadorController.hashFunction("func1"), "antonio@hotmail.com", 0),
                context.newFuncionario("Romeu", UtilizadorController.hashFunction("func2"), "romeu@hotmail.com", 1),
                context.newFuncionario("Tomé", UtilizadorController.hashFunction("func3"), "tome@hotmail.com", 2),
                context.newFuncionario("Olga", UtilizadorController.hashFunction("func4"), "olga@hotmail.com", 3),
                context.newFuncionario("Patrícia", UtilizadorController.hashFunction("func5"), "patricia@hotmail.com", 4),
            };
            foreach(Funcionario f in funcionarios) context.Utilizadores.Add(f);
            context.SaveChanges();

            var clientes = new Cliente[]{
                context.newCliente("Alfredo",UtilizadorController.hashFunction("cli1"),"alfredo@hotmail.com","Rua de Cima","987654321"),
                context.newCliente("Martim",UtilizadorController.hashFunction("cli2"),"martim@hotmail.com","Rua de Baixo","987654322"),
                context.newCliente("Carla",UtilizadorController.hashFunction("cli2"),"carla@hotmail.com","Rua do Lado","987654323"),
                context.newCliente("Matilde",UtilizadorController.hashFunction("cli2"),"matilde@hotmail.com","Rua de Trás","987654324"),
                context.newCliente("Diogo",UtilizadorController.hashFunction("cli2"),"diogo@hotmail.com","Rua da Esquina","987654325"),
            };
            foreach(Cliente c in clientes) context.Utilizadores.Add(c);
            context.SaveChanges();

            var cartoesc = new CartaoCredito[]{
                context.newCartaoCredito(3450126131722243,9,2020,295,"Portugal"),
                context.newCartaoCredito(5161227325730052,7,2019,229,"Russia"),
                context.newCartaoCredito(3459465147444819,10,2022,537,"US"),
            };
            foreach(CartaoCredito cc in cartoesc) context.Cartoes.Add(cc);
            context.SaveChanges();

            var encomendas= new Encomenda[]{
                context.newEncomenda(1,"Quinta da Rua","2018-05-28","05:45",1,6,1,3450126131722243),
                context.newEncomenda(2,"Quinta da Conde","2018-05-30","02:00",2,7,2,5161227325730052),
                context.newEncomenda(3,"Rua do Canto","2018-06-07","15:30",3,8,3,3459465147444819),
                context.newEncomenda(4,"Rua do Cais","2018-06-02","16:10",4,9,4,3450126131722243),            
            };
            foreach(Encomenda e in encomendas) context.Encomendas.Add(e);
            context.SaveChanges();
        }
    }
}