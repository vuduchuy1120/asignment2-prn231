using BusinessObjects.DTO;
using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Services
{
    public interface IBookRepository
    {
        public List<Book> GetBooks();
        public Book GetBookByID(int bookId);
        public void AddBook(BookRequest bookRequest);
        public Book UpdateBook(int bookId, BookRequest bookRequest);
        public void DeleteBook(int bookId);
            
        public List<Book> GetBooksByNameOrPrice(string name, decimal? price);
    }
}
