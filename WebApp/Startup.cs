using BLL;
using DAL;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp
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
            services.AddControllersWithViews();
            services.AddScoped<IRestaurantManager, RestaurantManager>();
            services.AddScoped<IRestaurantDB, RestaurantDB>();
            services.AddScoped<ICourierManager, CourierManager>();
            services.AddScoped<ICourierDB, CourierDB>();
            services.AddScoped<IDishManager, DishManager>();
            services.AddScoped<IDishDB, DishDB>();
            services.AddScoped<IOrderManager, OrderManager>();
            services.AddScoped<IOrderDB, OrderDB>();
            services.AddScoped<IOrderDetailDB, OrderDetailDB>();
            services.AddSession(options =>{
                options.IdleTimeout = TimeSpan.FromMinutes(5);
            });
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
            app.UseSession();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Home}/{id?}");
                endpoints.MapControllerRoute(
                    name: "admin",
                    pattern: "{controller=Admin}/{action=Admin}/{id?}"
                    );
                endpoints.MapControllerRoute(
                  name: "client",
                  pattern: "{controller=Client}/{action=Client}/{id?}"
                  );
                endpoints.MapControllerRoute(
                name: "courier",
                pattern: "{controller=Courier}/{action=Courier}/{id?}"
                );
            });
        }
    }
}
