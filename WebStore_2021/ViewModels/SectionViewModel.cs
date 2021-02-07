using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebStore_2021.ViewModels
{
    public record SectionViewModel
    {
        public int Id { get; init; }

        public string Name { get; init; }

        public int Order { get; init; }

        public SectionViewModel Parent { get; init; }

        public List<SectionViewModel> ChildSections { get; } = new();

        public int ProductsCount { get; set; }

        public int TotalProductsCount => ProductsCount + ChildSections.Sum(c => c.TotalProductsCount);
    }

}
