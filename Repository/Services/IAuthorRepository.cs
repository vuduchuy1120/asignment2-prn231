using BusinessObjects.DTO.Author;
using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Services
{
    public interface IAuthorRepository
    {
        public void AddAuthor(AuthorRequest author);
        public Author UpdateAuthor(int id, AuthorRequest author);
        public void DeleteAuthor(Author author);
        public Author GetAuthorById(int id);
        public List<Author> GetAllAuthors();

    }
}
