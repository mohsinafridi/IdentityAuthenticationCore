using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityAuthenticationCore.Data;
using IdentityAuthenticationCore.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace IdentityAuthenticationCore
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
             // For Sqlite Database
            // services.AddDbContext<ApplicationDbContext>(x => x.UseSqlite("ConnectionString"));
            services.AddDbContext<ApplicationDbContext>(config =>
            {
                // for in memory database
                config.UseInMemoryDatabase("ApplicationDbContext");
            });

            // AddIdentity :-  Registers the services  
            services.AddIdentity<AppUser, IdentityRole>(config =>
            {
                // User defined password policy settings.  
                config.Password.RequiredLength = 4;
                config.Password.RequireDigit = false;
                config.Password.RequireNonAlphanumeric = false;
                config.Password.RequireUppercase = false;
            })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            // Cookie settings   
            services.ConfigureApplicationCookie(config =>
            {
                config.Cookie.Name = "IdentityProjectCookie";
                config.LoginPath = "/Home/Login"; // User defined login path  
                config.ExpireTimeSpan = TimeSpan.FromMinutes(5);
            });

            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
