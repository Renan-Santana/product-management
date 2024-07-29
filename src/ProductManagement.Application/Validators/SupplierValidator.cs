using FluentValidation;
using ProductManagement.Application.DTOs.Request;

namespace ProductManagement.Application.Validators
{
    public class SupplierValidator : AbstractValidator<SupplierRequestDto>
    {
        public SupplierValidator()
        {
            RuleFor(x => x.Description)
               .NotEmpty().WithMessage("Description is required.");

            RuleFor(x => x.Cnpj)
               .NotEmpty().WithMessage("Cnpj is required.");
        }
    }
}
