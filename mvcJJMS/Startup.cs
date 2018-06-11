using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using mvcJJMS.Data;
using mvcJJMS.Controllers;
using Microsoft.EntityFrameworkCore;

namespace mvcJJMS{
    public class Startup{
        public Startup(IConfiguration configuration){
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        /// <summary>
        ///  This method gets called by the runtime and is used for adding services to the container
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services){
            //Register JJMSContext as a service
            services.AddDbContext<JJMSContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            //Register all Controllers as services
            services.AddMvc().AddControllersAsServices();
        }

        /// <summary>
        ///  This method gets called by the runtime and is used to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env){
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();
            else app.UseExceptionHandler("/Home/Error");

            app.UseStaticFiles();

            app.UseMvc(routes =>{
                routes.MapRoute(name: "default",template: "{controller=MenuPrincipal}/{action=Index}/");
            });
        }
    }
}
