

namespace Ecom.Core.DTOs
{
    public record BrandDTO
     (string Name,
      string Description
      );
    public record UpdateBrandDTO
           (string Name,
     string Description,
     int id
     );
}
