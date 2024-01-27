using BusinessObjects.DTO.Author;
using BusinessObjects.Models;
using DataAccess;
using Repository.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class AuthorRepository : IAuthorRepository
    {
        public void AddAuthor(AuthorRequest author) => AuthorDAO.addAuthor(author);
        public void DeleteAuthor(Author author) => AuthorDAO.deleteAuthor(author.AuthorId);
        public List<Author> GetAllAuthors() => AuthorDAO.getAllAuthor();
        public Author GetAuthorById(int id) => AuthorDAO.getAuthorById(id);
        public Author UpdateAuthor(int id, AuthorRequest author) => AuthorDAO.updateAuthor(id, author);
    }
}
