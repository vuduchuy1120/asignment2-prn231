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
    public class PublisherDAO
    {
        // CRUD
        static BookStoreContext context = new BookStoreContext();
        public static List<Publisher> GetPublishers()
        {
            try
            {
                using (var context = new BookStoreContext())
                {
                    return context.Publishers.Include(p => p.Books).ToList();

                };

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static Publisher GetPublisherById(int id)
        {
            try
            {
                using (var context = new BookStoreContext())
                {
                    return context.Publishers.SingleOrDefault(x => x.PubId == id);

                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static void AddPublisher(PublisherRequest publisherRequest)
        {
            var publisher = new Publisher()
            {
                PublisherName = publisherRequest.PublisherName,
                City = publisherRequest.City
            };
            try
            {
                using (var context = new BookStoreContext())
                {
                    context.Publishers.Add(publisher);
                    context.SaveChanges();
                };
             
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static Publisher UpdatePublisher(int id, PublisherRequest publisherRequest)
        {
            var publisher = context.Publishers.SingleOrDefault(x => x.PubId == id);
            if (publisher != null)
            {
                publisher.PublisherName = publisherRequest.PublisherName;
                publisher.City = publisherRequest.City;
                try
                {
                    using (var context = new BookStoreContext())
                    {
                        context.SaveChanges();
                    };
                    return publisher;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return null;
        }
        public static void DeletePublisher(int id)
        {
            var publisher = context.Publishers.SingleOrDefault(x => x.PubId == id);
            if (publisher != null)
            {
                try
                {
                    using (var context = new BookStoreContext())
                    {
                        context.Publishers.Remove(publisher);
                        context.SaveChanges();
                    };
                  
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}
