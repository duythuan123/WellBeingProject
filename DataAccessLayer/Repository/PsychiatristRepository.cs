using DataAccessLayer.Context;
using DataAccessLayer.Entities;
using DataAccessLayer.IRepository;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repository
{
    public class PsychiatristRepository : IPsychiatristRepository
    {
        private readonly WellMeetDbContext _context;

        public PsychiatristRepository(WellMeetDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Psychiatrist>> GetAllPsychiatristsAsync()
        {
            return await _context.Psychiatrists
                .Include(p => p.User) // Bao gồm thông tin người dùng nếu cần
                .Include(p => p.TimeSlots) // Bao gồm danh sách time slot
                .ToListAsync(); // Lấy tất cả bác sĩ tâm lý
        }

        public async Task<Psychiatrist> GetPsychiatristById(int id)
        {
            return await _context.Psychiatrists.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<Psychiatrist> GetPsychiatristByUserId(int userId)
        {
            return await _context.Psychiatrists
                .Include(p => p.User)
                .Include(p => p.TimeSlots)
                .FirstOrDefaultAsync(p => p.UserId == userId);
        }

        public async Task<Psychiatrist> GetPsychiatristByEmail(string email)
        {
            return await _context.Psychiatrists
                .Include(p => p.User)
                .FirstOrDefaultAsync(p => p.User.Email == email);
        }

        public async Task<Psychiatrist> GetPsychiatristByPhoneNumber(string phoneNumber)
        {
            return await _context.Psychiatrists
                .Include(p => p.User)
                .FirstOrDefaultAsync(p => p.User.Phonenumber == phoneNumber);
        }

        public async Task UpdatePsychiatristAsync(Psychiatrist psychiatrist, int userId)
        {
            _context.Psychiatrists.Update(psychiatrist); // Update Psychiatrist
            _context.Users.Update(psychiatrist.User);    // Update User thông qua Psychiatrist
            await _context.SaveChangesAsync();
        }

        public async Task AddAsync(Psychiatrist psychiatrist)
        {
            _context.Psychiatrists.Add(psychiatrist);
            await _context.SaveChangesAsync();
        }

        public async Task DeletePsychiatristAsync(Psychiatrist psychiatrist)
        {
            // Remove related appointments first
            var appointments = _context.Appointments.Where(a => a.PsychiatristId == psychiatrist.Id);
            _context.Appointments.RemoveRange(appointments);

            // Remove related time slots
            var timeSlots = _context.TimeSlots.Where(t => t.PsychiatristId == psychiatrist.Id);
            _context.TimeSlots.RemoveRange(timeSlots);

            // Remove the psychiatrist
            _context.Psychiatrists.Remove(psychiatrist);
            
            // Remove the associated user
            if (psychiatrist.User != null)
            {
                _context.Users.Remove(psychiatrist.User);
            }

            await _context.SaveChangesAsync();
        }
    }
}
