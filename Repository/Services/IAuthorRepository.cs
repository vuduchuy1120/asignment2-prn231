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
        public void AddAuthor(Author author);
        public void UpdateAuthor(Author author);
        public void DeleteAuthor(Author author);
        public Author GetAuthorById(int id);
        public List<Author> GetAllAuthors();

    }
}
