using TimViec.Models;

namespace TimViec.Respository
{
    public interface ISkillRespository
    {
        Task<IEnumerable<Skill>> GetAllAsync();
        Task<Skill> GetByIdAsync(int id);
        Task AddAsync(Skill skill);
        Task UpdateAsync(Skill skill);
        Task DeleteAsync(int id);
    }
}
