using BusinessObjects.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository;
using Repository.Services;

namespace BookStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleRepository roleRepository = new RoleRepository();

        // get all roles
        [HttpGet]
        public IActionResult GetAllRole()
        {
            try
            {
                var roles = roleRepository.getAllRole();
                var roleResponse = roles.Select(role => new BusinessObjects.DTO.RoleResponse
                {
                    RoleId = role.RoleId,
                    RoleName = role.RoleDesc
                });
                return Ok(new ApiResponse<object>("Get list successfull!", roleResponse));

            }catch(Exception e)
            {
                return BadRequest(new ApiResponse<object>(e.Message));
            }
        }
       
    }
}
