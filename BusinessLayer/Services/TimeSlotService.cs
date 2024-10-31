using BusinessLayer.IServices;
using BusinessLayer.Models.Request;
using BusinessLayer.Models.Response;
using DataAccessLayer.Entities;
using DataAccessLayer.IRepository;

namespace BusinessLayer.Services
{
    public class TimeSlotService : ITimeSlotService
    {
        private readonly ITimeSlotRepository _tsRepo;
        private readonly IPsychiatristRepository _pRepo;

        public TimeSlotService(ITimeSlotRepository repo, IPsychiatristRepository pRepo)
        {
            _tsRepo = repo;
            _pRepo = pRepo;
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        public async Task<BaseResponseModel<IEnumerable<TimeSlotResponseModel>>> GetAllTimeSlotsAsync()
        {
            var timeslots = await _tsRepo.GetAllTimeSlotAsync();

            var response = timeslots.Select(ts => new TimeSlotResponseModel
            {
                StartTime = ts.StartTime,
                EndTime = ts.EndTime,
                SlotDate = ts.SlotDate,
                PsychiatristName = ts.Psychiatrist.User.Fullname,
            }).ToList();

            return new BaseResponseModel<IEnumerable<TimeSlotResponseModel>>
            {
                Code = 200,
                Message = "Get all time slots success!",
                Data = response
            };
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public async Task<BaseResponseModel<TimeSlotModel>> GetTimeSlotByIdAsync(int id)
        {
            var timeSlot = await _tsRepo.GetTimeSlotByIdAsync(id); // Giả sử _tsRepo là repository cho TimeSlot

            if (timeSlot == null)
            {
                return new BaseResponseModel<TimeSlotModel>
                {
                    Code = 404,
                    Message = "Time slot not found!",
                    Data = null
                };
            }

            var responseModel = new TimeSlotModel
            {
                TimeSlotId = timeSlot.TimeSlotId,
                StartTime = timeSlot.StartTime,
                EndTime = timeSlot.EndTime,
                SlotDate = timeSlot.SlotDate,
            };

            return new BaseResponseModel<TimeSlotModel>
            {
                Code = 200,
                Message = "Time slot found successfully!",
                Data = responseModel
            };
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        public async Task<BaseResponseModel<List<AddTimeSlotResponseModel>>> AddTimeSlotAsync(AddTimeSlotRequestModel request, int psychiatristId)
        {
            var responseList = new List<AddTimeSlotResponseModel>();

            // Kiểm tra xem psychiatrist có tồn tại không
            var existedPsychiatrist = await _pRepo.GetPsychiatristById(psychiatristId);
            if (existedPsychiatrist == null)
            {
                return new BaseResponseModel<List<AddTimeSlotResponseModel>>
                {
                    Code = 500,
                    Message = "Psychiatrist not exists!",
                    Data = null
                };
            }

            // Loop để tạo TimeSlot cho 30 ngày tiếp theo
            for (int i = 0; i < 30; i++)
            {
                var currentSlotDate = request.SlotDate.AddDays(i);

                // Kiểm tra trùng lặp TimeSlot
                var existingTimeSlot = await _tsRepo.GetTimeSlotByDetailsAsync(request.StartTime, currentSlotDate, psychiatristId);
                if (existingTimeSlot == null)
                {
                    // Tạo mới TimeSlot với Status là "AVAILABLE"
                    var newTimeSlot = new TimeSlot
                    {
                        StartTime = request.StartTime,
                        EndTime = request.EndTime,
                        SlotDate = currentSlotDate,
                        PsychiatristId = psychiatristId,
                        Status = "AVAILABLE"
                    };

                    // Thêm TimeSlot vào database
                    await _tsRepo.AddTimeSlotAsync(newTimeSlot);

                    // Thêm vào response list
                    responseList.Add(new AddTimeSlotResponseModel
                    {
                        StartTime = newTimeSlot.StartTime,
                        EndTime = newTimeSlot.EndTime,
                        SlotDate = newTimeSlot.SlotDate,
                        Status = newTimeSlot.Status
                    });
                }
            }

            return new BaseResponseModel<List<AddTimeSlotResponseModel>>
            {
                Code = 200,
                Message = "Time slots created successfully for the next 30 days.",
                Data = responseList
            };
        }


        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public async Task<BaseResponseModel<TimeSlotResponseModel>> UpdateTimeSlotAsync(TimeSlotRequestModel request, int timeSlotId)
        {
            // Tìm TimeSlot theo ID
            var timeSlot = await _tsRepo.GetTimeSlotByIdAsync(timeSlotId);

            if (timeSlot == null)
            {
                return new BaseResponseModel<TimeSlotResponseModel>
                {
                    Code = 404,
                    Message = "Time slot not found!",
                    Data = null
                };
            }

            // Cập nhật giá trị mới cho TimeSlot
            timeSlot.StartTime = request.StartTime;
            timeSlot.EndTime = request.EndTime;
            timeSlot.SlotDate = request.SlotDate;

            // Lưu thay đổi vào cơ sở dữ liệu
            await _tsRepo.UpdateTimeSlotAsync(timeSlot);

            return new BaseResponseModel<TimeSlotResponseModel>
            {
                Code = 200,
                Message = "Time slot updated successfully!",
                Data = new TimeSlotResponseModel
                {
                    StartTime = timeSlot.StartTime,
                    EndTime = timeSlot.EndTime,
                    SlotDate = timeSlot.SlotDate,
                }
            };
        }


        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public async Task<BaseResponseModel<TimeSlot>> DeleteTimeSlotAsync(int timeSlotId)
        {
            var timeSlot = await _tsRepo.GetTimeSlotByIdAsync(timeSlotId);  // Lấy đối tượng TimeSlot

            if (timeSlot == null)
            {
                return new BaseResponseModel<TimeSlot>
                {
                    Code = 404,
                    Message = "Time slot not found.",
                    Data = null
                };
            }

            try
            {
                await _tsRepo.DeleteTimeSlotAsync(timeSlot);  // Truyền đối tượng TimeSlot vào thay vì ID
                return new BaseResponseModel<TimeSlot>
                {
                    Code = 200,
                    Message = "Time slot deleted successfully.",
                    Data = timeSlot
                };
            }
            catch (Exception ex)
            {
                return new BaseResponseModel<TimeSlot>
                {
                    Code = 500,
                    Message = "An error occurred while deleting the time slot: " + ex.Message,
                    Data = null
                };
            }
        }
    }
}
