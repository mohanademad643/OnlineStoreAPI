using Ecom.Application.DTOs;
using MediatR;
namespace Ecom.Application.Features.Brands.Queries.GetAllBrands
{
    public record GetAllBrandsQuery : IRequest<IReadOnlyList<UpdateBrandDTO>>;

}
