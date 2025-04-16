using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomExceptions;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace Service
{
    public class UserRepository : IRepository<User>
    {
        private readonly BlogDbContext _context;

        public UserRepository(BlogDbContext context)
        {
            _context = context;
        }

        public User Create(User user)
        {
            // Check if a user with the same userid already exists
            if (_context.Users.Any(u => u.UserId == user.UserId))
            {
                throw new UserAlreadyExistsException($"User with User Id '{user.UserId}' already exists.");
            }

            _context.Users.Add(user);
            _context.SaveChanges();
            return user;
        }

        public User GetById(int id)
        {
            var userfound = _context.Users.Find(id);
            if (userfound != null)
            {
                return userfound;
            }
            else
            {
                throw new UserNotFoundException($"User with User ID {id} not found.");
            }
        }

        public IEnumerable<User> GetAll()
        {
            return _context.Users.ToList();
        }

        public void Update(User user)
        {
            var userfound = GetById(user.UserId);
            if (userfound != null)
            {
                userfound.Username = user.Username;
                userfound.Email = user.Email;
                _context.SaveChanges();
            }
            else
            {
                throw new UserNotFoundException($"Category with ID '{user.UserId}' not found.");
            }
        }

        public void Delete(int id)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserId == id);
            if (user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
            }
            else
            {
                throw new UserNotFoundException($"Category with ID '{user.UserId}' not found.");
            }
        }
    }
}
