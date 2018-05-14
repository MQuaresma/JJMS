using mvcJJMS.Models;
using System;
using System.Linq;

namespace mvcJJMS.Data{
    public static class DbInitializer{
        public static void Initialize(JJMSContext context){
            context.Database.EnsureCreated();

            //Check if Db has been seeded
            if (context.Utilizador.Any()){
                return;
            }
            
            //TODO: seed database
            
            var utilizadores = new Utilizador[]{
                new Utilzador{Email="1",Password="1",Nome="1",FuncionarioID=-1,ClienteID=-1},
                new Utilzador{Email="2",Password="2",Nome="2",FuncionarioID=-1,ClienteID=-1}
            };

            foreach (Utilizador s in utilizadores){
                context.Students.Add(s);
            }
            context.SaveChanges();
        }
    }
}