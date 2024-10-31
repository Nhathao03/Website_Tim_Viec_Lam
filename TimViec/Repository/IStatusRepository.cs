using TimViec.Models;
using TimViec.ViewModel;

namespace TimViec.Repository
{
    public interface IStatusRepository
    {
        Task<IEnumerable<StatusJob>> GetAllAsync();
        Task<StatusJob> GetByIdAsync(int id); 
        Task AddAsync(StatusJob statusJob);
        Task UpdateAsync(StatusJob statusJob);
        Task DeleteAsync(int id);
        List<StatusViewModel> GetListJobByEmail(string email);
        List<CompanyCheckStatusViewModel> CompanyCheckStatus(string email);


    }
}
