using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SeguridadProveedor.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;
/*
 Valor:
4-UnWh3NTk8f6we-Zc9-WY1oq.4TKiUA.Z
Id: 
40f53eb1-769e-47a3-a46e-fbb85dfbdcec
 */
namespace MvcCore
{
    public class Startup
    {
        IConfiguration Configuration;
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            String cadenasql =
                this.Configuration.GetConnectionString("cadenaaspnetdb");
            services.AddDbContext<ApplicationDbContext>
                (options => options.UseSqlServer(cadenasql));
            //DEBEMOS INDICAR QUE UTILIZAREMOS SERVICIOS
            //DE TERCEROS PARA LA AUTENTICACION
            services.AddDefaultIdentity<IdentityUser>()
       .AddEntityFrameworkStores<ApplicationDbContext>();
            //id app: bf7accfe-a7ee-4200-be68-b452cc396467
            //Secret Key: z89dOpFWPX4U1nhkpwy_.OWkF~qAjy_gFg
            services.AddAuthentication()
            .AddMicrosoftAccount
            (microsoftOptions =>
            {
                microsoftOptions.ClientId = "671b9023-0034-4dee-85d4-05a23628ea37";
                microsoftOptions.ClientSecret = "4-UnWh3NTk8f6we-Zc9-WY1oq.4TKiUA.Z";
            });


            services.AddControllersWithViews
                (options => options.EnableEndpointRouting = false);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseAuthorization();
            
            
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}"
                    );
            });
        }
    }
}
