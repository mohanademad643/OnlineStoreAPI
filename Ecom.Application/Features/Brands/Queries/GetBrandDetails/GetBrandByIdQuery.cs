using Ecom.Application.DTOs;
using MediatR;

namespace Ecom.Application.Features.Brands.Queries.GetBrandDetails
{

    public record GetBrandByIdQuery(int Id) : IRequest<BrandDTO>;
}
