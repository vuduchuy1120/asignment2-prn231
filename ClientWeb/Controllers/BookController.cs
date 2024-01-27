using BusinessObjects.DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ClientWeb.Controllers
{
    public class BookController : Controller
    {
        private readonly string _baseUrl = "https://localhost:7029/api/Book";
        public async Task<IActionResult> IndexAsync()
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(_baseUrl);

                var responseJson = await response.Content.ReadAsStringAsync();

                var successResponse = JsonConvert.DeserializeObject<ApiResponse<List<BookResponse>>>(responseJson);

                var list = successResponse.Data;

                return View(list);
            }

        }
    }
}
