using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.DTO.User
{
    public class ChangePasswordRequest
    {
        public int UserId { get; set; }
        public string Password { get; set; } = null!;
    }
}
