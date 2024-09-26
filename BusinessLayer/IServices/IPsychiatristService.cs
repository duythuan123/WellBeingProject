using BusinessLayer.Models.Request;
using BusinessLayer.Models.Response;
using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.IServices
{
    public interface IPsychiatristService
    {
        Task<BaseResponseModel<PsychiatristResponseModel>> GetPsychiatristDetailAsync(int userId);
        Task<BaseResponseModel<PsychiatristResponseModel>> UpdatePsychiatristAsync(PsychiatristRequestModelForUpdate request, int userId);
    }
}
