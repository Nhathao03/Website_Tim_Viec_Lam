using TimViec.Models;

namespace TimViec.Repository
{
    public interface IfeedbackRepository
    {
        Task<IEnumerable<feedback>> GetAllAsync();
        Task<feedback> GetByIdAsync(int id);
        Task AddAsync(feedback feedback);
        Task UpdateAsync(feedback feedback);
        Task DeleteAsync(int id);
    }
}
