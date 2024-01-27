using BusinessObjects.DTO;
using BusinessObjects.Models;
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
           using(var context = new BookStoreContext())
            {
                return context.Users.Where(u => u.Email == email).FirstOrDefault();
            }

        }
        // get all users
        public static List<User> GetAllUsers()
        {
            using (var context = new BookStoreContext())
            {
                return context.Users.ToList();
            }
        }
        // get user by id
        public static User GetUserById(int id)
        {
            using (var context = new BookStoreContext())
            {
                return context.Users.Where(u => u.UserId == id).FirstOrDefault();
            }
        }
        // add user
        public static void AddUser(UserRequest user)
        {
            using (var context = new BookStoreContext())
            {
                User user1 = new User()
                {
                    Email = user.Email,
                    Password = user.Password,
                    RoleId = 1,
                    PubId = 1
                };
                context.Users.Add(user1);
                context.SaveChanges();
            }
        }
        // update user
        public static User UpdateUser(int id, UpdateUserRequest user)
        {
            using (var context = new BookStoreContext())
            {
                User user1 = context.Users.Where(u => u.UserId == id).FirstOrDefault();
                user1.PubId = user.PubId;
                user1.FirstName = user.FirstName;
                user1.LastName = user.LastName;
                user1.MiddleName = user.MiddleName;
                user1.Source = user.Source;
                user1.HireDate = user.HireDate;
                context.Users.Update(user1);
                context.SaveChanges();
                return user1;
            }
        }

        // delete user
        public static void DeleteUser(int id)
        {
            using (var context = new BookStoreContext())
            {
                User user = context.Users.Where(u => u.UserId == id).FirstOrDefault();
                context.Users.Remove(user);
                context.SaveChanges();
            }
        }


    }
}
