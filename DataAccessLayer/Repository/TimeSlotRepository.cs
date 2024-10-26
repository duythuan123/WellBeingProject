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
    public class TimeSlotRepository : ITimeSlotRepository
    {
        private readonly WellMeetDbContext _context;

        public TimeSlotRepository(WellMeetDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TimeSlot>> GetAllTimeSlotAsync()
        {
            return await _context.TimeSlots
                   .Include(ts => ts.Psychiatrist)
                   .ThenInclude(p => p.User)               
                   .ToListAsync();
        }

        public async Task<TimeSlot> GetTimeSlotByDetailsAsync(TimeSpan startTime, DateTime slotDate, int psychiatristId)
        {
            return await _context.TimeSlots
                .FirstOrDefaultAsync(ts => ts.StartTime == startTime 
                                        && ts.SlotDate == slotDate
                                        && ts.PsychiatristId == psychiatristId);
        }

        public async Task<TimeSlot> GetTimeSlotByPsychiatristId(int psychiatristId)
        {
            return await _context.TimeSlots
                .FirstOrDefaultAsync(p => p.PsychiatristId == psychiatristId);
        }

        public async Task<TimeSlot?> GetTimeSlotByIdAsync(int id)
        {
            return await _context.TimeSlots.FindAsync(id);
        }

        public async Task AddTimeSlotAsync(TimeSlot timeSlot)
        {
            _context.TimeSlots.Add(timeSlot);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTimeSlotAsync(TimeSlot timeSlot)
        {
            _context.TimeSlots.Update(timeSlot);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTimeSlotAsync(TimeSlot timeSlot)
        {
            _context.TimeSlots.Remove(timeSlot);
            await _context.SaveChangesAsync();
        }
    }
}
