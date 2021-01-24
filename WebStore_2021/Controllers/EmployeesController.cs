using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebStore_2021.Data;
using WebStore_2021.Models;

namespace WebStore_2021.Controllers
{
    //[Route("Staff")]
    public class EmployeesController : Controller
    {
        private List<Employee> _Employees;

        public EmployeesController()
        {
            _Employees = TestData.Employees;
        }

        //[Route("all")]
        public IActionResult Index() => View(_Employees);

        //[Route("info(id:{id})")] // в {} указывается параметр маршрута
        public IActionResult Details(int id) // http://localhost:5000/employees/details/2
        {
            var employee = _Employees.FirstOrDefault(e => e.Id == id);
            if (employee is not null)
                return View(employee);
            return NotFound();
        }
        ////визуализируем список сотрудников
        //public IActionResult Employees()
        //{
        //    return View(__Employees);
        //}

        //public IActionResult Card(int Id)
        //{
        //    List<object> currentWorker = new List<object>();
        //    foreach (var worker in __Employees)
        //    {
        //        if (worker.Id == Id)
        //        {
        //            ViewBag.Message = worker.DateOfBirth;
        //            currentWorker.Add(worker.LastName);
        //            currentWorker.Add(worker.FirstName);
        //            currentWorker.Add(worker.Patronymic);
        //            break;
        //        }
        //    }
        //    return View(currentWorker);
        //}
    }
}
