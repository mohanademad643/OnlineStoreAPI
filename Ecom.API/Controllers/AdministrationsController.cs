using Ecom.Application.DTO.Adminstration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Ecom.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AdministrationsController : ControllerBase
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public AdministrationsController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }


        [HttpGet("GetAll-Roles")]
        public async Task<IActionResult> get()
        {
            var roles = _roleManager.Roles;
            return Ok(roles);
        }
        [HttpPost("Add-Role")]
        public async Task<IActionResult> Add([FromBody] AddRoleDto role )
        {
            if (role == null || !ModelState.IsValid)
            {
                return BadRequest("Can't Add Role");
            }
            IdentityRole identityRole = new() { Name = role.RoleName };

            var result = await _roleManager.CreateAsync(identityRole);
            if (!result.Succeeded) {

                var errors = result.Errors.Select(e => e.Description);

                return BadRequest(new AddRoleResponseDto { Errors = errors });
            
            }

            return StatusCode(201);
        }

        [HttpPut("Update-Role")]
        public async Task<IActionResult> Update([FromBody] RolesDto Currole)
        {
            var role = await _roleManager.FindByIdAsync(Currole.Id);
            if (role == null)
            {
                return BadRequest(" Role Not Exsist");
            }
            role.Name = Currole.RoleName;
            var result = await _roleManager.UpdateAsync(role);
            if (!result.Succeeded)
            {

                var errors = result.Errors.Select(e => e.Description);

                return BadRequest(new AddRoleResponseDto { Errors = errors });

            }

            return StatusCode(201);
        }
        [HttpDelete("Delete-Role")]

        public async Task<IActionResult> Delete(string RoleId)
        {
            var role = await _roleManager.FindByIdAsync(RoleId);
            if (role == null)
            {
                return BadRequest(" Role Not Exsist");
            }
            var result = await _roleManager.DeleteAsync(role);
            if (!result.Succeeded)
            {

                var errors = result.Errors.Select(e => e.Description);

                return BadRequest(new AddRoleResponseDto { Errors = errors });

            }
            return StatusCode(202);
        }
    }
}
