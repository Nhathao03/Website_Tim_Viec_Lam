using TimViec.Models;
using TimViec.ViewModel;

namespace TimViec.Repository
{
    public interface ITemplateRepository
    {
        Task<IEnumerable<Template>> GetAllAsync(); 
        Task<Template> GetByIdAsync(int id);
        Task AddAsync(Template template);
        Task UpdateAsync(Template template);
        Task DeleteAsync(int id);
        List<GetAllTemplate_ViewModel> GetAllInformation_Table_Template_and_TypeCV();
        List<ListTemplateCV> Get_ListTemplates();
        List<Get_template_by_category_ViewModel> Get_Template_By_Categories(int category_id);
    }
}
