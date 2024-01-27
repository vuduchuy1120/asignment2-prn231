using BusinessObjects.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository;
using Repository.Services;

namespace BookStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        IUserRepository repository = new UserRepository();

        [HttpGet]
        public IActionResult GetUsers()
        {
            try
            {
                var list = repository.GetUsers();
                var response = list.Select(user => new UserResponse
                {
                    UserId = user.UserId,
                    Password = user.Password,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    MiddleName = user.MiddleName,
                    RoleId = user.RoleId,
                    PubId = user.PubId
                }).ToList();
                return Ok(new ApiResponse<object>("Get list successfull!", response));
            }
            catch (Exception e)
            {
                return BadRequest(new ApiResponse<object>(e.Message));
            }
        }
        [HttpPost]
        public IActionResult AddUser(UserRequest user)
        {
            try
            {
                repository.AddUser(user);
                return Ok(new ApiResponse<object>(user));
            }
            catch (Exception e)
            {
                return BadRequest(new ApiResponse<object>(e.Message));
            }
        }
        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, UpdateUserRequest user)
        {
            var updateUser = repository.GetUserByID(id);
            if (updateUser == null)
            {
                return NotFound(new ApiResponse<object>("User not found!"));
            }
            try
            {
                updateUser = repository.UpdateUser(id, user);

                // convert to response
                var response = new UserResponse
                {
                    UserId = updateUser.UserId,
                    Password = updateUser.Password,
                    Email = updateUser.Email,
                    FirstName = updateUser.FirstName,
                    LastName = updateUser.LastName,
                    MiddleName = updateUser.MiddleName,
                    RoleId = updateUser.RoleId
                };

                return Ok(new ApiResponse<object>("Get list successfull!", response));
            }
            catch (Exception e)
            {
                return BadRequest(new ApiResponse<object>(e.Message));
            }

        }
        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            var user = repository.GetUserByID(id);
            if (user == null)
            {
                return NotFound(new ApiResponse<object>("User not found!"));
            }
            try
            {
                repository.DeleteUser(id);
                return Ok(new ApiResponse<object>("Delete successfull!"));
            }
            catch (Exception e)
            {
                return BadRequest(new ApiResponse<object>(e.Message));
            }
        }
        [HttpGet("{id}")]
        public IActionResult GetUserById(int id)
        {
            var user = repository.GetUserByID(id);
            if (user == null)
            {
                return NotFound(new ApiResponse<object>("User not found!"));
            }
            try
            {
                var response = new UserResponse
                {
                    UserId = user.UserId,
                    Password = user.Password,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    MiddleName = user.MiddleName,
                    RoleId = user.RoleId
                };
                return Ok(new ApiResponse<object>("Get successfull!", response));
            }
            catch (Exception e)
            {
                return BadRequest(new ApiResponse<object>(e.Message));
            }
        }

    }
}
