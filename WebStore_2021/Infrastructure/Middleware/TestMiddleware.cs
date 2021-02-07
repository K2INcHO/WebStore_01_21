using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace WebStore_2021.Infrastructure.Middleware
{
    public class TestMiddleware
    {
        private readonly RequestDelegate _Next;

        public TestMiddleware(RequestDelegate Next) => _Next = Next;

        public async Task InvokeAsync(HttpContext context)
        {
            // Действие до следующего узла в конвейере
            //context.Response.b

            var next = _Next(context);

            // Действие во время того, как оставшаяся часть конвейера что-то делает с контекстом
            await next; // выполняется точка синхронизации

            // Действие по завершении работы оставшейся чайсти конвейера
        }
    }
}
