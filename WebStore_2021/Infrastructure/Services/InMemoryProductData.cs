using System.Collections.Generic;
using WebStore.Domain.Entities;
using WebStore_2021.Data;
using WebStore_2021.Infrastructure.Interfaces;

namespace WebStore_2021.Infrastructure.Services
{
    public class InMemoryProductData : IProductData
    {
        public IEnumerable<Section> GetSections() => TestData.Sections;

        public IEnumerable<Brand> GetBrands() => TestData.Brands;
    }
}
