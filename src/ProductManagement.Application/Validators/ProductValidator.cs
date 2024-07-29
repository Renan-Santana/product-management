using FluentValidation;
using ProductManagement.Application.DTOs.Request;

namespace ProductManagement.Application.Validators
{
    public class ProductValidator : AbstractValidator<ProductRequestDto>
    {
        public ProductValidator()
        {
            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required.");

            RuleFor(x => x.ManufacturingDate)
                .LessThan(x => x.ExpirationDate).WithMessage("Manufacturing date must be less than expiration date.");
        }
    }
}
