using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using WebStore_2021.Infrastructure.Conventions;
using WebStore_2021.Infrastructure.Interfaces;
using WebStore_2021.Infrastructure.Middleware;
using WebStore_2021.Infrastructure.Services;

namespace WebStore_2021
{
    public record Startup(IConfiguration Configuration)
    {  
        public void ConfigureServices(IServiceCollection services)
        {
            //регистрируем сервис
            services.AddTransient<IEmployeesData, InMemoryEmployeesData>();

            //services.AddTransient<>(); // при каждом запросе интерфейса будет создаваться новый объект реализации
            //services.AddScoped<>();    // реализация сервиса создается только в первый раз при его запросе
            //services.AddSingleton<>(); // (промежут. вариант) в контейнере сервисов могут создаваться области видимости

            //services.AddMvc(opt => opt.Conventions.Add(new TestControllerModelConvention())); // можно писать так
            services
                .AddControllersWithViews(/*opt => opt.Conventions.Add(new TestControllerModelConvention())*/)
                .AddRazorRuntimeCompilation();
        }
        // Все вызовы к классу App - добавление промежуточного ПО (настройка конвейера)
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env/*, IServiceProvider services*/)
        {
            //var employees1 = services.GetService<IEmployeesData>();
            //var employees2 = services.GetService<IEmployeesData>();

            //var hash1 = employees1.GetHashCode();
            //var hash2 = employees1.GetHashCode();

            //using(var scope = services.CreateScope())
            //{
            //    var employees3 = scope.ServiceProvider.GetService<IEmployeesData>();
            //    var hash3 = employees1.GetHashCode();
            //}

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage(); // добавляется страница обработки исключений
                app.UseBrowserLink();
            }

            app.UseStaticFiles(); //подключаем статическое содержимое (по умолчанию будет жить в wwwroot) - срабатывает проверка на обработку файла

            app.UseRouting(); // происходит извлечение информации о маршрутах

            app.UseWelcomePage("/welcome");

            app.UseMiddleware<TestMiddleware>();

            app.MapWhen(
                context => context.Request.Query.ContainsKey("id") && context.Request.Query["id"] == "5",
                context => context.Run(async request => await request.Response.WriteAsync("Hello with id == 5!"))
                ); // альтернатива Map
            app.Map("/hello", context => context.Run(async request => await request.Response.WriteAsync("Hello!!!")));

            //прописываем маршруты
            app.UseEndpoints(endpoints =>
            {
                // Проекция запроса на действие
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
