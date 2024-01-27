using BusinessObjects.DTO;
using BusinessObjects.DTO.Author;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ClientWeb.Controllers
{
    [Authorize(Policy = "Admin")]
    public class AuthorController : Controller
    {
        private readonly string _baseUrl = "https://localhost:7029/api/Authors";
        public async Task<IActionResult> IndexAsync()
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(_baseUrl);

                var responseJson = await response.Content.ReadAsStringAsync();

                var successResponse = JsonConvert.DeserializeObject<ApiResponse<List<AuthorResponse>>>(responseJson);

                var list = successResponse.Data;

                return View(list);
            }

        }

        [HttpPost]
        public async Task<IActionResult> Add(string firstname, string lastname,
                                    string? phone, string? address,
                                    string? city, string? state,
                                    string? zip, string email)
        {
            var author = new AuthorRequest()
            {
                FirstName = firstname,
                LastName = lastname,
                Phone = phone,
                Address = address,
                City = city,
                State = state,
                Zip = zip,
                Email = email
            };

            try
            {
                using (var client = new HttpClient())
                {
                    var response = await client.PostAsJsonAsync(_baseUrl, author);


                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index", "Author");
                    }
                    else
                    {                        
                        return RedirectToAction("Index", "Author", new {Message = "Email invalid!"});

                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception for troubleshooting
                ModelState.AddModelError("", "An error occurred while processing your request.");
                return View();
            }
        }
        [HttpPost]
        public async Task<IActionResult> Update(int authorId, string firstname, string lastname,
                                      string? phone, string? address,
                                      string? city, string? state,
                                      string? zip, string email)
        {
            var author = new AuthorRequest()
            {
                FirstName = firstname,
                LastName = lastname,
                Phone = phone,
                Address = address,
                City = city,
                State = state,
                Zip = zip,
                Email = email
            };

            try
            {
                using (var client = new HttpClient())
                {
                    var response = await client.PutAsJsonAsync(_baseUrl + "/" + authorId, author);
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index", "Author");
                    }
                    else
                    {
                        return View();
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception for troubleshooting
                ModelState.AddModelError("", "An error occurred while processing your request.");
                return View();
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            using (var client = new HttpClient())
            {
                var response = await client.DeleteAsync(_baseUrl + "/" + id);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index", "Author");
                }
                else
                {
                    return View();
                }
            }
        }

    }
}

