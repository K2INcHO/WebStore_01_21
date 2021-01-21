using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebStore_2021.Models;

namespace WebStore_2021.Controllers
{
    //контроллер нужен для того, чтобы обработать входящее подключение
    //на каждый контроллер должно быть свое представление (View)
    public class HomeController : Controller
    {
        public IActionResult Index() => View();

        //добавить позже все остальные действия контроллера из всех остальных html
    }
}
