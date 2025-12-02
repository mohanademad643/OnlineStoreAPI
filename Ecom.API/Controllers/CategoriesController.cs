using AutoMapper;
using Ecom.API.Helper;
using Ecom.Application.DTOs;
using Ecom.Application.Interfaces;
using Ecom.Domain.Entities.Products;
using Microsoft.AspNetCore.Mvc;

namespace Ecom.API.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    public class CategoriesController : BaseController
    {
        public CategoriesController(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        [HttpGet("Get-All")]
        public async Task<IActionResult> get()
        {
            try
            {
                var Category = await _unitOfWork.CategoryRepository.GetAllAsync();

                if (Category is null) {
                    return BadRequest(new ResponseAPI(400)); 
                }
                return Ok(Category);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }

        [HttpGet("Get-By-id/{id}")]
        public async Task<IActionResult> getById(int id)
        {
            try
            {
                var CategoryId = await _unitOfWork.CategoryRepository.GetByIdAsync(id);

                if (CategoryId is null) return  BadRequest(new ResponseAPI(400,$"No Found Category Id = {id}"));

                return Ok(CategoryId);

            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Add-Category")]
        public async Task<IActionResult> AddCategory(CategoryDTO categoryDTO)
        {
            try
            {
                var category = _mapper.Map<Category>(categoryDTO);

                 await _unitOfWork.CategoryRepository.AddAsync(category);
                return Ok(new ResponseAPI(200,"Item Has Been Added Successfully"));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("Update-Category")]
        public async Task<IActionResult> UpdateCategory(UpdateCategoryDTO categoryDTO)
        {
            try
            {
                var category = _mapper.Map<Category>(categoryDTO);

                await _unitOfWork.CategoryRepository.UpdateAsync(category);

                return Ok(new ResponseAPI(200, "Item Has Been Updated Successfully"));

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("Delete-Category/{id}")]
        public async Task<IActionResult> DeleteById(int id)
        {
            try
            {
                await _unitOfWork.CategoryRepository.DeleteAsync(id);

                return Ok(new ResponseAPI(200, "Item Has Been Deleted Successfully"));

            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
