using AutoMapper;
using BusinessLayer.IServices;
using BusinessLayer.Models.Request;
using BusinessLayer.Models.Response;
using DataAccessLayer.Entities;
using DataAccessLayer.Enums;
using DataAccessLayer.IRepository;

namespace BusinessLayer.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _repo;
        private readonly IMapper _mapper;
        private readonly ITimeSlotRepository _tsRepo;

        public AppointmentService(IAppointmentRepository repo, IMapper mapper, ITimeSlotRepository tsrepo, IAppointmentRepository appRepo)
        {
            _repo = repo;
            _mapper = mapper;
            _tsRepo = tsrepo;
        }

        public async Task<BaseResponseModel<AppointmentResponseModel>> AddAsync(AppointmentRequestModel request)
        {
            var timeSlot = await _tsRepo.GetTimeSlotByIdAsync((int)request.TimeSlotId);

            if (timeSlot.Status == TimeslotStatus.BOOKED.ToString())
            {
                return new BaseResponseModel<AppointmentResponseModel>
                {
                    Code = 404,
                    Message = "Time slot is not in available status",
                    Data = null
                };
            }

            var newAppointment = _mapper.Map<Appointment>(request);

            try
            {
                await _repo.AddAsync(newAppointment);
            }
            catch (Exception ex)
            {
                return new BaseResponseModel<AppointmentResponseModel>
                {
                    Code = 500,
                    Message = ex.Message,
                    Data = null
                };
            }

            return new BaseResponseModel<AppointmentResponseModel>
            {
                Code = 200,
                Message = "Appointment Created Success",
                Data = _mapper.Map<AppointmentResponseModel>(newAppointment)
            };
        }

        public async Task<BaseResponseModel<AppointmentResponseModel>> UpdateAsync(AppointmentRequestModelForUpdate request, int id)
        {
            var existedAppointment = await _repo.GetById(id);
            if (existedAppointment == null)
            {
                return new BaseResponseModel<AppointmentResponseModel>
                {
                    Code = 404,
                    Message = "Appointment not exists",
                    Data = null
                };
            }

            _mapper.Map(request, existedAppointment);

            try
            {
                await _repo.UpdateAsync(existedAppointment);
            }
            catch (Exception ex)
            {
                return new BaseResponseModel<AppointmentResponseModel>
                {
                    Code = 500,
                    Message = ex.Message,
                    Data = null
                };
            }

            return new BaseResponseModel<AppointmentResponseModel>
            {
                Code = 200,
                Message = "Appointment Updated Success",
                Data = _mapper.Map<AppointmentResponseModel>(existedAppointment)
            };
        }

        public async Task<BaseResponseModel<AppointmentResponseModel>> FinishAppointmentAsync(int id)
        {
            var existedAppointment = await _repo.GetById(id);
            if (existedAppointment == null)
            {
                return new BaseResponseModel<AppointmentResponseModel>
                {
                    Code = 404,
                    Message = "Appointment not exists",
                    Data = null
                };
            }

            var existedTimeslot = await _tsRepo.GetTimeSlotByIdAsync((int)existedAppointment.TimeSlotId);

            existedTimeslot.Status = TimeslotStatus.AVAILABLE.ToString();
            existedAppointment.Status = AppointmentStatus.COMPLETED.ToString();

            try
            {
                await _repo.UpdateAsync(existedAppointment);
                await _tsRepo.UpdateTimeSlotAsync(existedTimeslot);

            }
            catch (Exception ex)
            {
                return new BaseResponseModel<AppointmentResponseModel>
                {
                    Code = 500,
                    Message = ex.Message,
                    Data = null
                };
            }

            return new BaseResponseModel<AppointmentResponseModel>
            {
                Code = 200,
                Message = "Appointment Finished Success",
                Data = _mapper.Map<AppointmentResponseModel>(existedAppointment)
            };
        }

        public async Task<BaseResponseModel<AppointmentResponseModel>> GetByIdAsync(int id)
        {
            var existedAppointment = await _repo.GetById(id);
            if (existedAppointment == null)
            {
                return new BaseResponseModel<AppointmentResponseModel>
                {
                    Code = 404,
                    Message = "Appointment not exists",
                    Data = null
                };
            }

            return new BaseResponseModel<AppointmentResponseModel>
            {
                Code = 200,
                Message = "Get Appointment Detail Success",
                Data = _mapper.Map<AppointmentResponseModel>(existedAppointment)
            };
        }

        public async Task<BaseResponseModel<IEnumerable<AppointmentResponseModel>>> GetByUserIdAsync(int id)
        {
            var appointments = await _repo.GetByUserId(id);
            var appointmentResponseModels = _mapper.Map<IEnumerable<AppointmentResponseModel>>(appointments);

            if (appointments.Count() == 0)
            {
                return new BaseResponseModel<IEnumerable<AppointmentResponseModel>>
                {
                    Code = 200,
                    Message = "No Appointments in the server have this UserId",
                    Data = appointmentResponseModels
                };
            }

            return new BaseResponseModel<IEnumerable<AppointmentResponseModel>>
            {
                Code = 200,
                Message = "Appointments retrieved successfully",
                Data = appointmentResponseModels
            };
        }

        public async Task<BaseResponseModel<IEnumerable<AppointmentResponseModel>>> GetByPsychiatristIdAsync(int id)
        {
            var appointments = await _repo.GetByPsychiatristId(id);
            var appointmentResponseModels = _mapper.Map<IEnumerable<AppointmentResponseModel>>(appointments);

            if (appointments.Count() == 0)
            {
                return new BaseResponseModel<IEnumerable<AppointmentResponseModel>>
                {
                    Code = 200,
                    Message = "No Appointments in the server have this PsychiatristId",
                    Data = appointmentResponseModels
                };
            }

            return new BaseResponseModel<IEnumerable<AppointmentResponseModel>>
            {
                Code = 200,
                Message = "Appointments retrieved successfully",
                Data = appointmentResponseModels
            };
        }

        public async Task<BaseResponseModel<IEnumerable<AppointmentResponseModel>>> GetAllAsync()
        {
            var appointments = await _repo.GetAllAsync();
            var appointmentResponseModels = _mapper.Map<IEnumerable<AppointmentResponseModel>>(appointments);

            if (appointments.Count() == 0)
            {
                return new BaseResponseModel<IEnumerable<AppointmentResponseModel>>
                {
                    Code = 200,
                    Message = "No Appointments in the server",
                    Data = appointmentResponseModels
                };
            }

            return new BaseResponseModel<IEnumerable<AppointmentResponseModel>>
            {
                Code = 200,
                Message = "Appointments retrieved successfully",
                Data = appointmentResponseModels
            };
        }

        public async Task<BaseResponseModel> DeleteAsync(int id)
        {
            var appointment = await _repo.GetById(id);
            if (appointment == null)
            {
                return new BaseResponseModel
                {
                    Code = 404,
                    Message = "Appointment not found",
                };
            }

            try
            {
                await _repo.DeleteAsync(id);

            }
            catch (Exception ex)
            {
                return new BaseResponseModel
                {
                    Code = 500,
                    Message = ex.Message,
                };
            }

            return new BaseResponseModel
            {
                Code = 200,
                Message = "Appointment is Deleted Successfully",
            };
        }
    }
}
