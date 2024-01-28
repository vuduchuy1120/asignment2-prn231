using BusinessObjects.DTO;
using BusinessObjects.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class BookDAO
    {
        public static List<Book> GetBooks()
        {
            using (var context = new BookStoreContext())
            {
                return context.Books.Include(p => p.BookAuthors).Include(p=>p.Pub).ToList();
            };
        }
        public static Book GetBookById(int id)

        {
            using (var context = new BookStoreContext())
            {
                return context.Books.Include(p=>p.BookAuthors).Include(p => p.Pub).SingleOrDefault(x => x.BookId == id);
            };
        }

        public static Book getBookByIdNotInclude(int id)
        {
            using (var context = new BookStoreContext())
            {
                return context.Books.SingleOrDefault(x => x.BookId == id);
            };
        }
        public static void AddBook(BookRequest bookRequest)
        {
            var book = new Book()
            {
                Title = bookRequest.Title,
                Type = bookRequest.Type,
                Price = bookRequest.Price,
                PubId = bookRequest.PubId,
                Advance = bookRequest.Advance,
                Royalty = bookRequest.Royalty,
                YtdSales = bookRequest.YtdSales,
                Notes = bookRequest.Notes,
                PublishedDate = bookRequest.PublishedDate
            };
            try
            {
                using (var context = new BookStoreContext())
                {
                    context.Books.Add(book);
                    context.SaveChanges();
                };

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static Book UpdateBook(int id, BookRequest bookRequest)
        {
            using (var context = new BookStoreContext())
            {
                var book = context.Books.Include(p=>p.Pub).SingleOrDefault(x => x.BookId == id);
                if (book != null)
                {
                    book.Title = bookRequest.Title;
                    book.Type = bookRequest.Type;
                    book.Price = bookRequest.Price;
                    book.PubId = bookRequest.PubId;
                    book.Advance = bookRequest.Advance;
                    book.Royalty = bookRequest.Royalty;
                    book.YtdSales = bookRequest.YtdSales;
                    book.Notes = bookRequest.Notes;
                    book.PublishedDate = bookRequest.PublishedDate;
                    try
                    {
                        context.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
                return book;
            }

        }
        public static void DeleteBook(int id)
        {
            using (var context = new BookStoreContext())
            {
                var book = context.Books.SingleOrDefault(x => x.BookId == id);
                if (book != null)
                {
                    try
                    {
                        var listBookAuthor = context.BookAuthors.Where(x => x.BookId == id).ToList();
                        foreach (var item in listBookAuthor)
                        {
                            context.BookAuthors.Remove(item);
                        }
                        context.Books.Remove(book);
                        context.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }

                }
            }
        }

        // search book by name or price parameter is searchName and searchPrice
        public static List<Book> SearchBook(string searchName, decimal? searchPrice)
        {
            using (var context = new BookStoreContext())
            {
                var books = context.Books.Include(p => p.BookAuthors).Include(p => p.Pub).ToList();
                if (searchName != null)
                {
                    books = books.Where(x => x.Title.ToLower().Contains(searchName.ToLower())).ToList();
                }
                if (searchPrice != null)
                {
                    books = books.Where(x => x.Price == searchPrice).ToList();
                }
                return books;
            }
        }


    }
}
