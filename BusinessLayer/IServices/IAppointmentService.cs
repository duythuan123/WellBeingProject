using BusinessLayer.Models.Request;
using BusinessLayer.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.IServices
{
    public interface IAppointmentService
    {
        Task<BaseResponseModel<IEnumerable<AppointmentResponseModel>>> GetAllAsync();
        Task<BaseResponseModel<AppointmentResponseModel>> AddAsync(AppointmentRequestModel request);
        Task<BaseResponseModel<AppointmentResponseModel>> UpdateAsync(AppointmentRequestModelForUpdate request, int id);
        Task<BaseResponseModel> DeleteAsync(int id);
        Task<BaseResponseModel<AppointmentResponseModel>> GetByIdAsync(int id);
        Task<BaseResponseModel<IEnumerable<AppointmentResponseModel>>> GetByUserIdAsync(int id);
        Task<BaseResponseModel<IEnumerable<AppointmentResponseModel>>> GetByPsychiatristIdAsync(int id);
    }
}
