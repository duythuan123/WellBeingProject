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
    public class PsychiatristRepository : IPsychiatristRepository
    {
        private readonly AppDbContext _context;

        public PsychiatristRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Psychiatrist>> GetAllPsychiatristsAsync()
        {
            return await _context.Psychiatrists
                .Include(p => p.User) // Bao gồm thông tin người dùng nếu cần
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

    }
}
