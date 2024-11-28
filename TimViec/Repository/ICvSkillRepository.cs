using TimViec.Models;

namespace TimViec.Repository
{
    public interface ICvSkillRepository
    {
        Task<IEnumerable<CVSkill>> GetAllAsync();
        Task<CVSkill> GetByIdAsync(int id);
        Task AddAsync(CVSkill cv_skill);
        Task UpdateAsync(CVSkill cv_skill);
        Task DeleteAsync(int id);
    }
}
