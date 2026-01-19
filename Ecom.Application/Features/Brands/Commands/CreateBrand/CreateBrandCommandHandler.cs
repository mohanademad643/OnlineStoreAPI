using AutoMapper;
using Ecom.Application.DTOs;
using Ecom.Application.Interfaces;
using Ecom.Domain.Entities.Products;
using MediatR;

namespace Ecom.Application.Features.Brands.Commands.CreateBrand
{
    internal class CreateBrandCommandHandler : IRequestHandler<CreateBrandCommand, BrandDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CreateBrandCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<BrandDTO> Handle(CreateBrandCommand request, CancellationToken cancellationToken)
        {
            var BrandEntity = _mapper.Map<Brand>(request.BrandDTO);

            await _unitOfWork.BrandRepository.AddAsync(BrandEntity,cancellationToken);

            return _mapper.Map<BrandDTO>(BrandEntity);
        }
    }
}
