using BusinessLayer.Models.Request;
using BusinessLayer.Models.Response;

namespace BusinessLayer.IServices
{
    public interface IPsychiatristService
    {
        Task<BaseResponseModel<IEnumerable<PsychiatristResponseModel>>> GetAllPsychiatristsAsync();
        Task<BaseResponseModel<PsychiatristResponseModel>> GetPsychiatristDetailAsync(int userId);
        Task<BaseResponseModel<PsychiatristResponseModel>> UpdatePsychiatristAsync(PsychiatristRequestModelForUpdate request, int userId);
    }
}
