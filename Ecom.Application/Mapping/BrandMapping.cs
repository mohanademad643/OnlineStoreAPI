using AutoMapper;
using Ecom.Application.DTOs;
using Ecom.Domain.Entities.Products;


namespace Ecom.Application.Mapping
{
    public class BrandMapping : Profile
    {
        public BrandMapping()
        {
            CreateMap<BrandDTO, Brand>().ReverseMap();
            CreateMap<UpdateBrandDTO, Brand>().ReverseMap();
        }
    }
}
