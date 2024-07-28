using System;

namespace ProductManagement.Domain.Entities
{
    public class Product : EntityBase<int>
    {
        public string Description { get; set; }
        public bool Situation { get; set; }
        public DateTime ManufacturingDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public short SupplierId { get; set; }
        public Supplier? Supplier { get; set; }
    }
}
