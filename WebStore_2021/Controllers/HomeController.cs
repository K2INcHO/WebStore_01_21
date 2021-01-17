using Microsoft.AspNetCore.Mvc;

namespace WebStore_2021.Controllers
{
    //контроллер нужен для того, чтобы обработать входящее подключение
    //на каждый контроллер должно быть свое представление (View)
    public class HomeController : Controller
    {
        public IActionResult Index() => View("SecondView");

        public IActionResult SecondAction()
        {
            return Content("Second controller action");
        }
    }
}
