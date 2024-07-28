using System.Collections.Generic;

namespace ProductManagement.Domain.Entities
{
    public class Supplier : EntityBase<short>
    {
        public string Description { get; set; }
        public string Cnpj { get; set; }
        public ICollection<Product> Products { get; set; } = new List<Product>();

    }
}
