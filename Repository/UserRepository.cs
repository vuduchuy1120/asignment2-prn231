using BusinessObjects.DTO;
using BusinessObjects.Models;
using DataAccess;
using Repository.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class UserRepository : IUserRepository
    {
        public void AddUser(UserRequest userRequest) => UserDAO.AddUser(userRequest);

        public void DeleteUser(int userId) => UserDAO.DeleteUser(userId);

        public User GetUserByID(int userId) => UserDAO.GetUserById(userId);

        public List<User> GetUsers() => UserDAO.GetAllUsers();

        public User UpdateUser(int userId, UpdateUserRequest userRequest) => UserDAO.UpdateUser(userId, userRequest);
    }
}
