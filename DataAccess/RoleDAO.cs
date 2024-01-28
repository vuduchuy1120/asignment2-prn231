using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class RoleDAO
    {
        public static List<Role> getAllRole()
        {
            using(var db = new BookStoreContext())
            {
                return db.Roles.ToList();
            }
        }
    }
}
