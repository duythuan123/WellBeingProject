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
        Task<BaseResponseModel<Psychiatrist>> UpdatePsychiatristAsync(PsychiatristRequestModelForUpdate request, int id);
    }
}
