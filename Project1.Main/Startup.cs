using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Project1.DataAccess.Model;
using Project1.Business;
using Project1.DataAccess.Repository;
using System;

namespace Project1.Main {

    public class Startup {

        public Startup (IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices (IServiceCollection services) {

            services.AddDbContext<Project0Context>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("SqlServer")));

            services.AddScoped <ICustomerRepository, CustomerRepository> ();
            services.AddScoped <IStoreRepository, StoreRepository> ();

            services.AddDistributedMemoryCache ();

            services.AddSession (options => {

                options.IdleTimeout = TimeSpan.FromSeconds (10);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            services.AddControllersWithViews ();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure (IApplicationBuilder app, IWebHostEnvironment env) {

            if (env.IsDevelopment ()) {
                app.UseDeveloperExceptionPage ();
            }

            else {
                app.UseExceptionHandler ("/Home/Error");
            }

            app.UseStaticFiles ();

            app.UseRouting ();

            app.UseAuthorization ();
            app.UseSession ();

            app.UseEndpoints (endpoints => {

                endpoints.MapControllerRoute (

                    name: "default",
                    pattern: "{controller=Customer}/{action=Index}/{id?}");
            });
        }
    }
}
