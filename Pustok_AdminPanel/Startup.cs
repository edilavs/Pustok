using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Pustok.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pustok
{
    public class Startup
    {
      
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(@"Server=VALIDAPC;Database=BP202_Pustok;Trusted_Connection=TRUE")
            );

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseStaticFiles();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                 "area",
                 "{area:exists}/{controller=dashboard}/{action=index}/{id?}"
                 );
                endpoints.MapControllerRoute(
                    "default",
                    "{controller=home}/{action=index}/{id?}");

             
            });
        }
    }
}
