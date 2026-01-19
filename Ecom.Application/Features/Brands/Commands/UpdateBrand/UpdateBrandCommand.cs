using Ecom.Application.DTOs;
using MediatR;
namespace Ecom.Application.Features.Brands.Commands.UpdateBrand
{

    public record UpdateBrandCommand(
      UpdateBrandDTO BrandDTO
  ) : IRequest<bool>;
}
