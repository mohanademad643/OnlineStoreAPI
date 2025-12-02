using Ecom.Application.DTOs;
using MediatR;

namespace Ecom.Application.Features.Products.Commands.CreateProduct
{
    public record AddProductCommand(AddProductDTO ProductDto) : IRequest<bool>;

}
