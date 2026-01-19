using Ecom.Application.Features.Categories.Commands.UpdateCategory;
using FluentValidation;

namespace Ecom.Application.Features.Categories.Validators
{
    public class UpdateCategoryCommandValidator
        : AbstractValidator<UpdateCategoryCommand>
    {
        public UpdateCategoryCommandValidator()
        {
            RuleFor(x => x.categoryDTO.id)
                .GreaterThan(0).WithMessage("Invalid category id");

            RuleFor(x => x.categoryDTO.Name)
                .NotEmpty().WithMessage("Name is required");

            RuleFor(x => x.categoryDTO.Description)
                .NotEmpty().WithMessage("Description is required");
        }
    }
}
