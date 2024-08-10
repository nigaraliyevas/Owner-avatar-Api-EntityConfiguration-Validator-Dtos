using Api_EntityConfiguration_Validator_Dtos.Apps.AdminApp.Dtos.ProductDto;
using FluentValidation;

namespace Api_EntityConfiguration_Validator_Dtos.Apps.AdminApp.Validators.ProductValidator
{
    public class ProductCreateDtoValidator : AbstractValidator<ProductCreateDto>
    {
        public ProductCreateDtoValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("can’t be empty")
                .MaximumLength(50)
                .WithMessage("maxlength 50");

            RuleFor(s => s.SalePrice)
                .NotEmpty().WithMessage("can't be empty")
                .GreaterThan(100)
                .WithMessage("SalePrice must be greater than 100");

            RuleFor(s => s.CostPrice)
                .NotEmpty().WithMessage("can't be empty")
                .GreaterThan(100)
                .WithMessage("CostPrice must be greater than 100");

            RuleFor(p => p)
                .Custom((p, message) =>
                {
                    if (p.SalePrice < p.CostPrice)
                    {
                        message.AddFailure("CostPrice", "Cannot be less that SalePrice");
                    }
                });

        }
    }
}
