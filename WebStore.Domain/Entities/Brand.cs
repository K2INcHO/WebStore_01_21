using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using WebStore.Domain.Entities.Base;

namespace WebStore.Domain.Entities
{
    //[Table("Brands")]
    public class Brand : NamedEntity
    {
        //[Column("BrandOrder")]
        public int Order { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
