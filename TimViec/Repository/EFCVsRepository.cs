using TimViec.Data;
using TimViec.Models;
using Microsoft.EntityFrameworkCore;
using TimViec.ViewModel;

namespace TimViec.Respository
{
    public class EFCVsRepository : ICVsRepository
    {
        private readonly ApplicationDbContext _context;
        public EFCVsRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<CV>> GetAllAsync()
        {
            return await _context.cv.ToListAsync();
        }
        public async Task<CV> GetByIdAsync(int Id)
        {
            return await _context.cv.FindAsync(Id);
        }
        public async Task AddAsync(CV cv)
        {
            _context.cv.Add(cv);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(CV cv)
        {
            _context.cv.Update(cv);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var cv = await _context.cv.FindAsync(id);
            _context.cv.Remove(cv);
            await _context.SaveChangesAsync();
        }

        public List<Get_CV_ByCvid_ViewModel> GetTemplates_by_ID_CV(int CV_ID)
        {
            var result = from CV in _context.cv
                         join section in _context.section on CV.Id equals section.cvId
                         join tem in _context.template on CV.templateId equals tem.Id
                         join typeSec in _context.typeSections on section.TypeSectionId equals typeSec.Id
                         where CV.Id == CV_ID
                         select new Get_CV_ByCvid_ViewModel
                         {
                             Id = section.Id,
                             UserID = CV.UserID,
                             Title = CV.Title,
                             TypeName = typeSec.Name,
                             ContentJson = section.ContentJson,
                             StyleJson = section.StyleJson,
                             ImagePath = tem.ImagePath,
                             TypeID = typeSec.Id,
                         };
            return result.ToList();
        }

	}
}
