using System;
using System.Collections.Generic;

namespace BusinessObjects.Models
{
    public partial class Book
    {
        public Book()
        {
            BookAuthors = new HashSet<BookAuthor>();
        }

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

        public virtual Publisher Pub { get; set; } = null!;
        public virtual ICollection<BookAuthor> BookAuthors { get; set; }
    }
}
