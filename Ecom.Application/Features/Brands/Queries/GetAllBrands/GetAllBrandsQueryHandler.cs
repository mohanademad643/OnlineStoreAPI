using AutoMapper;
using Ecom.Application.DTOs;
using Ecom.Application.Interfaces;
using MediatR;

namespace Ecom.Application.Features.Brands.Queries.GetAllBrands
{
    internal class GetAllBrandsQueryHandler
        : IRequestHandler<GetAllBrandsQuery, IReadOnlyList<UpdateBrandDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetAllBrandsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IReadOnlyList<UpdateBrandDTO>> Handle(GetAllBrandsQuery request, CancellationToken cancellationToken)
        {
            var Brand = await _unitOfWork.BrandRepository.GetAllAsync(cancellationToken);
            var maping = _mapper.Map<IReadOnlyList<UpdateBrandDTO>>(Brand);
            return maping;
        }
    }
}
