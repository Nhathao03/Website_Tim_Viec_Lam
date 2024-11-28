using TimViec.Models;

namespace TimViec.Repository
{
    public interface ICvActivityRepository
    {
        Task<IEnumerable<CVActivities>> GetAllAsync();
        Task<CVActivities> GetByIdAsync(int id);
        Task AddAsync(CVActivities cv_activity);
        Task UpdateAsync(CVActivities cv_activity);
        Task DeleteAsync(int id);
    }
}
