using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.IRepository
{
    public interface IPsychiatristRepository
    {
        Task<IEnumerable<Psychiatrist>> GetAllPsychiatristsAsync();
        Task<Psychiatrist> GetPsychiatristById(int id);
        Task<Psychiatrist> GetPsychiatristByUserId(int userId);
        Task<Psychiatrist> GetPsychiatristByEmail(string email);
        Task<Psychiatrist> GetPsychiatristByPhoneNumber(string phoneNumber);
        Task UpdatePsychiatristAsync(Psychiatrist psychiatrist, int userId);
    }
}
