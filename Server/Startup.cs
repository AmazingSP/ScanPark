using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Server.Models;
using System;

namespace Server
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; }

        // Konfigurationsdatei wird erstellt
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        // Hier werden die einzelnen Services, wie
        // Datenbankverbindungen, Benutzerkontext, etc. konfiguriert
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<UserIdentityDBContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddIdentity<UserIdentity, UserRole>()
                .AddEntityFrameworkStores<UserIdentityDBContext>()
                .AddDefaultTokenProviders();

            services.AddMvc();
        }

        // Diese Methode wird zur Laufzeit aufgerufen
        // Hier wird der HTTP request konfiguriert.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Admin/Error");
            }

            app.UseStaticFiles();
            app.UseIdentity();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Account}/{action=Login}/{id?}");
            });
        }
    }
}
