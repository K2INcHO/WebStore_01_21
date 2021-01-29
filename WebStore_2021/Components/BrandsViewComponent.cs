using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using WebStore_2021.Infrastructure.Interfaces;
using WebStore_2021.ViewModels;

namespace WebStore_2021.Components
{
    public class BrandsViewComponent : ViewComponent
    {
        private readonly IProductData _ProductData;

        public BrandsViewComponent(IProductData ProductData) => _ProductData = ProductData;

        public IViewComponentResult Invoke() => View(GetBrands());

        private IEnumerable<BrandsViewModel> GetBrands() =>
            _ProductData.GetBrands()
                .OrderBy(brand => brand.Order)
                .Select(brand => new BrandsViewModel
                {
                    Id = brand.Id,
                    Name = brand.Name
                });
    }
}
