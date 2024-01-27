using BusinessObjects.DTO;
using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Services
{
    public interface IPublisherRepository
    {
        public List<Publisher> GetPublishers();
        public Publisher GetPublisherByID(int publisherId);
        public void AddPublisher(PublisherRequest publisherRequest);
        public Publisher UpdatePublisher(int publisherId, PublisherRequest publisherRequest);
        public void DeletePublisher(int publisherId);

    }
}
