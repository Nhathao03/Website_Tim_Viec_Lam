using TimViec.Models;

namespace TimViec.Repository
{
    public interface ICvDetailsRepository
    {
        Task<IEnumerable<CVDetails>> GetAllAsync();
        Task<CVDetails> GetByIdAsync(int id);
        Task AddAsync(CVDetails cv_detail);
        Task UpdateAsync(CVDetails cv_detail);
        Task DeleteAsync(int id);
    }
}
