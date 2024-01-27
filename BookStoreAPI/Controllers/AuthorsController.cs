using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BusinessObjects.Models;
using Repository.Services;
using Repository;
using BusinessObjects.DTO;
using BusinessObjects.DTO.Author;

namespace BookStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private IAuthorRepository repository = new AuthorRepository();

        // GET: api/Authors
        [HttpGet]
        public IActionResult GetAuthors()
        {
            try
            {
                var list = repository.GetAllAuthors();
                // convert Author to AuthorResponse
                var response = list.Select(author => new AuthorResponse
                {
                    AuthorId = author.AuthorId,
                    LastName = author.LastName,
                    FirstName = author.FirstName,
                    Email = author.Email,
                    Phone = author.Phone,
                    Address = author.Address,
                    City = author.City,
                    State = author.State,
                    Zip = author.Zip,
                    BookAuthors = author.BookAuthors.Select(book => new BookAuthorResponse
                    {
                        BookId = book.BookId,
                        AuthorOrder = book.AuthorOrder,
                        RoyalityPercentage = book.RoyalityPercentage
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
        public IActionResult AddAuthor(AuthorRequest author)
        {
            try
            {
                repository.AddAuthor(author);
                return Ok(new ApiResponse<object>(author));
            }
            catch (Exception e)
            {
                return BadRequest(new ApiResponse<object>(e.Message));
            }
        }
        [HttpPut]
        [Route("{id}")]
        public IActionResult UpdateAuthor([FromRoute] int id, AuthorRequest author)
        {

            var authorUpdate = repository.GetAuthorById(id);
            if (authorUpdate == null)
            {
                return BadRequest(new ApiResponse<object>("Author not found"));
            }
            try
            {

                repository.UpdateAuthor(id, author);

                return Ok(new ApiResponse<object>(author));
            }
            catch (Exception e)
            {
                return BadRequest(new ApiResponse<object>(e.Message));
            }

        }
        [HttpDelete("{id}")]
        public IActionResult DeleteAuthor(int id)
        {
            try
            {
                Author author = repository.GetAuthorById(id);
                repository.DeleteAuthor(author);
                return Ok(new ApiResponse<object>(true, "Delete successfull!"));
            }
            catch
            {
                return BadRequest(new ApiResponse<object>("Author not found"));
            }
        }
        [HttpGet("{id}")]
        public IActionResult GetAuthorById(int id)
        {

           var author = repository.GetAuthorById(id);
            if (author == null)
            {
                return BadRequest(new ApiResponse<object>("Author not found"));
            }
            try
            {
                var response = new AuthorResponse
                {
                    AuthorId = author.AuthorId,
                    LastName = author.LastName,
                    FirstName = author.FirstName,
                    Email = author.Email,
                    Phone = author.Phone,
                    Address = author.Address,
                    City = author.City,
                    State = author.State,
                    Zip = author.Zip,
                    BookAuthors = author.BookAuthors.Select(book => new BookAuthorResponse
                    {
                        BookId = book.BookId,
                        AuthorOrder = book.AuthorOrder,
                        RoyalityPercentage = book.RoyalityPercentage
                    }).ToList()
                };
                return Ok(new ApiResponse<object>("Get successfull!", response));
            }
            catch (Exception e)
            {
                return BadRequest(new ApiResponse<object>(e.Message));
            }
        }

    }
}
