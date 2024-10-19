
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
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly WellMeetDbContext _context;

        public AppointmentRepository(WellMeetDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Appointment>> GetAllAsync()
        {
            return await _context.Appointments.ToListAsync();
        }

        public async Task<Appointment> GetById(int id)
        {
            return await _context.Appointments.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<IEnumerable<Appointment>> GetByUserId(int id)
        {
            return await _context.Appointments.Where(u => u.UserId == id).ToListAsync();
        }

        public async Task<IEnumerable<Appointment>> GetByPsychiatristId(int id)
        {
            return await _context.Appointments.Where(u => u.PsychiatristId == id).ToListAsync();
        }

        public async Task AddAsync(Appointment appointment)
        {
            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Appointment appointment)
        {
            _context.Appointments.Update(appointment);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var existingAppointment = await _context.Appointments.FindAsync(id);
            _context.Appointments.Remove(existingAppointment);
            await _context.SaveChangesAsync();
        }
    }
}
