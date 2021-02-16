using Microsoft.AspNetCore.Mvc;
using System.Linq;
using WebStore.Domain;
using WebStore_2021.Infrastructure.Interfaces;
using WebStore_2021.Infrastructure.Mapping;
using WebStore_2021.ViewModels;

namespace WebStore_2021.Controllers
{
    public class CatalogController : Controller
    {
        private IProductData _ProductData;

        public CatalogController(IProductData ProductData) => _ProductData = ProductData;

        public IActionResult Index(int? BrandId, int? SectionId)
        {
            var filter = new ProductFilter
            {
                BrandId = BrandId,
                SectionId = SectionId
            };

            var products = _ProductData.GetProducts(filter);

            return View(new CatalogViewModel
            {
                SectionId = SectionId,
                BrandId = BrandId,
                Products = products.OrderBy(p => p.Order).ToView()
            });
        }

        public IActionResult Details(int id)
        {
            var product = _ProductData.GetProductById(id);

            return View(product.ToView());
        }
    }
}
