using AutoMapper;
using Ecom.API.Helper;
using Ecom.Application.DTOs;
using Ecom.Application.Features.Brands.Commands.CreateBrand;
using Ecom.Application.Features.Brands.Commands.DeleteBrand;
using Ecom.Application.Features.Brands.Commands.UpdateBrand;
using Ecom.Application.Features.Brands.Queries.GetAllBrands;
using Ecom.Application.Features.Categories.Queries.GetCategoryById;
using Ecom.Application.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ecom.API.Controllers
{

    public class BrandsController : BaseController
    {
        private readonly IMediator _mediator;



        public BrandsController(IUnitOfWork unitOfWork, IMapper mapper, IMediator mediator) : base(unitOfWork, mapper)
        {
            _mediator = mediator;
        }

        //[HttpGet("Get-All")]
        //public async Task<IActionResult> get()
        //{
        //    try
        //    {
        //        var Brand = await _unitOfWork.BrandRepository.GetAllAsync();

        //        if (Brand is null)
        //        {
        //            return BadRequest(new ResponseAPI(400));
        //        }
        //        return Ok(Brand);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);

        //    }
        //}

        [HttpGet("Get-All")]
        public async Task<IActionResult> get(CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(new GetAllBrandsQuery(),cancellationToken);
            return Ok(result);
        }
        //[HttpGet("Get-By-id/{id}")]
        //public async Task<IActionResult> getById(int id)
        //{
        //    try
        //    {
        //        var BrandId = await _unitOfWork.BrandRepository.GetByIdAsync(id);

        //        if (BrandId is null) return BadRequest(new ResponseAPI(400, $"No Found Brand Id = {id}"));

        //        return Ok(BrandId);

        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

        [HttpGet("Get-By-id/{id}")]
        public async Task<IActionResult> getById(int id, CancellationToken cancellationToken = default)
        {
            var Brand = await _mediator.Send(new GetCategoryByIdQuery(id),cancellationToken);

            if (Brand is null)
                return NotFound(new ResponseAPI(
                    StatusCodes.Status404NotFound,
                    $"Brand with id '{id}' was not found"
                ));

            return Ok(Brand);
        }

        //[HttpPost("Add-Brand")]
        //public async Task<IActionResult> AddBrand(BrandDTO brandDTO)
        //{
        //    try
        //    {
        //        var brand = _mapper.Map<Brand>(brandDTO);

        //        await _unitOfWork.BrandRepository.AddAsync(brand);
        //        return Ok(new ResponseAPI(200, "Item Has Been Added Successfully"));
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}
        [HttpPost("Add-Brand")]
        public async Task<IActionResult> AddBrand(BrandDTO brandDTO, CancellationToken cancellationToken = default)
        {
             await _mediator.Send(new CreateBrandCommand(brandDTO),cancellationToken);
            return Ok(new ResponseAPI(200, "Created Successfully"));
        }
        //[HttpPut("Update-Brand")]
        //public async Task<IActionResult> UpdateBrand(UpdateBrandDTO updateBrandDTO)
        //{
        //    try
        //    {
        //        var brand = _mapper.Map<Brand>(updateBrandDTO);

        //        await _unitOfWork.BrandRepository.UpdateAsync(brand);

        //        return Ok(new ResponseAPI(200, "Item Has Been Updated Successfully"));

        //    }
        //    catch (Exception ex)
        //    {

        //        return BadRequest(ex.Message);
        //    }
        //}
        [HttpPut("Update-Brand")]
        public async Task<IActionResult> UpdateBrand(UpdateBrandDTO updateBrandDTO, CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(
               new UpdateBrandCommand(updateBrandDTO),cancellationToken);

            if (!result)
                return NotFound(new ResponseAPI(
                    404,
                    $"Category with id '{updateBrandDTO.id}' was not found"
                ));

            return Ok(new ResponseAPI(
               200,
                "Item has been updated successfully"
            ));
        }

        //[HttpDelete("Delete-Brand/{id}")]
        //public async Task<IActionResult> DeleteById(int id)
        //{
        //    try
        //    {
        //        await _unitOfWork.BrandRepository.DeleteAsync(id);

        //        return Ok(new ResponseAPI(200, "Item Has Been Deleted Successfully"));

        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}
        [HttpDelete("Delete-Brand/{id}")]
        public async Task<IActionResult> DeleteById(int id, CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(new DeleteBrandCommand(id), cancellationToken);

            if (!result)
                return NotFound(new ResponseAPI(404, $"Brand with Id {id} not found"));

            return Ok(new ResponseAPI(200, "Item Has Been Deleted Successfully"));
        }
    }
}
