using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.IRepository
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllUsersAsync(string searchTerm);
        Task<User> GetUserByEmailAsync(string email);
        Task<User> GetUserById(int id);
        Task AddUserAsync(User user);
        Task UpdateUserAsync(User user, int id);
        Task SavePasswordResetTokenAsync(User user, string token, DateTime expiration);
        Task<User> GetUserByPasswordResetTokenAsync(string token);
        Task UpdatePasswordAsync(User user, int id);
    }
}
