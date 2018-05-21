using mvcJJMS.Models;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace mvcJJMS.Data{
    public static class DbInitializer{

        static private byte[] hashFunction(string input){
			var sha384 = new SHA384CryptoServiceProvider();
			return sha384.ComputeHash(Encoding.UTF8.GetBytes(input));
		}
        public static void Initialize(JJMSContext context){
            context.Database.EnsureCreated();

            //Check if Db has been seeded
            if (context.Utilizadores.Any()){
                return;
            }
            
            //TODO: seed database
            var fornecedores = new Fornecedor[]{
                context.newFornecedor("EmpresaTOP","Rua das Quintas"),
                context.newFornecedor("SuperHiper","Rua da Rotunda"),
            };
            foreach(Fornecedor f in fornecedores) context.Fornecedores.Add(f);
            context.SaveChanges();

            var funcionarios = new Funcionario[]{
                context.newFuncionario("António", hashFunction("func1"), "antonio@hotmail.com", 0),
                context.newFuncionario("Romeu", hashFunction("func2"), "romeu@hotmail.com", 1),
            };
            foreach(Funcionario f in funcionarios) context.Funcionarios.Add(f);
            context.SaveChanges();

            var clientes = new Cliente[]{
                context.newCliente("Alfredo",hashFunction("cli1"),"alfredo@hotmail.com","Rua de Cima","987654321"),
                context.newCliente("Martim",hashFunction("cli2"),"martim@hotmail.com","Rua de Baixo","987654322"),
            };
            foreach(Cliente c in clientes) context.Clientes.Add(c);
            context.SaveChanges();
        }
    }
}