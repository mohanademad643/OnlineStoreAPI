namespace Ecom.Application.Features.Categories.Dto
{
    public record CategoryDTO
    (string Name ,
     string Description
     );

    public record UpdateCategoryDTO
           (string Name,
     string Description,
     int id
     );
}
