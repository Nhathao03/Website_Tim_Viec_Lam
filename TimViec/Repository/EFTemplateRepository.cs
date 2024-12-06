using Microsoft.EntityFrameworkCore;
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
        public List<CreateCV_ViewModel> GetAllInformation_Table_Template_and_TypeCV()
        {
            var result = from t in _context.template
                         join type in _context.types on t.TypeID equals type.Id         
                         select new CreateCV_ViewModel
                         {
                             Id = t.Id,
                             Type_Name = type.Name,
                             HtmlTemplate = t.HtmlTemplate,
                             Image = t.ImagePath,
                             TypeID = type.Id,
                         };
            return result.ToList();

        }
    }
}
