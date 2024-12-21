using TimViec.Models;
using TimViec.ViewModel;

namespace TimViec.Respository
{
    public interface ICVsRepository
    {
        Task<IEnumerable<CV>> GetAllAsync();
        Task<CV> GetByIdAsync(int id);
        Task AddAsync(CV cv);
        Task UpdateAsync(CV cv);
        Task DeleteAsync(int id);
        List<Get_CV_ByCvid_ViewModel> GetTemplates_by_ID_CV(int CV_ID);
        List<ManageCVofUser> ManageCV(string id);
    }
}
