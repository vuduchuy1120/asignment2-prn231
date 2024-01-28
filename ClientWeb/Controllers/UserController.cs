using BusinessObjects.DTO;
using BusinessObjects.DTO.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text.Json;

namespace ClientWeb.Controllers
{
    public class UserController : Controller
    {
        private readonly string _baseUrl = "https://localhost:7029/api/User";
        private readonly string roleUrl = "https://localhost:7029/api/Role";
        private readonly string publisherUrl = "https://localhost:7029/api/Publisher";
        public async Task<IActionResult> IndexAsync()
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(_baseUrl);

                var responseJson = await response.Content.ReadAsStringAsync();

                var successResponse = JsonConvert.DeserializeObject<ApiResponse<List<UserResponse>>>(responseJson);

                var list = successResponse.Data;

                // Get Roles
                var roleResponse = await client.GetAsync(roleUrl);
                var roleResponseJson = await roleResponse.Content.ReadAsStringAsync();
                var roleSuccessResponse = JsonConvert.DeserializeObject<ApiResponse<List<RoleResponse>>>(roleResponseJson);
                var roleList = roleSuccessResponse.Data.ToList();
                ViewData["RoleID"] = new SelectList(roleList, "RoleId", "RoleName");
                // get publishers
                var publisherResponse = await client.GetAsync(publisherUrl);
                var publisherResponseJson = await publisherResponse.Content.ReadAsStringAsync();
                var publisherSuccessResponse = JsonConvert.DeserializeObject<ApiResponse<List<PublisherResponse>>>(publisherResponseJson);
                var listPublisher = publisherSuccessResponse.Data.ToList();
                ViewData["PubID"] = new SelectList(listPublisher, "PublisherId", "PublisherName");

                return View(list);
            }

        }

        [HttpPost]
        public async Task<IActionResult> Add(string email, string password, string? firstname,
          string? middlename, string? lastName, string? source, int role, int pubId, DateTime? hireDate)
        {
            var user = new AdminCreateUserRequest()
            {
                EmailAddress = email,
                Password = password,
                FirstName = firstname,
                MiddleName = middlename,
                LastName = lastName,
                Source = source,
                RoleId = role,
                PubId = pubId,
                HireDate = hireDate
            };

            try
            {
                using (var client = new HttpClient())
                {
                    var response = await client.PostAsJsonAsync(_baseUrl + "/admin/create", user);
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index", "User");
                    }
                    else
                    {
                        return RedirectToAction("Index", "User", new { Message = "Failed to add user." });
                    }
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "User", new { Message = "An error occurred while processing your request." });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, string email, string password, string? firstname,
            string? middlename, string? lastName,int role, string? source, int pubId, DateTime hireDate)
        {
            var user = new UserUpdateProfile()
            {
                FirstName = firstname,
                MiddleName = middlename,
                LastName = lastName,
                Source = source,
                PubId = pubId,
                RoleId = role,
                HireDate = hireDate
            };

            try
            {
                using (var client = new HttpClient())
                {
                    var response = await client.PutAsJsonAsync(_baseUrl + "/admin/update/" + id, user);
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index", "User");
                    }
                    else
                    {
                        return RedirectToAction("Index", "User", new { Message = "Failed to update user." });
                    }
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "User", new { Message = "An error occurred while processing your request." });
            }
        }

    

        public async Task<IActionResult> Delete(int id)
        {
            using (var client = new HttpClient())
            {
                var response = await client.DeleteAsync(_baseUrl + "/" + id);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index", "User");
                }
                else
                {
                    return RedirectToAction("Index", "User", new { Message = "Failed to delete user." });
                }
            }
        }
    }

}
