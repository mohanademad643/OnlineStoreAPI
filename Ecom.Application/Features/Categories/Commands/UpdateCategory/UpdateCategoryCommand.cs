using MediatR;
using Ecom.Application.Features.Categories.Dto;

namespace Ecom.Application.Features.Categories.Commands.UpdateCategory
{
    public record UpdateCategoryCommand(
        UpdateCategoryDTO categoryDTO
    ) : IRequest<bool>;
}
