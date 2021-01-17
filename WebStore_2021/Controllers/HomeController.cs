using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebStore_2021.Models;

namespace WebStore_2021.Controllers
{
    //контроллер нужен для того, чтобы обработать входящее подключение
    //на каждый контроллер должно быть свое представление (View)
    public class HomeController : Controller
    {
        private static readonly List<Employee> __Employees = new()
        {
            new Employee { Id = 1, LastName = "Иванов", FirstName = "Иван", Patronymic = "Иванович", Age = 37 },
            new Employee { Id = 2, LastName = "Петров", FirstName = "Петр", Patronymic = "Петрович", Age = 25 },
            new Employee { Id = 3, LastName = "Сидоров", FirstName = "Сидор", Patronymic = "Сидорович", Age = 27 },
        };

        public IActionResult Index() => View("SecondView");

        public IActionResult SecondAction()
        {
            return Content("Second controller action");
        }

        //визуализируем список сотрудников
        public IActionResult Employees()
        {
            return View(__Employees);
        }
    }
}
