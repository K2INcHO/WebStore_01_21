using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebStore_2021.Data;
using WebStore_2021.Infrastructure.Interfaces;
using WebStore_2021.Models;
using WebStore_2021.ViewModels;

namespace WebStore_2021.Controllers
{
    //[Route("Staff")]
    public class EmployeesController : Controller
    {
        private IEmployeesData _EmployeesData;

        public EmployeesController(IEmployeesData EmployeesData) => _EmployeesData = EmployeesData;


        //[Route("all")]
        public IActionResult Index() => View(_EmployeesData.Get());

        //[Route("info(id:{id})")] // в {} указывается параметр маршрута
        public IActionResult Details(int id) // http://localhost:5000/employees/details/2
        {
            var employee = _EmployeesData.Get(id);
            if (employee is not null)
                return View(employee);
            return NotFound();
        }

        public IActionResult Create() => View("Edit", new EmployeeViewModel());

        #region Edit
        public IActionResult Edit(int? id)
        {
            if (id is null)
                return View(new EmployeeViewModel());

            if (id <= 0) return BadRequest();

            var employee = _EmployeesData.Get((int)id);

            if (employee is null)
                return NotFound();

            return View(new EmployeeViewModel
            {
                Id = employee.Id,
                LastName = employee.LastName,
                Name = employee.FirstName,
                MiddleName = employee.Patronymic,
                Age = employee.Age,
            });
        }

        [HttpPost]
        public IActionResult Edit(EmployeeViewModel model)
        {
            if (model is null)
                throw new ArgumentNullException(nameof(model));

            if (model.Name == "Усама" && model.MiddleName == "бен" && model.LastName == "Байден")
                ModelState.AddModelError("", "Его выбрали нечестно!");

            if (ModelState.IsValid) return View(model);

            var employee = new Employee
            {
                Id = model.Id,
                LastName = model.LastName,
                FirstName = model.Name,
                Patronymic = model.MiddleName,
                Age = model.Age
            };

            if (employee.Id == 0)
                _EmployeesData.Add(employee);
            else
                _EmployeesData.Update(employee);

            return RedirectToAction("Index");
        }
        #endregion

        #region Delete
        public IActionResult Delete(int id)
        {
            if (id <= 0) return BadRequest();

            var employee = _EmployeesData.Get(id);

            if (employee is null)
                return NotFound();

            return View(new EmployeeViewModel
            {
                Id = employee.Id,
                LastName = employee.LastName,
                Name = employee.FirstName,
                MiddleName = employee.Patronymic,
                Age = employee.Age,
            });
        }

        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            _EmployeesData.Delete(id);
            return RedirectToAction("Index");
        }
        #endregion


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
