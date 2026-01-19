using FluentValidation;
namespace Ecom.Application.Features.Brands.Commands.UpdateBrand
{
    internal class UpdateBrandCommandValidator
   : AbstractValidator<UpdateBrandCommand>
    {
        public UpdateBrandCommandValidator()
        {
            RuleFor(x => x.BrandDTO.id)
                .GreaterThan(0).WithMessage("Invalid brand id");

            RuleFor(x => x.BrandDTO.Name)
                .NotEmpty().WithMessage("Name is required");

            RuleFor(x => x.BrandDTO.Description)
                .NotEmpty().WithMessage("Description is required");
        }
    }
}
