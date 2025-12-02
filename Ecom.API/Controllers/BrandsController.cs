using AutoMapper;
using Ecom.API.Helper;
using Ecom.Application.DTOs;
using Ecom.Application.Interfaces;
using Ecom.Domain.Entities.Products;
using Microsoft.AspNetCore.Mvc;


namespace Ecom.API.Controllers
{

    public class BrandsController : BaseController
    {
        public BrandsController(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }
        [HttpGet("Get-All")]
        public async Task<IActionResult> get()
        {
            try
            {
                var Brand = await _unitOfWork.BrandRepository.GetAllAsync();

                if (Brand is null)
                {
                    return BadRequest(new ResponseAPI(400));
                }
                return Ok(Brand);
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
                var BrandId = await _unitOfWork.BrandRepository.GetByIdAsync(id);

                if (BrandId is null) return BadRequest(new ResponseAPI(400, $"No Found Brand Id = {id}"));

                return Ok(BrandId);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Add-Brand")]
        public async Task<IActionResult> AddBrand(BrandDTO brandDTO)
        {
            try
            {
                var brand = _mapper.Map<Brand>(brandDTO);

                await _unitOfWork.BrandRepository.AddAsync(brand);
                return Ok(new ResponseAPI(200, "Item Has Been Added Successfully"));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("Update-Brand")]
        public async Task<IActionResult> UpdateBrand(UpdateBrandDTO updateBrandDTO)
        {
            try
            {
                var brand = _mapper.Map<Brand>(updateBrandDTO);

                await _unitOfWork.BrandRepository.UpdateAsync(brand);

                return Ok(new ResponseAPI(200, "Item Has Been Updated Successfully"));

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("Delete-Brand/{id}")]
        public async Task<IActionResult> DeleteById(int id)
        {
            try
            {
                await _unitOfWork.BrandRepository.DeleteAsync(id);

                return Ok(new ResponseAPI(200, "Item Has Been Deleted Successfully"));

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
