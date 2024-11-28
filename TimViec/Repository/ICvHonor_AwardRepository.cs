using TimViec.Models;

namespace TimViec.Repository
{
    public interface ICvHonor_AwardRepository
    {
        Task<IEnumerable<CVHonors_Awards>> GetAllAsync();
        Task<CVHonors_Awards> GetByIdAsync(int id);
        Task AddAsync(CVHonors_Awards honor_award);
        Task UpdateAsync(CVHonors_Awards honor_award);
        Task DeleteAsync(int id);
    }
}
