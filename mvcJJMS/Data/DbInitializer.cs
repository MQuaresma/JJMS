using mvcJJMS.Models;
using System;
using System.Linq;

namespace mvcJJMS.Data{
    public static class DbInitializer{
        public static void Initialize(JJMSContext context){
            context.Database.EnsureCreated();

            //Check if Db has been seeded
            if (context.Utilizadores.Any()){
                return;
            }
            
            //TODO: seed database
            
            var utilizadores = new Utilizador[]{
                new Utilizador(),
                new Utilizador(),
            };

            foreach (Utilizador s in utilizadores){
                context.Utilizadores.Add(s);
            }
            context.SaveChanges();

            var fornecedores = new Fornecedor[]{
                context.newFornecedor("nome","morada")
            };

            foreach(Fornecedor f in fornecedores){
                context.Fornecedores.Add(f);
            }
            context.SaveChanges();
        }
    }
}