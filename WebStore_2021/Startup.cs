using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace WebStore_2021
{
    public record Startup(IConfiguration Configuration)
    {  
        //private IConfiguration Configuration { get; }

        //public Startup(IConfiguration Configuration)
        //{
        //    this.Configuration = Configuration;
        //}

        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddMvc();
            //services.AddControllersWithViews();
            services.AddControllersWithViews().AddRazorRuntimeCompilation();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles(); //���������� ����������� ���������� (�� ��������� ����� ���� � wwwroot)

            app.UseRouting();

            //var greetings = Configuration["Greetings"];

            //����������� ��������
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/greetings", async context =>
                {
                    await context.Response.WriteAsync("Greetings");
                });

                endpoints.MapControllerRoute(
                    "default",
                    "{controller=Home}/{action=Index}/{id?}"); //? - ������, ��� �������� ������������
                // http://localhost:5000/ -> controller = "Home" action = "Index" �������� = null
                // http://localhost:5000/Catalog/Products/5 -> controller = "Catalog" action = "Products" �������� = 5
            });
        }
    }
}
