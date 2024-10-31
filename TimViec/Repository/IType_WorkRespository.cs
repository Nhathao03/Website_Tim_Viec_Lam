using TimViec.Models;

namespace TimViec.Respository
{
    public interface IType_WorkRespository
    {
        Task<IEnumerable<Type_work>> GetAllAsync();
        Task<Type_work> GetByIdAsync(int id);
        Task AddAsync(Type_work type_Work);
        Task UpdateAsync(Type_work type_Work);
        Task DeleteAsync(int id);
    }
}
