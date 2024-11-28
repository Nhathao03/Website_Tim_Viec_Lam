using TimViec.Models;

namespace TimViec.Repository
{
    public interface ICvEducationRepository
    {
        Task<IEnumerable<CVEducation>> GetAllAsync();
        Task<CVEducation> GetByIdAsync(int id);
        Task AddAsync(CVEducation cv_education);
        Task UpdateAsync(CVEducation cv_education);
        Task DeleteAsync(int id);
    }
}
