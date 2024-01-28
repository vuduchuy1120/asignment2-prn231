using System;
using System.Collections.Generic;

namespace BusinessObjects.Models
{
    public partial class User
    {
        public int UserId { get; set; }
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string? Source { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public int RoleId { get; set; }
        public int PubId { get; set; }
        public DateTime? HireDate { get; set; }

        public virtual Publisher? Pub { get; set; } = null!;
        public virtual Role? Role { get; set; } = null!;
    }
}
