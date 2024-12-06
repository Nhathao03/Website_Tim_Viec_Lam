using TimViec.Models;

namespace TimViec.Respository
{
    public interface ISectionRespository
    {
        Task<IEnumerable<Sections>> GetAllAsync();
        Task<Sections> GetByIdAsync(int id);
        Task AddAsync(Sections sections);
        Task UpdateAsync(Sections sections);
        Task DeleteAsync(int id);
    }
}
