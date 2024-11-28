using TimViec.Models;

namespace TimViec.Repository
{
    public interface ICvCertificationRepository
    {
        Task<IEnumerable<CVCertifications>> GetAllAsync();
        Task<CVCertifications> GetByIdAsync(int id);
        Task AddAsync(CVCertifications cv_certification);
        Task UpdateAsync(CVCertifications cv_certification);
        Task DeleteAsync(int id);
    }
}
