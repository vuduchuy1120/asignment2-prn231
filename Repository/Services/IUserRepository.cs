using BusinessObjects.DTO;
using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Services
{
    public interface IUserRepository
    {
        public List<User> GetUsers();
        public User GetUserByID(int userId);
        public void AddUser(UserRequest userRequest);
        public User UpdateUser(int userId, UpdateUserRequest userRequest);
        public void DeleteUser(int userId);
    }
}
