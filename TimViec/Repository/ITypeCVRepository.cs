using TimViec.Models;

namespace TimViec.Repository
{
    public interface ITypeCVRepository
    {
        Task<IEnumerable<TypeCV>> GetAllAsync();
        Task<TypeCV> GetByIdAsync(int id);
        Task AddAsync(TypeCV typeCV);
        Task UpdateAsync(TypeCV typeCV);
        Task DeleteAsync(int id);
    }
}
