using DataAccessLayer.Entities;

namespace DataAccessLayer.IRepository
{
    public interface IPsychiatristRepository
    {
        Task<IEnumerable<Psychiatrist>> GetAllPsychiatristsAsync();
        Task<Psychiatrist> GetPsychiatristById(int id);
        Task<Psychiatrist> GetPsychiatristByUserId(int userId);
        Task<Psychiatrist> GetPsychiatristByEmail(string email);
        Task<Psychiatrist> GetPsychiatristByPhoneNumber(string phoneNumber);
        Task AddAsync(Psychiatrist psychiatrist);
        Task UpdatePsychiatristAsync(Psychiatrist psychiatrist, int userId);
        Task DeletePsychiatristAsync(Psychiatrist psychiatrist);
    }
}
