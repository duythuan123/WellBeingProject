using DataAccessLayer.Entities;

namespace DataAccessLayer.IRepository
{
    public interface IAppointmentRepository
    {
        Task<IEnumerable<Appointment>> GetAllAsync();
        Task<Appointment> GetById(int id);
        Task<IEnumerable<Appointment>> GetByUserId(int id);
        Task<IEnumerable<Appointment>> GetByPsychiatristId(int id);
        Task AddAsync(Appointment appointment);
        Task UpdateAsync(Appointment appointment);
        Task DeleteAsync(int id);
        Task AddPaymentAsync(Payment payment);
    }
}
