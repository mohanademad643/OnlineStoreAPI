
using AutoMapper;
using Ecom.Application.Features.Categories.Dto;
using Ecom.Application.Interfaces;
using Ecom.Domain.Entities.Products;
using MediatR;

namespace Ecom.Application.Features.Categories.Commands.AddCategory
{
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, CategoryDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateCategoryCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CategoryDTO> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var categoryEntity = _mapper.Map<Category>(request.categoryDTO);

            await _unitOfWork.CategoryRepository.AddAsync(categoryEntity, cancellationToken);

            return _mapper.Map<CategoryDTO>(categoryEntity);
        }
    }
}
