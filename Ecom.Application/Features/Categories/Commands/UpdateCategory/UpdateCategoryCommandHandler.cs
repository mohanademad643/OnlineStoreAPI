using MediatR;
using AutoMapper;
using Ecom.Application.Interfaces;

namespace Ecom.Application.Features.Categories.Commands.UpdateCategory
{
    public class UpdateCategoryCommandHandler
        : IRequestHandler<UpdateCategoryCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateCategoryCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> Handle(
            UpdateCategoryCommand request,
            CancellationToken cancellationToken)
        {
            var category = await _unitOfWork.CategoryRepository
                .GetByIdAsync(request.categoryDTO.id, cancellationToken);

            if (category is null)
                return false;

            _mapper.Map(request.categoryDTO, category);

            await _unitOfWork.CategoryRepository.UpdateAsync(category, cancellationToken);

            return true;
        }
    }
}
