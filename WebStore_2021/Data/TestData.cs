using System.Collections.Generic;
using WebStore.Domain.Entities;
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

        public static IEnumerable<Section> Sections { get; } = new[]
        {
            new Section { Id = 1, Name = "Спорт", Order = 0 },
            new Section { Id = 2, Name = "Nike", Order = 0, ParentId = 1 },
            new Section { Id = 3, Name = "Under Armour", Order = 1, ParentId = 1 },
            new Section { Id = 4, Name = "Adidas", Order = 2, ParentId = 1 },
            new Section { Id = 5, Name = "Puma", Order = 3, ParentId = 1 },
            new Section { Id = 6, Name = "ASICS", Order = 4, ParentId = 1 },
            new Section { Id = 7, Name = "Для мужчин", Order = 1 },
            new Section { Id = 8, Name = "Fendi", Order = 0, ParentId = 7 },
            new Section { Id = 9, Name = "Guess", Order = 1, ParentId = 7 },
            new Section { Id = 10, Name = "Valentino", Order = 2, ParentId = 7 },
            new Section { Id = 11, Name = "Диор", Order = 3, ParentId = 7 }
        };

        public static IEnumerable<Brand> Brands { get; } = new[]
        {
            new Brand { Id = 1, Name = "Acne", Order = 0},
            new Brand { Id = 2, Name = "Grune Erde", Order = 1},
            new Brand { Id = 3, Name = "Albiro", Order = 2},
            new Brand { Id = 4, Name = "Ronhill", Order = 3},
            new Brand { Id = 5, Name = "Oddmolly", Order = 4},
            new Brand { Id = 6, Name = "Boudestijn", Order = 5},
            new Brand { Id = 7, Name = "Rosch creative culture", Order = 6},
        };
    }
}
