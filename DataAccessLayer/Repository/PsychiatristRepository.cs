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

        public async Task<Psychiatrist> GetPsychiatristById(int id)
        {
            return await _context.Psychiatrists.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task UpdatePsychiatristAsync(Psychiatrist psychiatrist, int id)
        {
            // Load the psychiatrist along with the related user
            var existingPsychiatrist = await _context.Psychiatrists
                .Include(p => p.User) // Include the User entity
                .FirstOrDefaultAsync(p => p.Id == id);

            if (existingPsychiatrist == null)
            {
                throw new Exception("Psychiatrist not found");
            }

            var existingEmail = await _context.Users.FirstOrDefaultAsync(u => u.Email == psychiatrist.User.Email);
            var existingPhone = await _context.Users.FirstOrDefaultAsync(u => u.Phonenumber == psychiatrist.User.Phonenumber);

            if (existingEmail != null && existingPsychiatrist.User.Email != psychiatrist.User.Email)
            {
                throw new Exception("Email already exists");
            }

            if (existingPhone != null && existingPsychiatrist.User.Phonenumber != psychiatrist.User.Phonenumber)
            {
                throw new Exception("Phone number already exists");
            }
            
            // Update the psychiatrist information           
            existingPsychiatrist.Specialization = psychiatrist.Specialization;
            existingPsychiatrist.Bio = psychiatrist.Bio;
            existingPsychiatrist.Experience = psychiatrist.Experience;
            existingPsychiatrist.Location = psychiatrist.Location;

            // Update the user information
            existingPsychiatrist.User.Fullname = psychiatrist.User.Fullname;
            existingPsychiatrist.User.Email = psychiatrist.User.Email;
            existingPsychiatrist.User.Phonenumber = psychiatrist.User.Phonenumber;
            existingPsychiatrist.User.DateOfBirth = psychiatrist.User.DateOfBirth;
            existingPsychiatrist.User.Address = psychiatrist.User.Address;
            existingPsychiatrist.User.Gender = psychiatrist.User.Gender;

            // Mark both entities as modified
            _context.Entry(existingPsychiatrist).State = EntityState.Modified;
            _context.Entry(existingPsychiatrist.User).State = EntityState.Modified;

            // Save the changes
            await _context.SaveChangesAsync();
        }

    }
}
