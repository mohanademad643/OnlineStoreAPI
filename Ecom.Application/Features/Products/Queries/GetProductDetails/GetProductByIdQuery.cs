

using Ecom.Application.DTOs;
using Ecom.Domain.Entities.Products;
using MediatR;
using System.Linq.Expressions;

namespace Ecom.Application.Features.Products.Queries.GetProductDetails
{
    public record GetProductByIdQuery(int Id, params Expression<Func<Product, object>>[] Includes) : IRequest<ProductDTO>
    {
       
    }

}
