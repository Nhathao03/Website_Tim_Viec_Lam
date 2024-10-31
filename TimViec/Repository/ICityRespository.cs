using TimViec.Models;

namespace TimViec.Respository
{
    public interface ICityRespository
    {
        Task<IEnumerable<City>> GetAllAsync();
        Task<City> GetByIdAsync(int id);
        Task AddAsync(City city);
        Task UpdateAsync(City city);
        Task DeleteAsync(int id);
    }
}
