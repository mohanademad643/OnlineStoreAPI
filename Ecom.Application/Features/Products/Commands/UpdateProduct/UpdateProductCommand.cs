
using Ecom.Application.DTOs;
using MediatR;

namespace Ecom.Application.Features.Products.Commands.UpdateProduct
{
    public record UpdateProductCommand(UpdateProductDTO ProductDto) : IRequest<bool>;

}
