using BusinessLayer.Models.Request;
using BusinessLayer.Models.Response;
using DataAccessLayer.Entities;

namespace BusinessLayer.IServices
{
    public interface ITimeSlotService
    {
        Task<BaseResponseModel<IEnumerable<TimeSlotResponseModel>>> GetAllTimeSlotsAsync();
        Task<BaseResponseModel<TimeSlotModel>> GetTimeSlotByIdAsync(int id);
        Task<BaseResponseModel<List<AddTimeSlotResponseModel>>> AddTimeSlotAsync(AddTimeSlotRequestModel request, int userId);
        Task<BaseResponseModel<TimeSlotResponseModel>> UpdateTimeSlotAsync(TimeSlotRequestModel request, int timeSlotId);
        Task<BaseResponseModel<TimeSlot>> DeleteTimeSlotAsync(int timeSlotId);

    }
}
