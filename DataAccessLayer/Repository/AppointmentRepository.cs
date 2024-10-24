﻿
using DataAccessLayer.Context;
using DataAccessLayer.Entities;
using DataAccessLayer.Enums;
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

            existingAppointment.PaymentId = null;
            _context.Entry(existingAppointment).State = EntityState.Modified;
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
