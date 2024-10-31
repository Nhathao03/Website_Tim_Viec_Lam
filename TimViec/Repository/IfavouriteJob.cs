using TimViec.Models;

namespace TimViec.Repository
{
    public interface IfavouriteJob
    {
        Task<IEnumerable<favourite_job>> GetAllAsync();
        Task<favourite_job> GetByIdAsync(int id);
        Task AddAsync(favourite_job favourite_Job);
        Task UpdateAsync(favourite_job favourite_Job);
        Task DeleteAsync(int id);
    }
}
