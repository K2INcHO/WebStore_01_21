using WebStore.Domain.Entities.Base;

namespace WebStore.Domain.Entities
{
    public class Brand : NamedEntity
    {
        public int Order { get; set; }
    }
}
