using BusinessObjects.DTO.User;
using BusinessObjects.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class UserDAO
    {
        // get user by email
        public static User GetUserByEmail(string email)
        {
            using (var context = new BookStoreContext())
            {
                return context.Users.Include(r=>r.Role).Where(u => u.Email == email).FirstOrDefault();
            }

        }
        // get all users
        public static List<User> GetAllUsers()
        {
            using (var context = new BookStoreContext())
            {
                return context.Users.Include(p=>p.Role).ToList();
            }
        }
        // get user by id
        public static User GetUserById(int id)
        {
            using (var context = new BookStoreContext())
            {
                return context.Users.Include(r => r.Role).Where(u => u.UserId == id).FirstOrDefault();
            }
        }
        // add user

        public static void RegisterUser(User user)
        {
            using (var context = new BookStoreContext())
            {
                context.Users.Add(user);
                context.SaveChanges();
            }
        }

        public static void UpdateUser(User user)
        {
            using (var context = new BookStoreContext())
            {
                Console.WriteLine(user.RoleId);
                context.Users.Update(user);

                context.SaveChanges();
            }
        }

        public static void DeleteUser(int id)
        {
            using (var context = new BookStoreContext())
            {
                User user = context.Users.Include(r => r.Role).Where(u => u.UserId == id).FirstOrDefault();
                context.Users.Remove(user);
                context.SaveChanges();
            }
        }

        public static User GetUserByEmailAndPassword(string email, string password)
        {

            using (var context = new BookStoreContext())
            {
                return context.Users.Include(r => r.Role).Where(u => u.Email == email && u.Password == password).FirstOrDefault();
            }
        }
        public static bool IsEmailWasUsed(string email)
        {
            using (var _context = new BookStoreContext())
            {
                return _context.Users.Include(r => r.Role).Any(u => u.Email == email);
            }
        }

        public static bool IsUserExist(int userId)
        {
            using (var _context = new BookStoreContext())
            {
                return _context.Users.Include(r => r.Role).Any(x => x.UserId == userId);
            }
        }
        public static void ChangeUserRole(int userId, int roleId)
        {
            var user = GetUserById(userId);
            user.RoleId = roleId;
            using (var _context = new BookStoreContext())
            {
                _context.Users.Update(user);
                _context.SaveChanges();
            }
        }

        public static void ChangePassword(int userId, string newPassword)
        {
            var user = GetUserById(userId);
            user.Password = newPassword;
            using (var _context = new BookStoreContext())
            {
                _context.Users.Update(user);
                _context.SaveChanges();
            }
        }

    }
}
