using BusinessObjects.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ClientWeb.Controllers
{
    [Authorize(Policy ="Admin")]
    public class PublisherController : Controller
    {
        private readonly string _baseUrl = "https://localhost:7029/api/Publisher";
        public async Task<IActionResult> IndexAsync()
        {
            using(var client = new HttpClient())
            {
                var response = await client.GetAsync(_baseUrl);

                var responseJson = await response.Content.ReadAsStringAsync();

                var successResponse = JsonConvert.DeserializeObject<ApiResponse<List<PublisherResponse>>>(responseJson);

                var list = successResponse.Data;

                return View(list);
            }

        }

        [HttpPost]
        public async Task<IActionResult> Add(string publisherName, string city)
        {
            var publisher = new PublisherRequest()
            {
                PublisherName = publisherName,
                City = city
           
            };

            try
            {
                using (var client = new HttpClient())
                {
                    var response = await client.PostAsJsonAsync(_baseUrl, publisher);
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index", "Publisher");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Failed to add publisher.");
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

        [HttpPost]
        public async Task<IActionResult> Update(int publisherId, string publisherName, string city)
        {
            var publisher = new PublisherRequest()
            {
                PublisherName = publisherName,
                City = city
            };

            try
            {
                using (var client = new HttpClient())
                {
                    var response = await client.PutAsJsonAsync(_baseUrl + "/" + publisherId, publisher);
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index", "Publisher");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Failed to update publisher.");
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
                    return RedirectToAction("Index", "Publisher");
                }
                else
                {
                    ModelState.AddModelError("", "Failed to delete publisher.");
                    return View();
                }
            }
        }


    }
}
