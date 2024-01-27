using BusinessObjects.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository;
using Repository.Services;

namespace BookStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublisherController : ControllerBase
    {
        private IPublisherRepository repository = new PublisherRepository();
        //Get: api/Publisher
        [HttpGet]
        public IActionResult GetPublisher()
        {
            try
            {
                var list = repository.GetPublishers();
                var response = list.Select(publisher => new PublisherResponse
                {
                    PublisherId = publisher.PubId,
                    PublisherName = publisher.PublisherName,
                    City = publisher.City,
                    Country = publisher.Country,
                    Books = publisher.Books.Select(book => new BookResponse
                    {
                        BookId = book.BookId,
                        Title = book.Title,        
                        PubId = book.PubId,
                        BookAuthors = book.BookAuthors.Select(bookAuthor => new BookAuthorResponse
                        {
                            AuthorId = bookAuthor.AuthorId               
                        }).ToList()
                    }).ToList()
                }).ToList();
                return Ok(new ApiResponse<object>("Get list successfull!", response));

            }
            catch (Exception e)
            {
                return BadRequest(new ApiResponse<object>(e.Message));
            }
        }
        [HttpPost]
        public IActionResult AddPublisher(PublisherRequest publisher)
        {
            try
            {
                repository.AddPublisher(publisher);
                return Ok(new ApiResponse<object>(publisher));
            }
            catch (Exception e)
            {
                return BadRequest(new ApiResponse<object>(e.Message));
            }
        }
        [HttpPut("{id}")]
        public IActionResult UpdatePublisher(int id, PublisherRequest publisher)
        {
            var updatePublisher = repository.GetPublisherByID(id);
            if (updatePublisher == null)
            {
                return NotFound(new ApiResponse<object>("Publisher not found!"));
            }
            try
            {
                updatePublisher = repository.UpdatePublisher(id, publisher);

                // convert to response
                var response = new PublisherResponse
                {
                    PublisherId = updatePublisher.PubId,
                    PublisherName = updatePublisher.PublisherName,
                    City = updatePublisher.City,
                    State = updatePublisher.State,
                    Country = updatePublisher.Country
                };

                return Ok(new ApiResponse<object>(response));
            }
            catch (Exception e)
            {
                return BadRequest(new ApiResponse<object>(e.Message));
            }
        }
        [HttpDelete("{id}")]
        public IActionResult DeletePublisher(int id)
        {
            if(repository.GetPublisherByID(id) == null)
            {
                return NotFound(new ApiResponse<object>("Publisher not found!"));
            }
            try
            {
                repository.DeletePublisher(id);
                return Ok(new ApiResponse<object>(true,"Delete successfull!"));
            }
            catch (Exception e)
            {
                return BadRequest(new ApiResponse<object>(e.Message));
            }
        }

        // getbyID
        [HttpGet("{id}")]
        public IActionResult GetPublisherById(int id)
        {
            try
            {
                var publisher = repository.GetPublisherByID(id);
                if (publisher == null)
                {
                    return NotFound(new ApiResponse<object>("Publisher not found!"));
                }
                // convert to response
                var response = new PublisherResponse
                {
                    PublisherId = publisher.PubId,
                    PublisherName = publisher.PublisherName,
                    City = publisher.City,
                    State = publisher.State,
                    Country = publisher.Country
                };
                return Ok(new ApiResponse<object>(response));
            }
            catch (Exception e)
            {
                return BadRequest(new ApiResponse<object>(e.Message));
            }
        }

    }
}
