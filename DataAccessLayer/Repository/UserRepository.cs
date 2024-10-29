using DataAccessLayer.Context;
using DataAccessLayer.Entities;
using DataAccessLayer.Enums;
using DataAccessLayer.IRepository;
using Microsoft.EntityFrameworkCore;

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

        public async Task UpdateUserAsync(User user)
        {
            _context.Users.Update(user);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(int id)
        {
            var existingUser = await _context.Users.FindAsync(id);
            _context.Users.Remove(existingUser);
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

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {

            return await _context.Users.ToListAsync();
        }

        public async Task<int> GetTotalAccount()
        {
            // Get the total number of users
            return  await _context.Users.CountAsync();
        }

        public async Task<double> GetTotalRevenue()
        {
            // Calculate total revenue from payments
            return (double)await _context.Payments
                .Where(p => p.PaymentStatus == PaymentStatus.SUCCESS.ToString())
                .SumAsync(p => p.Amount ?? 0);
        }

        public async Task<int> GetTotalAppointments()
        {
            // Get the total number of appointments
            return await _context.Appointments.CountAsync();
        }

    }
}
