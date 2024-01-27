using BusinessObjects.DTO.User;
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
        public void RegisterUser(User user);
        public void UpdateUser(User user);
        public void DeleteUser(int userId);
        public void ChangeUserRole(int userId, int roleId);
        public bool IsUserExist(int userId);
        public bool IsEmailWasUsed(string email);
        public void ChangeUserPassword(int userId, string password);

        public User GetUserByEmailAndPassword(string email, string password);
    }
}
