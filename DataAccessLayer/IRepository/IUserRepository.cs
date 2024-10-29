using DataAccessLayer.Entities;

namespace DataAccessLayer.IRepository
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User> GetUserByEmailAsync(string email);
        Task<User> GetUserById(int id);
        Task AddUserAsync(User user);
        Task UpdateUserAsync(User user);
        Task DeleteUserAsync(int id);
        Task SavePasswordResetTokenAsync(User user, string token, DateTime expiration);
        Task<User> GetUserByPasswordResetTokenAsync(string token);
        Task UpdatePasswordAsync(User user, int id);
        Task<int> GetTotalAccount();
        Task<double> GetTotalRevenue();
        Task<int> GetTotalAppointments();
    }
}
