
using DataAccessLayer.Context;
using DataAccessLayer.Entities;
using DataAccessLayer.Enums;
using DataAccessLayer.IRepository;
using Microsoft.EntityFrameworkCore;

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
            return await _context.Appointments.Include(p => p.Psychiatrist).ToListAsync();
        }

        public async Task<Appointment> GetById(int id)
        {
            return await _context.Appointments.Include(p => p.Psychiatrist).FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Appointment>> GetByUserId(int id)
        {
            return await _context.Appointments.Include(p => p.Psychiatrist).Where(p => p.UserId == id).ToListAsync();
        }

        public async Task<IEnumerable<Appointment>> GetByPsychiatristId(int id)
        {
            return await _context.Appointments.Include(p => p.Psychiatrist).Where(p => p.PsychiatristId == id).ToListAsync();
        }

        public async Task AddAsync(Appointment appointment)
        {
            _context.Appointments.Add(appointment);

            var timeslot = await _context.TimeSlots.FindAsync(appointment.TimeSlotId);

            timeslot.Status = TimeslotStatus.BOOKED.ToString();

            _context.Entry(timeslot).State = EntityState.Modified;

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
            var existingPayment = await _context.Payments.FindAsync(existingAppointment.PaymentId);
            var existingTimeSlot = await _context.TimeSlots.FindAsync(existingAppointment.TimeSlotId);

            existingAppointment.PaymentId = null;
            _context.Entry(existingAppointment).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            existingTimeSlot.Status = TimeslotStatus.AVAILABLE.ToString();
            _context.Entry(existingTimeSlot).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            _context.Appointments.Remove(existingAppointment);
            _context.Payments.Remove(existingPayment);

            await _context.SaveChangesAsync();
        }

        public async Task AddPaymentAsync(Payment payment)
        {
            _context.Payments.Add(payment);
            await _context.SaveChangesAsync();
        }
    }
}
