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
                PubId = user.PubId,
                HireDate = user.HireDate
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

                var newUser = repository.GetUserByEmailAndPassword(userReq.Email, userReq.Password);
                return Ok(new ApiResponse<object>(UserToDTO(newUser)));
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
                if (repository.IsEmailWasUsed(loginReq.Email))
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
            updateUser.Pub = null;
            try
            {
                updateUser.Source = userReq.Source;
                updateUser.FirstName = userReq.FirstName;
                updateUser.LastName = userReq.LastName;
                updateUser.MiddleName = userReq.MiddleName;
                updateUser.PubId = userReq.PubId;

                updateUser.HireDate = userReq.HireDate;

                repository.UpdateUser(updateUser);
                var user1 = repository.GetUserByID(id);

                return Ok(new ApiResponse<object>(UserToDTO(user1)));
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
        // Admin create User
        [HttpPost("admin/create")]
        public IActionResult AdminCreateUser(AdminCreateUserRequest userReq)
        {
            try
            {
                var user = new User
                {
                    Password = userReq.Password,
                    Email = userReq.EmailAddress,
                    RoleId = userReq.RoleId,
                    PubId = userReq.PubId,
                    Source = userReq.Source,
                    FirstName = userReq.FirstName,
                    LastName = userReq.LastName,
                    MiddleName = userReq.MiddleName,
                    HireDate = userReq.HireDate
                };
                repository.RegisterUser(user);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(new ApiResponse<object>(e.Message));
            }
        }

        // Admin update User
        [HttpPut("admin/update/{id}")]
        public IActionResult AdminUpdateUser(int id, UserUpdateProfile userReq)
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
                updateUser.RoleId = userReq.RoleId;
                updateUser.Role = null;
                updateUser.MiddleName = userReq.MiddleName;
                updateUser.PubId = userReq.PubId;
                updateUser.HireDate = userReq.HireDate;
                repository.UpdateUser(updateUser);

                var user = repository.GetUserByID(id);


                return Ok(new ApiResponse<object>("update succesfull", UserToDTO(user)));
            }
            catch (Exception e)
            {
                return BadRequest(new ApiResponse<object>(e.Message));
            }

        }

        [HttpPut("updateProfile/{id}")]
        public IActionResult UpdateProfile(int id, UpdateProfileRequest userReq)
        {
            var updateUser = repository.GetUserByID(id);
            if (updateUser == null)
            {
                return NotFound(new ApiResponse<object>("User not found!"));
            }
            updateUser.Pub = null;
            try
            {
                updateUser.FirstName = userReq.FirstName;
                updateUser.LastName = userReq.LastName;
                updateUser.MiddleName = userReq.MiddleName;
                updateUser.HireDate = userReq.HireDate;
                updateUser.PubId = userReq.pubID;

                repository.UpdateUser(updateUser);

                var user = repository.GetUserByID(id);
                return Ok(new ApiResponse<object>("update succesfull", UserToDTO(user)));
            }
            catch (Exception e)
            {
                return BadRequest(new ApiResponse<object>(e.Message));
            }
        }


    }
}
