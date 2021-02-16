using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using WebStore.DAL.Context;
using WebStore.Domain.Entities.Identity;
using WebStore.Infrastructure.Services.InSQL;
using WebStore_2021.Data;
using WebStore_2021.Infrastructure.Conventions;
using WebStore_2021.Infrastructure.Interfaces;
using WebStore_2021.Infrastructure.Middleware;
using WebStore_2021.Infrastructure.Services;
using WebStore_2021.Infrastructure.Services.InCookies;
using WebStore_2021.Infrastructure.Services.Inmemory;
using WebStore_2021.Infrastructure.Services.InSQL;

namespace WebStore_2021
{
    public record Startup(IConfiguration Configuration)
    {
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddDbContext<WebStoreDB>(opt => opt.UseSqlite(Configuration.GetConnectionString("Sqlite")));
            services.AddDbContext<WebStoreDB>(opt =>
                opt.UseSqlServer(Configuration.GetConnectionString("Default"))
                .UseLazyLoadingProxies()
                );
            services.AddTransient<WebStoreDbInitializer>();

            services.AddIdentity<User, Role>(/*opt => { }*/)
                .AddEntityFrameworkStores<WebStoreDB>()
                .AddDefaultTokenProviders();

            // конфигурация системы Identity
            services.Configure<IdentityOptions>(opt =>
            {
#if DEBUG
                opt.Password.RequiredLength = 3;
                opt.Password.RequireDigit = false;
                opt.Password.RequireLowercase = false;
                opt.Password.RequireUppercase = false;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequiredUniqueChars = 3;
#endif
                opt.User.RequireUniqueEmail = false;
                opt.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";

                opt.Lockout.AllowedForNewUsers = false;
                opt.Lockout.MaxFailedAccessAttempts = 10;
                opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
            });

            // конфигурация системы Cookes
            services.ConfigureApplicationCookie(opt =>
            {
                opt.Cookie.Name = "WebStore.GB";
                opt.Cookie.HttpOnly = true;
                opt.ExpireTimeSpan = TimeSpan.FromDays(10);

                opt.LoginPath = "/Account/Login";
                opt.LogoutPath = "/Account/Logout";
                opt.AccessDeniedPath = "/Account/AccessDenied";

                opt.SlidingExpiration = true;
            });

            //регистрируем сервисы
            services.AddTransient<IEmployeesData, InMemoryEmployeesData>();
            //services.AddTransient<IProductData, InMemoryProductData>();
            services.AddTransient<IProductData, SQLProductData>();
            services.AddTransient<ICartService, InCookiesCartService>();
            services.AddTransient<IOrderService, SqlOrderService>();

            services
                .AddControllersWithViews(/*opt => opt.Conventions.Add(new TestControllerModelConvention())*/)
                .AddRazorRuntimeCompilation();
        }
        // Все вызовы к классу App - добавление промежуточного ПО (настройка конвейера)
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, WebStoreDbInitializer db)
        {
            db.Initialize();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage(); // добавляется страница обработки исключений
                app.UseBrowserLink();   // добавляем скрипт в конец страницы для подключения к студии (для отладки)
            }

            app.UseStaticFiles(); //подключаем статическое содержимое (по умолчанию будет жить в wwwroot) - срабатывает проверка на обработку файла

            app.UseRouting(); // происходит извлечение информации о маршрутах

            app.UseAuthentication();    // извлекает из cookes объект пользователя и десерилезует его
            app.UseAuthorization();

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
                endpoints.MapControllerRoute(
                    name: "areas",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                );

                endpoints.MapControllerRoute(
                    "default",
                    "{controller=Home}/{action=Index}/{id?}"); //? - значит, что параметр необязателен
                // http://localhost:5000/ -> controller = "Home" action = "Index" параметр = null
                // http://localhost:5000/Catalog/Products/5 -> controller = "Catalog" action = "Products" параметр = 5
            });
        }
    }
}
