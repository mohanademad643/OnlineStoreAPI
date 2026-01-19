using Ecom.Application.Features.Categories.Dto;
using MediatR;

namespace Ecom.Application.Features.Categories.Commands.AddCategory
{
    public record CreateCategoryCommand(CategoryDTO categoryDTO): IRequest<CategoryDTO>;
    
}
