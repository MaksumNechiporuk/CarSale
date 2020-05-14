
using CarSale.Entities;
using Entities.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using System;
using System.IO;

namespace CarSale
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DBContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/build";
            });
            services.AddIdentity<AppUser, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 5;
                options.Password.RequiredUniqueChars = 2;
            })
              .AddEntityFrameworkStores<DBContext>();
        }


        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider services, IConfiguration config)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }
            app.UseStaticFiles();
            app.UseSpaStaticFiles();


            app.UseAuthentication();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseReactDevelopmentServer(npmScript: "start");

                    // spa.UseReactDevelopmentServer(npmScript: "start --host 0.0.0.0:3000");
                    // spa.UseReactDevelopmentServer("http://localhost:3000");

                }
            });
            using (var scope = app.ApplicationServices.CreateScope())
            {
                DBContext content = scope.ServiceProvider.GetRequiredService<DBContext>();
                DBObject.Initial(content, services, env, config);
            }
        }
    }
}
