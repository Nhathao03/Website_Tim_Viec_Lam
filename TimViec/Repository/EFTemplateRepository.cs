using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Plugins;
using TimViec.Data;
using TimViec.Models;
using TimViec.ViewModel;

namespace TimViec.Repository
{
    public class EFTemplateRepository : ITemplateRepository
    {
        private readonly ApplicationDbContext _context;
        public EFTemplateRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Template>> GetAllAsync()
        {
            return await _context.template.ToListAsync();
        }
        
        public async Task<Template> GetByIdAsync(int Id)
        {
            return await _context.template.FindAsync(Id);
        }
        public async Task AddAsync(Template template)
        {
            _context.template.Add(template);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(Template template)
        {
            _context.template.Update(template);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var template = await _context.template.FindAsync(id);
            _context.template.Remove(template);
            await _context.SaveChangesAsync();
        }

        //USER USE
        public List<GetAllTemplate_ViewModel> GetAllInformation_Table_Template_and_TypeCV()
        {
            var result = from cv in _context.cv
                         join tem in _context.template on cv.templateId equals tem.Id
                         join typeCV in _context.types on tem.TypeID equals typeCV.Id
                         where cv.IsDefault == false
                         select new GetAllTemplate_ViewModel
                         {
                             CV_Id = cv.Id,
                             TypeID = typeCV.Id,
                             Type_Name = typeCV.Name,
                             Image = tem.ImagePath,
                         };
            return result.ToList();
        }

        //ADMIN USE
        public List<ListTemplateCV> Get_ListTemplates()
        {
            var result = from t in _context.template
                         join type in _context.types on t.TypeID equals type.Id
                         select new ListTemplateCV
                         {
                             Id = t.Id,
                             ImageCV = t.ImagePath,
                             TypeCV = type.Name,
                         };
            return result.ToList();
        }
        public List<Get_template_by_category_ViewModel> Get_Template_By_Categories(int category_Id)
        {
            var result = from t in _context.template
                         where(t.TypeID.Equals(category_Id))
                         select new Get_template_by_category_ViewModel
                         {
                             Id = t.Id,
                             Image = t.ImagePath,
                             Category = t.Name,
                         };
            return result.ToList();
        }
    }
}
