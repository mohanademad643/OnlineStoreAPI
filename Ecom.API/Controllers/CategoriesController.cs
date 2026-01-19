using AutoMapper;
using Ecom.API.Helper;
using Ecom.Application.Features.Categories.Commands.AddCategory;
using Ecom.Application.Features.Categories.Commands.DeleteCategory;
using Ecom.Application.Features.Categories.Commands.UpdateCategory;
using Ecom.Application.Features.Categories.Dto;
using Ecom.Application.Features.Categories.Queries.GetAllCategory;
using Ecom.Application.Features.Categories.Queries.GetCategoryById;
using Ecom.Application.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ecom.API.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    public class CategoriesController : BaseController
    {
        private readonly IMediator _mediator;



        public CategoriesController(IUnitOfWork unitOfWork, IMapper mapper, IMediator mediator) : base(unitOfWork, mapper)
        {
            _mediator = mediator;
        }

        //[HttpGet("Get-All")]
        //public async Task<IActionResult> get()
        //{
        //    try
        //    {
        //        var Category = await _unitOfWork.CategoryRepository.GetAllAsync();

        //        if (Category is null) {
        //            return BadRequest(new ResponseAPI(400)); 
        //        }
        //        return Ok(Category);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);

        //    }
        //}
        [HttpGet("Get-All")]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(new GetAllCategoriesQuery(),cancellationToken);
            return Ok(result);
        }

        //[HttpGet("Get-By-id/{id}")]
        //public async Task<IActionResult> getById(int id)
        //{
        //    try
        //    {
        //        var CategoryId = await _unitOfWork.CategoryRepository.GetByIdAsync(id);

        //        if (CategoryId is null) return  BadRequest(new ResponseAPI(400,$"No Found Category Id = {id}"));

        //        return Ok(CategoryId);

        //    }
        //    catch (Exception ex) 
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}
        [HttpGet("Get-By-Id/{id}")]
        public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken = default)
        {
            var category = await _mediator.Send(new GetCategoryByIdQuery(id),cancellationToken);

            if (category is null)
                return NotFound(new ResponseAPI(
                    StatusCodes.Status404NotFound,
                    $"Category with id '{id}' was not found"
                ));

            return Ok(category);
        }

        //[HttpPost("Add-Category")]
        //public async Task<IActionResult> AddCategory(CategoryDTO categoryDTO)
        //{
        //    try
        //    {
        //        var category = _mapper.Map<Category>(categoryDTO);

        //         await _unitOfWork.CategoryRepository.AddAsync(category);
        //        return Ok(new ResponseAPI(200,"Item Has Been Added Successfully"));
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}
        
        [HttpPost("Add-Category")]
        public async Task<IActionResult> Create(CategoryDTO category, CancellationToken cancellationToken)
        {
            var id = await _mediator.Send(new CreateCategoryCommand(category),cancellationToken);
            return Ok(new ResponseAPI(200,"Created Successfully"));
        }
        [HttpPut("Update-Category")]
        public async Task<IActionResult> UpdateCategory(
    UpdateCategoryDTO categoryDTO, CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(
                new UpdateCategoryCommand(categoryDTO),  cancellationToken
            );

            if (!result)
                return NotFound(new ResponseAPI(
                    404,
                    $"Category with id '{categoryDTO.id}' was not found"
                ));

            return Ok(new ResponseAPI(
               200,
                "Item has been updated successfully"
            ));
        }

        //[HttpPut("Update-Category")]
        //public async Task<IActionResult> UpdateCategory(UpdateCategoryDTO categoryDTO)
        //{
        //    try
        //    {
        //        var category = _mapper.Map<Category>(categoryDTO);

        //        await _unitOfWork.CategoryRepository.UpdateAsync(category);

        //        return Ok(new ResponseAPI(200, "Item Has Been Updated Successfully"));

        //    }
        //    catch (Exception ex)
        //    {

        //        return BadRequest(ex.Message);
        //    }
        //}
        //[HttpDelete("Delete-Category/{id}")]
        //public async Task<IActionResult> DeleteById(int id)
        //{
        //    try
        //    {
        //        await _unitOfWork.CategoryRepository.DeleteAsync(id);

        //        return Ok(new ResponseAPI(200, "Item Has Been Deleted Successfully"));

        //    }
        //    catch(Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}
        [HttpDelete("Delete-Category/{id}")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(new DeleteCategoryCommand(id),cancellationToken);

            if (!result)
                return NotFound(new ResponseAPI(404, $"Category with Id {id} not found" ));

            return Ok(new ResponseAPI(200, "Item Has Been Deleted Successfully"));
        }

    }
}
