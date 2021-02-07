using System.Collections.Generic;

namespace WebStore_2021.ViewModels
{
    public record CatalogViewModel
    {
        public IEnumerable<ProductViewModel> Products { get; init; }

        public int? SectionId { get; init; }

        public int? BrandId { get; set; }
    }
}
