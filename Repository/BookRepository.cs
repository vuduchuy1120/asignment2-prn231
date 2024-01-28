using BusinessObjects.DTO;
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
    public class BookRepository : IBookRepository
    {
        public void AddBook(BookRequest bookRequest) => BookDAO.AddBook(bookRequest);


        public void DeleteBook(int bookId) => BookDAO.DeleteBook(bookId);

        public Book GetBookByID(int bookId) => BookDAO.GetBookById(bookId);

        public List<Book> GetBooks() => BookDAO.GetBooks();

        public List<Book> GetBooksByNameOrPrice(string name, decimal? price)=> BookDAO.SearchBook(name, price);
        public Book UpdateBook(int bookId, BookRequest bookRequest)=> BookDAO.UpdateBook(bookId, bookRequest);
    }
}
