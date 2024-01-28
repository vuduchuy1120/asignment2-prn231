using BusinessObjects.DTO;
using BusinessObjects.DTO.Author;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace ClientWeb.Controllers
{
    public class BookController : Controller
    {

        private readonly string _baseUrl = "https://localhost:7029/api/Book";
        //public async Task<IActionResult> IndexAsync()
        //{
        //    using (var client = new HttpClient())
        //    {
        //        var response = await client.GetAsync(_baseUrl);

        //        var responseJson = await response.Content.ReadAsStringAsync();

        //        var successResponse = JsonConvert.DeserializeObject<ApiResponse<List<BookResponse>>>(responseJson);

        //        var list = successResponse.Data;

        //        var publisherResponse = await client.GetAsync("https://localhost:7029/api/Publisher");
        //        var publisherResponseJson = await publisherResponse.Content.ReadAsStringAsync();
        //        var publisherSuccessResponse = JsonConvert.DeserializeObject<ApiResponse<List<PublisherResponse>>>(publisherResponseJson);
        //        var publisherList = publisherSuccessResponse.Data.ToList();

        //        ViewData["pubID"] = new SelectList(publisherList, "PublisherId", "PublisherName");

        //        return View(list);
        //    }

        //}
        public async Task<IActionResult> IndexAsync(string? name, decimal? price)
        {
            using (var client = new HttpClient())
            {
                var bookUrl = $"{_baseUrl}";

                if (name != null || price != null)
                {
                    bookUrl += "/search";

                    if (name != null)
                    {
                        bookUrl += $"?name={name}";
                    }

                    if (price != null)
                    {
                        bookUrl += (name != null) ? $"&price={price}" : $"?price={price}";
                    }
                }

                var bookResponse = await client.GetAsync(bookUrl);

                if (!bookResponse.IsSuccessStatusCode)
                {
                    return View(); // Handle error
                }

                var bookResponseJson = await bookResponse.Content.ReadAsStringAsync();
                var bookSuccessResponse = JsonConvert.DeserializeObject<ApiResponse<List<BookResponse>>>(bookResponseJson);
                var bookList = bookSuccessResponse.Data;

                var publisherResponse = await client.GetAsync("https://localhost:7029/api/Publisher");
                var publisherResponseJson = await publisherResponse.Content.ReadAsStringAsync();
                var publisherSuccessResponse = JsonConvert.DeserializeObject<ApiResponse<List<PublisherResponse>>>(publisherResponseJson);
                var publisherList = publisherSuccessResponse.Data.ToList();

                ViewData["pubID"] = new SelectList(publisherList, "PublisherId", "PublisherName");

                return View(bookList);
            }
        }


        [HttpPost]
        public async Task<IActionResult> Add(string title, string type, int pubId,
            decimal? price1, DateTime? publishedDate)

        {
           var book = new BookRequest()
           {
                Title = title,
                Type = type,
                PubId = pubId,
                Price = price1,
                PublishedDate = publishedDate
            };

            try
            {
                using (var client = new HttpClient())
                {
                    var response = await client.PostAsJsonAsync(_baseUrl, book);


                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index", "Book");
                    }
                    else
                    {
                        return RedirectToAction("Index", "Book", new { Message = "Error!" });

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
        public async Task<IActionResult> Update(int BookId, string title, string type, int pubId,
            decimal? price1, DateTime? publishedDate)
        {
            var book = new BookRequest()
            {
                Title = title,
                Type = type,
                PubId = pubId,
                Price = price1,
                PublishedDate = publishedDate
            };

            try
            {
                using (var client = new HttpClient())
                {
                    var response = await client.PutAsJsonAsync(_baseUrl + "/" + BookId, book);
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index", "Book");
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
                    return RedirectToAction("Index", "Book");
                }
                else
                {
                    return View();
                }
            }
        }
        //Search
        public async Task<IActionResult> Search(string? name, decimal? price)
        {
            using (var client = new HttpClient())
            {

                if (name == null && price == null)
                {
                    var response = await client.GetAsync(_baseUrl + "/search");
                    if(response.IsSuccessStatusCode)
                    {
                        var responseJson = await response.Content.ReadAsStringAsync();
                        var successResponse = JsonConvert.DeserializeObject<ApiResponse<List<BookResponse>>>(responseJson);
                        var list = successResponse.Data;
                        return View(list);
                    }
                    else
                    {
                        return View();
                    }

                }
                if(name == null && price != null)
                {
                    var response = await client.GetAsync(_baseUrl + "/search?price=" + price);
                    if(response.IsSuccessStatusCode)
                    {
                        var responseJson = await response.Content.ReadAsStringAsync();
                        var successResponse = JsonConvert.DeserializeObject<ApiResponse<List<BookResponse>>>(responseJson);
                        var list = successResponse.Data;
                        return View(list);
                    }
                    else
                    {
                        return View();
                    }
                }
                if(price == null && name != null)
                {
                    var response = await client.GetAsync(_baseUrl + "/search?name=" + name);
                    if(response.IsSuccessStatusCode)
                    {
                        var responseJson = await response.Content.ReadAsStringAsync();
                        var successResponse = JsonConvert.DeserializeObject<ApiResponse<List<BookResponse>>>(responseJson);
                        var list = successResponse.Data;
                        return View(list);
                    }
                    else
                    {
                        return View();
                    }
                }
                else
                {
                    var response = await client.GetAsync(_baseUrl + "/search?name=" + name + "&price=" + price);
                    if(response.IsSuccessStatusCode)
                    {
                        var responseJson = await response.Content.ReadAsStringAsync();
                        var successResponse = JsonConvert.DeserializeObject<ApiResponse<List<BookResponse>>>(responseJson);
                        var list = successResponse.Data;
                        return View(list);
                    }
                    else
                    {
                        return View();
                    }
                }

            }
        }
    }
}
