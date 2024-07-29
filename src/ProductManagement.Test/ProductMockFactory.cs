using FluentValidation.Results;
using ProductManagement.Application.DTOs.Request;
using ProductManagement.Application.DTOs.Response;
using ProductManagement.Domain.Entities;
using System.Collections.Generic;
using System.Linq;


namespace ProductManagement.Test
{
    internal static class ProductMockFactory
    {
        internal static int productId { get; set; } = 1;
        internal static Product emptyProduct { get; set; } = new Product();
        internal static ValidationResult emptyValidationResult { get; set; } = new ValidationResult();
        internal static ProductRequestDto emptyProductRequestDto { get; set; } = new ProductRequestDto();
        internal static ProductResponseDto emptyProductResponseDto { get; set; } = new ProductResponseDto();
        internal static ProductFilterRequestDto emptyProductFilterRequestDto { get; set; } = new ProductFilterRequestDto { PageNumber = 1, PageSize = 10 };
        internal static IEnumerable<Product> emptyProductList { get; set; } = Enumerable.Empty<Product>();
        internal static IEnumerable<Product> validProductList { get; set; } = Enumerable.Repeat(emptyProduct, 1);
        internal static List<ProductResponseDto> validProductResponseDtoList { get; set; } = new List<ProductResponseDto> { emptyProductResponseDto };


        internal static ValidationResult GetValidationResult()
        {
            var validationResult = new ValidationResult(new List<ValidationFailure>
            {
                new ValidationFailure("Field", "Validation failed")
            });

            return validationResult;
        }
    }
}
