using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Security;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using mvcJJMS.Data;

namespace mvcJJMS
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = BuildWebHost(args);
            using(var scope=host.Services.CreateScope()){
                var services= scope.ServiceProvider;
                try{
                    var context = services.GetRequiredService<JJMSContext>();
                    DbInitializer.Initialize(context);

                }catch (SqlException e){
                    Console.WriteLine(e.ToString());
                }
            }    
            host.Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
