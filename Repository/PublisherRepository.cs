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
    public class PublisherRepository : IPublisherRepository
    {
        public void AddPublisher(PublisherRequest publisherRequest) => PublisherDAO.AddPublisher(publisherRequest);

        public void DeletePublisher(int publisherId) => PublisherDAO.DeletePublisher(publisherId);

        public Publisher GetPublisherByID(int publisherId) => PublisherDAO.GetPublisherById(publisherId);

        public List<Publisher> GetPublishers() => PublisherDAO.GetPublishers();


        public Publisher UpdatePublisher(int publisherId, PublisherRequest publisherRequest) => PublisherDAO.UpdatePublisher(publisherId, publisherRequest);
    }
}

