using BusinessObjects.DTO;
using BusinessObjects.DTO.User;
using BusinessObjects.Models;
using ClientWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Security.Claims;

namespace ClientWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly string _baseUrl = "https://localhost:7029/api/User";

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> IndexAsync()
        {
            //get user info
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            using(var client = new HttpClient())
            {
                try
                {
                    using (var response = await client.GetAsync(_baseUrl + "/" + userId))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var responseJson = await response.Content.ReadAsStringAsync();
                            var successResponse = JsonConvert.DeserializeObject<ApiResponse<UserResponse>>(responseJson);
                            var user = successResponse.Data;


                            var publisherResponse = await client.GetAsync("https://localhost:7029/api/Publisher");
                            var publisherResponseJson = await publisherResponse.Content.ReadAsStringAsync();
                            var publisherSuccessResponse = JsonConvert.DeserializeObject<ApiResponse<List<PublisherResponse>>>(publisherResponseJson);
                            var publisherList = publisherSuccessResponse.Data.ToList();

                            ViewData["pubID"] = new SelectList(publisherList, "PublisherId", "PublisherName");

                            return View(user);
                        }
                        else
                        {
                            return RedirectToAction("Index", "Login");
                        }
                    }
                }
                catch (Exception ex)
                {
                    return RedirectToAction("Index", "Login");
                }
            }
            

        }

        // update Profile
        [HttpPost]
        public async Task<IActionResult> Update(string? firstname,
                     string? middlename, string? lastName, int publisher, DateTime? hireDate)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            Console.WriteLine("User ID: " + userId);
            var user = new UpdateProfileRequest()
            {
                FirstName = firstname,
                MiddleName = middlename,
                LastName = lastName,
                HireDate = hireDate,
                pubID = publisher

            };

            using (var client = new HttpClient())
            {
                try
                {
                    using (var response = await client.PutAsJsonAsync(_baseUrl + "/updateProfile/" + userId, user))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var responseJson = await response.Content.ReadAsStringAsync();
                            var successResponse = JsonConvert.DeserializeObject<ApiResponse<UserResponse>>(responseJson);

                            var userResponse = successResponse.Data;

                            return RedirectToAction("Index", new {Messeage = "Update Successfull!"});
                        }
                        else
                        {
                            return RedirectToAction("Index", "Login");
                        }
                    }
                }
                catch (Exception ex)
                {
                    return RedirectToAction("Index", "Login");
                }
            }
        }   

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
