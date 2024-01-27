using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.DTO
{
    public class BookResponse
    {
        public int BookId { get; set; }
        public string Title { get; set; } = null!;
        public string Type { get; set; } = null!;
        public int PubId { get; set; }
        public decimal? Price { get; set; }
        public string? Advance { get; set; }
        public string? Royalty { get; set; }
        public string? YtdSales { get; set; }
        public string? Notes { get; set; }
        public DateTime? PublishedDate { get; set; }

        public List<BookAuthorResponse> BookAuthors { get; set; }

    }
}
