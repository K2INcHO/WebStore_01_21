using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace WebStore_2021
{
    public class Startup
    {  
        private IConfiguration Configuration { get; }

        public Startup(IConfiguration Configuration)
        {
            this.Configuration = Configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddMvc();
            services.AddControllersWithViews();
            //services.AddControllersWithViews().AddRazorRunTimeCompilation(); // будет раскомментровано после обновления asp.net до версии 5.0
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles(); //подключаем статическое содержимое

            app.UseRouting();

            //var greetings = Configuration["Greetings"];

            //прописываем маршруты
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/greetings", async context =>
                {
                    await context.Response.WriteAsync("Greetings");
                });

                endpoints.MapControllerRoute(
                    "default",
                    "{controller=Home}/{action=Index}/{id?}"); //? - значит, что параметр необязателен
                // http://localhost:5000/ -> controller = "Home" action = "Index" параметр = null
                // http://localhost:5000/Catalog/Products/5 -> controller = "Catalog" action = "Products" параметр = 5
            });
        }
    }
}
