using BusinessObjects.DTO;
using BusinessObjects.DTO.User;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text.Json;

namespace ClientWeb.Controllers
{
    public class UserController : Controller
    {
        private readonly string _baseUrl = "https://localhost:7029/api/User";
        public async Task<IActionResult> IndexAsync()
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(_baseUrl);

                var responseJson = await response.Content.ReadAsStringAsync();

                var successResponse = JsonConvert.DeserializeObject<ApiResponse<List<UserResponse>>>(responseJson);

                var list = successResponse.Data;

                return View(list);
            }

        }
    }
}
