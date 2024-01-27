using BusinessObjects.DTO.User;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Newtonsoft.Json;
using BusinessObjects.DTO.common;
using BusinessObjects.DTO;

namespace ClientWeb.Controllers
{
    public class LoginController : Controller
    {
        private readonly string _baseUrl = "https://localhost:7029/api/User";

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                return RedirectToAction("Index", new { Message = "Please input both email and password!" });
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
                    using (var response = await client.PostAsJsonAsync(_baseUrl + "/login", authRequest))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var responseJson = await response.Content.ReadAsStringAsync();
                            var successResponse = JsonConvert.DeserializeObject<ApiResponse<UserResponse>>(responseJson);

                            var user = successResponse.Data;

                            var claims = new List<Claim>
                                {
                                    new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                                    new Claim(ClaimTypes.Name, user.FirstName + user.LastName),
                                    new Claim("Role", user.RoleId.ToString())
                                };

                            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                            var authProperties = new AuthenticationProperties
                            {
                                AllowRefresh = true,
                                IsPersistent = true
                            };

                            await HttpContext.SignInAsync(
                                CookieAuthenticationDefaults.AuthenticationScheme,
                                new ClaimsPrincipal(claimsIdentity),
                                authProperties
                            );

                            return RedirectToAction("Index", "Home");
                        }

                        var errorResponseJson = await response.Content.ReadAsStringAsync();
                        var errorResponse = JsonConvert.DeserializeObject<ErrorResponse>(errorResponseJson);
                        return RedirectToAction("Index", new { Message = errorResponse.Message });
                    }
                }
                catch (HttpRequestException)
                {
                    return RedirectToAction("Index", new { Message = "Error connecting to service." });
                }
                catch (JsonException)
                {
                    return RedirectToAction("Index", new { Message = "Error parsing the service response." });
                }
            }
        }

        public async Task<IActionResult> SignOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index","Login");
        }


        
    }
}