using System.Collections.Generic;
using WebStore.Domain.Entities;

namespace WebStore_2021.Infrastructure.Interfaces
{
    public interface IProductData
    {
        IEnumerable<Section> GetSections();

        IEnumerable<Brand> GetBrands(); 
    }
}
