using BusinessObjects.DTO.Author;
using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class AuthorDAO
    {
 
        // getAllAuthor
        public static List<Author> getAllAuthor()
        {
            using(var _context = new BookStoreContext())
            {
                try
                {

                    return _context.Authors.ToList();
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
        }
        // getAuthorById
        public static Author getAuthorById(int id)
        {
            Author author = new Author();
            try
            {
                using(var _context = new BookStoreContext())
                {
                    author = _context.Authors.SingleOrDefault(x => x.AuthorId == id);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return author;
        }
        // addAuthor
        public static void addAuthor(AuthorRequest authorRequest)
        {
            
            var author = new Author()
            {
                LastName = authorRequest.LastName,
                FirstName = authorRequest.FirstName,
                Phone = authorRequest.Phone,
                Address = authorRequest.Address,
                City = authorRequest.City,
                State = authorRequest.State,
                Zip = authorRequest.Zip,
                Email = authorRequest.Email
            };
            try
            {
                using(var _context = new BookStoreContext()) { 
                    _context.Authors.Add(author);
                    _context.SaveChanges();
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        // updateAuthor
        public static Author updateAuthor(int id , AuthorRequest author)
        {
            try
            {
                using(var _context = new BookStoreContext())
                {
                    Author authorUpdate = _context.Authors.SingleOrDefault(x => x.AuthorId == id);
                    authorUpdate.LastName = author.LastName;
                    authorUpdate.FirstName = author.FirstName;
                    authorUpdate.Phone = author.Phone;
                    authorUpdate.Address = author.Address;
                    authorUpdate.City = author.City;
                    authorUpdate.State = author.State;
                    authorUpdate.Zip = author.Zip;
                    authorUpdate.Email = author.Email;
                    _context.SaveChanges();
                    return authorUpdate;

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        // deleteAuthor
        public static void deleteAuthor(int id)
        {
            try

            {
                using(var _context = new BookStoreContext()){
                    Author author = _context.Authors.SingleOrDefault(x => x.AuthorId == id);
                    _context.Authors.Remove(author);
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        //search Author by Name
        public static List<Author> searchAuthorByName(string name)
        {
            try
            {
                using (var _context = new BookStoreContext())
                {
                    return _context.Authors.Where(x => x.FirstName.Contains(name) || x.LastName.Contains(name)).ToList();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
