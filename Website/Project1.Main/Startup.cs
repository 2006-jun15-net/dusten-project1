using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using Project1.Business;
using Project1.DataAccess.Model;
using Project1.DataAccess.Repository;

using System;
using System.IO;

namespace Project1.Main {

    public class Startup {
		
		private const string ConnectionStringKey = "DB_CONNECTION_STR"; //"SqlServer";

        public Startup (IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices (IServiceCollection services) {

            services.AddLogging ();

            services.AddDbContext<Project0Context> (options =>
                 options.UseSqlServer (Environment.GetEnvironmentVariable (ConnectionStringKey)));
				 //Configuration.GetConnectionString (ConnectionStringKey)));

            services.AddScoped<ICustomerRepository, CustomerRepository> ();
            services.AddScoped<IStoreRepository, StoreRepository> ();
            services.AddScoped<ICustomerOrderRepository, CustomerOrderRepository> ();
            services.AddScoped<IStoreStockRepository, StoreStockRepository> ();

            services.AddDistributedMemoryCache ();

            services.AddSession (options => {

                options.IdleTimeout = TimeSpan.FromMinutes (20);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            services.AddControllersWithViews ();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure (IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory logger) {

            // Middleware happens here
            var path = Directory.GetCurrentDirectory ();

            logger.AddFile ($"{path}/logs/" + "{Date}.log");

            if (env.IsDevelopment ()) {
                app.UseDeveloperExceptionPage ();
            }

            else {
                app.UseExceptionHandler ("/Home/Error");
            }

            app.UseStaticFiles ();

            app.UseRouting ();

            app.UseAuthentication ();
            app.UseAuthorization ();

            app.UseSession ();

            app.UseEndpoints (endpoints => {

                endpoints.MapControllerRoute (

                    name: "store_orders_new",
                    pattern: "Store/Orders/New",
                    defaults: new { controller = "Store", action = "NewOrder" }
                );

                endpoints.MapControllerRoute (

                    name: "store_orders_list",
                    pattern: "Store/Orders/List",
                    defaults: new { controller = "Store", action = "ListOrders" }
                );

                endpoints.MapControllerRoute (

                    name: "default",
                    pattern: "{controller=Customer}/{action=Index}/{id?}"
                );
            });
        }
    }
}
