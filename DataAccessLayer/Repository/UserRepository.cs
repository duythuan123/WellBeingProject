using DataAccessLayer.Context;
using DataAccessLayer.Entities;
using DataAccessLayer.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly WellMeetDbContext _context;

        public UserRepository(WellMeetDbContext context)
        {
            _context = context;
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User> GetUserById(int id)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task AddUserAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateUserAsync(User user, int id)
        {
            var existingUser = await _context.Users.FindAsync(id);
            var existingEmail = await _context.Users.FirstOrDefaultAsync(u => u.Email == user.Email);

            if(existingEmail != null && existingUser.Email != user.Email)
            {
                throw new Exception("Email is existed");
            }

            if (existingUser != null)
            {
                // Update
                existingUser.Fullname = user.Fullname;
                existingUser.Email = user.Email;
                existingUser.DateOfBirth = user.DateOfBirth;
                existingUser.Phonenumber = user.Phonenumber;
                existingUser.Address = user.Address;
                existingUser.Gender = user.Gender;
                existingUser.Role = user.Role;
                existingUser.UserImage = user.UserImage;

                // Mark the existing customer entity as modified
                _context.Entry(existingUser).State = EntityState.Modified;

                // Save the changes
                _context.SaveChanges();
            }

            await _context.SaveChangesAsync();
        }

        public async Task SavePasswordResetTokenAsync(User user, string token, DateTime expiration)
        {
            var newToken = new Token
            {
                PasswordResetToken = token,
                PasswordResetTokenExpiration = expiration,
                UserId = user.Id,
                User = user
            };

            _context.Tokens.Add(newToken);
            await _context.SaveChangesAsync();
        }

        public async Task UpdatePasswordAsync(User user, int id)
        {
            var existingUser = await _context.Users.FindAsync(id);

            if (existingUser != null)
            {
                // Update password
                existingUser.Password = user.Password;

                // Mark the existing user entity as modified
                _context.Entry(existingUser).State = EntityState.Modified;

                // Save the changes
                await _context.SaveChangesAsync();
            }
        }

        public async Task<User> GetUserByPasswordResetTokenAsync(string token)
        {
            var tokenEntity = await _context.Tokens
                .Include(t => t.User)
                .FirstOrDefaultAsync(t => t.PasswordResetToken == token && t.PasswordResetTokenExpiration > DateTime.UtcNow);

            return tokenEntity?.User;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync(string searchTerm)
        {
            //searchTerm is null or empty return all users
            if (string.IsNullOrEmpty(searchTerm))
            {
                return await _context.Users.ToListAsync();
            }

            //filter users by Fullname or Email
            return await _context.Users
                .Where(u => u.Fullname.Contains(searchTerm) || u.Email.Contains(searchTerm))
                .ToListAsync();
        }
    }
}
