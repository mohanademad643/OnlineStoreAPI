using AutoMapper;
using Ecom.Application.DTOs;
using Ecom.Domain.Entities.Products;

namespace Ecom.Application.Mapping
{
    public class CategoryMapping:Profile
    {
        public CategoryMapping()
        {
            CreateMap<CategoryDTO,Category>().ReverseMap();
            CreateMap<UpdateCategoryDTO,Category>().ReverseMap();
        }
    }
}
