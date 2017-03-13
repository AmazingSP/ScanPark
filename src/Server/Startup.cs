using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Server.Models;
using Server.Models.Identity;

namespace Server
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer("Server=.\\SQLEXPRESS;Database=ScanParkDb;Trusted_Connection=True;"));
            services.AddIdentity<RegisteredUserModel, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddMvc();
        }


        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.UseIdentity();

            app.UseCookieAuthentication(new CookieAuthenticationOptions() {
                ExpireTimeSpan = new System.TimeSpan(0, 0, 0, 10),
                LoginPath = new PathString("/Account/Index")
            });

            app.UseMvcWithDefaultRoute();

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("404 - Seite nicht gefunden :-(");
            });
        }
    }
}
