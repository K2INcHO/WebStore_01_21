using System.Collections.Generic;
using WebStore_2021.Models;

namespace WebStore_2021.Data
{
    public static class TestData
    {
        public static List<Employee> Employees { get; } = new()
        {
            new Employee { Id = 1, LastName = "Иванов", FirstName = "Иван", Patronymic = "Иванович", Age = 37, DateOfBirth = "16.06.1983" },
            new Employee { Id = 2, LastName = "Петров", FirstName = "Петр", Patronymic = "Петрович", Age = 25, DateOfBirth = "11.03.1995" },
            new Employee { Id = 3, LastName = "Сидоров", FirstName = "Сидор", Patronymic = "Сидорович", Age = 27, DateOfBirth = "14.11.1993" },
        };
    }
}
