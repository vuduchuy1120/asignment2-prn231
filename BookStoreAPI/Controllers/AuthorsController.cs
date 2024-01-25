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

namespace BookStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private IAuthorRepository repository = new AuthorRepository();

        // GET: api/Authors
        [HttpGet]
        public ActionResult<IEnumerable<Author>> GetAuthors()
        {
            return repository.GetAllAuthors();
        }
        [HttpPost]
        public ActionResult<Author> AddAuthor(Author author)
        {
            repository.AddAuthor(author);
            return author;
        }
        [HttpPut]
        public ActionResult<Author> UpdateAuthor(Author author)
        {
            repository.UpdateAuthor(author);
            return author;
        }
        [HttpDelete("{id}")]
        public ActionResult<Author> DeleteAuthor(int id)
        {
            Author author = repository.GetAuthorById(id);
            repository.DeleteAuthor(author);
            return author;
        }
        [HttpGet("{id}")]
        public ActionResult<Author> GetAuthorById(int id)
        {
            return repository.GetAuthorById(id);
        }

    }
}
