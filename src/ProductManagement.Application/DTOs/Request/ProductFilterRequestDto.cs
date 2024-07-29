using System;

namespace ProductManagement.Application.DTOs.Request
{
    public class ProductFilterRequestDto
    {
        public string? Description { get; set; }
        public bool? Situation { get; set; }
        public DateTime? ManufacturingDate { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public short? SupplierId { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
