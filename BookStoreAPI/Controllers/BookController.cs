using BusinessObjects.DTO;
using Microsoft.AspNetCore.Mvc;
using Repository;
using Repository.Services;

namespace BookStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        IBookRepository repository = new BookRepository();
        // GET: api/Book
        [HttpGet]
        public IActionResult GetBooks()
        {
            try
            {
                var list = repository.GetBooks();
                var response = list.Select(book =>
                {
                    return new BookResponse
                    {
                        BookId = book.BookId,
                        Title = book.Title,
                        Type = book.Type,
                        PubId = book.PubId,
                        pubName = book.Pub.PublisherName,
                        Price = (book.Price),
                        Advance = book.Advance,
                        Royalty = book.Royalty,
                        YtdSales = book.YtdSales,
                        PublishedDate = book.PublishedDate,
                        Notes = book.Notes,

                        BookAuthors = book.BookAuthors.Select(author => new BookAuthorResponse
                        {
                            AuthorId = author.AuthorId,


                        }).ToList()
                    };
                }).ToList();
                return Ok(new ApiResponse<object>("Get list successfull!", response));
            }
            catch (Exception e)
            {
                return BadRequest(new ApiResponse<object>(e.Message));
            }
        }
        // GET: api/Book/5
        [HttpGet("{id}")]
        public IActionResult GetBookById(int id)
        {
            try
            {
                var book = repository.GetBookByID(id);
                if (book == null)
                {
                    return NotFound(new ApiResponse<object>("Book not found!"));
                }
                var response = new BookResponse
                {

                    BookId = book.BookId,
                    Title = book.Title,
                    Type = book.Type,
                    PubId = book.PubId,
                    pubName = book.Pub.PublisherName,

                    Price = book.Price,
                    Advance = book.Advance,
                    Royalty = book.Royalty,
                    YtdSales = book.YtdSales,

                    PublishedDate = book.PublishedDate,
                    Notes = book.Notes,

                    BookAuthors = book.BookAuthors.Select(author => new BookAuthorResponse
                    {
                        AuthorId = author.AuthorId
                    }).ToList()
                };
                return Ok(new ApiResponse<object>("Get book successfull!", response));
            }
            catch (Exception e)
            {
                return BadRequest(new ApiResponse<object>(e.Message));
            }
        }
        // POST: api/Book
        [HttpPost]
        public IActionResult AddBook(BookRequest book)
        {
            try
            {
                repository.AddBook(book);
                return Ok(new ApiResponse<object>(book));
            }
            catch (Exception e)
            {
                return BadRequest(new ApiResponse<object>(e.Message));
            }
        }
        // PUT: api/Book/5
        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, BookRequest book)
        {
            var updateBook = repository.GetBookByID(id);
            if (updateBook == null)
            {
                return NotFound(new ApiResponse<object>("Book not found!"));
            }
            try
            {
                updateBook = repository.UpdateBook(id, book);
                var book1 = repository.GetBookByID(id);
                // convert to response
                var response = new BookResponse
                {
                    BookId = book1.BookId,
                    Title = book1.Title,
                    Price = book1.Price,
                    Advance = book1.Advance,
                    Royalty = book1.Royalty,
                    PubId = book1.PubId,
                    pubName = book1.Pub.PublisherName,
                    YtdSales = book1.YtdSales,
                    Type = book1.Type,
                    PublishedDate = book1.PublishedDate,
                    Notes = book1.Notes,
                    BookAuthors = book1.BookAuthors.Select(author => new BookAuthorResponse
                    {
                        AuthorId = author.AuthorId

                    }).ToList()
                };

                return Ok(new ApiResponse<object>(response));
            }
            catch (Exception e)
            {
                return BadRequest(new ApiResponse<object>(e.Message));
            }
        }
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            try
            {
                var book = repository.GetBookByID(id);
                if (book == null)
                {
                    return NotFound(new ApiResponse<object>("Book not found!"));
                }
                repository.DeleteBook(id);

                return Ok(new ApiResponse<object>("Delete book successfull!"));
            }
            catch (Exception e)
            {
                return BadRequest(new ApiResponse<object>(e.Message));
            }
        }

        [HttpGet("search")]
        public IActionResult SearchBook(string ?name, decimal? price)
        {
            try
            {
                var list = repository.GetBooksByNameOrPrice(name, price);
                if(list == null)
                {
                    return Ok(new ApiResponse<object>(true,"Do not have book"));
                }
                var response = list.Select(book => new BookResponse
                {
                    BookId = book.BookId,
                    Title = book.Title,
                    Type = book.Type,
                    PubId = book.PubId,
                    pubName = book.Pub.PublisherName,
                    Price = (book.Price),
                    Advance = book.Advance,
                    Royalty = book.Royalty,
                    YtdSales = book.YtdSales,
                    PublishedDate = book.PublishedDate,
                    Notes = book.Notes,

                    BookAuthors = book.BookAuthors.Select(author => new BookAuthorResponse
                    {
                        AuthorId = author.AuthorId
                    }).ToList()
                });
                return Ok(new ApiResponse<object>(response));

            }
            catch
            {
                return BadRequest(new ApiResponse<object>(false, "Error!"));
            }


        }


    }
}
