using System;

namespace ProductManagement.Application.DTOs.Response
{
    public class ProductResponseDto
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public bool Situation { get; set; }
        public DateTime ManufacturingDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public SupplierResponseDto Supplier { get; set; }
    }
}
