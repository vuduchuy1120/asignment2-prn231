using BusinessObjects.DTO;
using BusinessObjects.DTO.common;
using BusinessObjects.DTO.User;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ClientWeb.Controllers
{
    public class RegisterController : Controller
    {
        private readonly string baseAuthUrl = "https://localhost:7029/api/User";
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(string email, string password, string confirmPassword)
        {
            // check user adready login 
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home", new { Message = "You are already logged in!" });
            }

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                return RedirectToAction("Register/Index", new { Message = "Please input both email and password" });
            }

            if (password != confirmPassword)
            {
                return RedirectToAction("Register/Index", new { Message = "Password and confirm password must be the same" });
            }

            var authRequest = new UserRequest
            {
                Email = email,
                Password = password
            };

            using (var client = new HttpClient())
            {
                try
                {
                    using (var response = await client.PostAsJsonAsync(baseAuthUrl + "/register", authRequest))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var responseJson = await response.Content.ReadAsStringAsync();
                            var successResponse = JsonConvert.DeserializeObject<ApiResponse<object>>(responseJson);
                            // pass by login
                            if (successResponse != null)
                            {
                                   return RedirectToAction("Login/Index", new { Message = successResponse.Message });
                            }
                            //return RedirectToAction("Index","Home", new { Message = successResponse.Message });
                        }

                        var errorResponseJson = await response.Content.ReadAsStringAsync();
                        var errorResponse = JsonConvert.DeserializeObject<ErrorResponse>(errorResponseJson);
                        return RedirectToAction("Register/Index", new { Message = errorResponse.Message });
                    }
                }
                catch (HttpRequestException)
                {
                    return RedirectToAction("Register/Index", new { Message = "Error connecting to service." });
                }
                catch (JsonException)
                {
                    return RedirectToAction("Register/Index", new { Message = "Error parsing the service response." });
                }
            }
        }
    }
}
