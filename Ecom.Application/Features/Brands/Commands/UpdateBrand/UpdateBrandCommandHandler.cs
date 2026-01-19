

using AutoMapper;
using Ecom.Application.Interfaces;
using MediatR;

namespace Ecom.Application.Features.Brands.Commands.UpdateBrand
{
    internal class UpdateBrandCommandHandler
       : IRequestHandler<UpdateBrandCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateBrandCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> Handle(UpdateBrandCommand request, CancellationToken cancellationToken)
        {
            var brand = await _unitOfWork.BrandRepository
               .GetByIdAsync(request.BrandDTO.id, cancellationToken);

            if (brand is null)
                return false;

            _mapper.Map(request.BrandDTO, brand);

            await _unitOfWork.BrandRepository.UpdateAsync(brand, cancellationToken);

            return true;
        }
    }
}
