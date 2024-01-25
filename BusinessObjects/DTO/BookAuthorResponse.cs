using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.DTO
{
    internal class BookAuthorResponse
    {
        public int AuthorId { get; set; }
        public int BookId { get; set; }
        public string? AuthorOrder { get; set; }
        public string? RoyalityPercentage { get; set; }
    }
}
