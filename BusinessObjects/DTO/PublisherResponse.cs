using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.DTO
{
    public class PublisherResponse
    {
        public int PublisherId { get; set; }
        public string PublisherName { get; set; } = null!;
        public string State { get; set; } = null!;
        public string City { get; set; } = null!;
        public string Country { get; set; }

        public List<BookResponse> Books { get; set; } = null!;
    }
}
