using mvcJJMS.Models;
using mvcJJMS.Controllers;
using System.Linq;

namespace mvcJJMS.Data{
    public static class DbInitializer{
        public static void Initialize(JJMSContext context){
            context.Database.EnsureCreated();

            //Check if Db has been seeded
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
            foreach(Funcionario f in funcionarios) context.Funcionarios.Add(f);
            context.SaveChanges();

            var clientes = new Cliente[]{
                context.newCliente("Alfredo",UtilizadorController.hashFunction("cli1"),"alfredo@hotmail.com","Rua de Cima","987654321"),
                context.newCliente("Martim",UtilizadorController.hashFunction("cli2"),"martim@hotmail.com","Rua de Baixo","987654322"),
                context.newCliente("Carla",UtilizadorController.hashFunction("cli2"),"carla@hotmail.com","Rua do Lado","987654323"),
                context.newCliente("Matilde",UtilizadorController.hashFunction("cli2"),"matilde@hotmail.com","Rua de Trás","987654324"),
                context.newCliente("Diogo",UtilizadorController.hashFunction("cli2"),"diogo@hotmail.com","Rua da Esquina","987654325"),
            };
            foreach(Cliente c in clientes) context.Clientes.Add(c);
            context.SaveChanges();
        }
    }
}