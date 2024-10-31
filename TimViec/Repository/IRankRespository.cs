  using TimViec.Models;

namespace TimViec.Respository
{
    public interface IRankRespository
    {
        Task<IEnumerable<Rank>> GetAllAsync();
        Task<Rank> GetByIdAsync(int id);
        Task AddAsync(Rank rank);
        Task UpdateAsync(Rank rank);
        Task DeleteAsync(int id);
    }
}
