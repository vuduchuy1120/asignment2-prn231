using BusinessObjects.DTO;
using BusinessObjects.DTO.User;
using BusinessObjects.Models;
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

        [ApiExplorerSettings(IgnoreApi = true)]
        public UserResponse UserToDTO(User user)
        {
            return new UserResponse
            {
                UserId = user.UserId,
                Password = user.Password,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                MiddleName = user.MiddleName,
                Source = user.Source,
                RoleId = user.RoleId,
                RoleName = user.Role.RoleDesc,
                PubId = user.PubId
            };
        }

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
                    Source = user.Source,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    MiddleName = user.MiddleName,
                    RoleId = user.RoleId,
                    RoleName = user.Role.RoleDesc,

                    PubId = user.PubId,
                    HireDate = user.HireDate
                }).ToList();
                return Ok(new ApiResponse<object>("Get list successfull!", response));
            }
            catch (Exception e)
            {
                return BadRequest(new ApiResponse<object>(e.Message));
            }
        }
        [HttpPost]
        [Route("register")]
        public IActionResult AddUser(UserRequest userReq)
        {
            try
            {
                var user = new User
                {
                    Password = userReq.Password,
                    Email = userReq.Email,
                    RoleId = 1,

                    PubId = 1
                };
                 repository.RegisterUser(user);
                

                return Ok(new ApiResponse<object>(UserToDTO(user)));
            }
            catch (Exception e)
            {
                return BadRequest(new ApiResponse<object>(e.Message));
            }
        }

        [HttpPost]
        [Route("login")]
        public IActionResult Login(UserRequest loginReq)
        {
            try
            {
                if(repository.IsEmailWasUsed(loginReq.Email))
                {
                    var user = repository.GetUserByEmailAndPassword(loginReq.Email, loginReq.Password);
                    if (user == null)
                    {
                        return NotFound(new ApiResponse<object>("Wrong Password"));
                    }
                    return Ok(new ApiResponse<object>("Login successfull!", UserToDTO(user)));
                }
                else
                {
                    return NotFound(new ApiResponse<object>("User not found!"));
                }
               
            }
            catch (Exception e)
            {
                return BadRequest(new ApiResponse<object>(e.Message));
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, UpdateUserRequest userReq)
        {
            var updateUser = repository.GetUserByID(id);
            if (updateUser == null)
            {
                return NotFound(new ApiResponse<object>("User not found!"));
            }
            try
            {
                updateUser.Source = userReq.Source;
                updateUser.FirstName = userReq.FirstName;
                updateUser.LastName = userReq.LastName;
                updateUser.MiddleName = userReq.MiddleName;
                updateUser.PubId = userReq.PubId;                
                updateUser.HireDate = userReq.HireDate;

                repository.UpdateUser(updateUser);                
                return Ok(new ApiResponse<object>(UserToDTO(updateUser)));
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
                return Ok(new ApiResponse<object>("Get successfull!", UserToDTO(user)));
            }
            catch (Exception e)
            {
                return BadRequest(new ApiResponse<object>(e.Message));
            }
        }

    }
}
