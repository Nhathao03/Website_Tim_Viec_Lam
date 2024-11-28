using TimViec.Models;

namespace TimViec.Repository
{
    public interface ICvExperienceRepository
    {
        Task<IEnumerable<CVExperience>> GetAllAsync();
        Task<CVExperience> GetByIdAsync(int id);
        Task AddAsync(CVExperience cv_experience);
        Task UpdateAsync(CVExperience cv_experience);
        Task DeleteAsync(int id);
    }
}
