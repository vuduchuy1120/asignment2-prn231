using BusinessObjects.DTO.User;
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

        public void ChangeUserPassword(int userId, string password) => UserDAO.ChangePassword(userId, password);
        public void ChangeUserRole(int userId, int roleId) => UserDAO.ChangeUserRole(userId, roleId);
        public void DeleteUser(int userId) => UserDAO.DeleteUser(userId);
        public User GetUserByEmailAndPassword(string email, string password) => UserDAO.GetUserByEmailAndPassword(email, password);
        public User GetUserByID(int userId) => UserDAO.GetUserById(userId);
        public List<User> GetUsers() => UserDAO.GetAllUsers();
        public bool IsEmailWasUsed(string email) => UserDAO.IsEmailWasUsed(email);
        public bool IsUserExist(int userId) => UserDAO.IsUserExist(userId);
        public void RegisterUser(User user) => UserDAO.RegisterUser(user);
        public void UpdateUser(User user) => UserDAO.UpdateUser(user);


    }
}
