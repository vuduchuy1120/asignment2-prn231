using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.DTO.User
{
    public class UserUpdateProfile
    {

        public int PubId { get; set; }

        public string? Source { get; set; }
        public int RoleId { get; set; }
        public string? FirstName { get; set; }

        public string? MiddleName { get; set; }

        public string? LastName { get; set; }

        public DateTime? HireDate { get; set; }
    }
}
