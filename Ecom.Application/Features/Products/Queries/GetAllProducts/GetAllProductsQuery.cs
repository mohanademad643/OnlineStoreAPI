using Ecom.Application.DTOs;
using Ecom.Application.Sharing;
using MediatR;

namespace Ecom.Application.Features.Products.Queries.GetAllProducts
{
    public record GetAllProductsQuery(ProductParams Params) : IRequest<ReturnProductDTO>;
}
