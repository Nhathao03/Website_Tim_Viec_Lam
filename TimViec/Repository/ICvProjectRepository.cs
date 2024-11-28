using TimViec.Models;

namespace TimViec.Repository
{
    public interface ICvProjectRepository
    {
        Task<IEnumerable<CVProject>> GetAllAsync();
        Task<CVProject> GetByIdAsync(int id);
        Task AddAsync(CVProject cv_project);
        Task UpdateAsync(CVProject cv_project);
        Task DeleteAsync(int id);
    }
}
