using DataAccessLayer.Entities;

namespace DataAccessLayer.IRepository
{
    public interface ITimeSlotRepository
    {
        Task<IEnumerable<TimeSlot>> GetAllTimeSlotAsync();
        Task<TimeSlot> GetTimeSlotByDetailsAsync(TimeSpan startTime, DateTime slotDate, int psychiatristId);
        Task<TimeSlot?> GetTimeSlotByIdAsync(int id);
        Task<TimeSlot> GetTimeSlotByPsychiatristId(int psychiatristId);
        Task AddTimeSlotAsync(TimeSlot timeSlot);
        Task UpdateTimeSlotAsync(TimeSlot timeSlot);
        Task DeleteTimeSlotAsync(TimeSlot timeSlot);
    }
}
