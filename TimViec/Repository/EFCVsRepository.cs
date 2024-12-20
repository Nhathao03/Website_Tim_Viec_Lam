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
                             CViD = CV.Id,
                             Title = CV.Title,
                             TypeName = typeSec.Name,
                             ContentJson = section.ContentJson,
                             StyleJson = section.StyleJson,
                             ImagePath = tem.ImagePath,
                             TypeID = typeSec.Id,
                             Background = tem.HtmlTemplate,
                             NameCV = CV.Title,
                         };
            return result.ToList();
        }

        //USER USE
        public List<ManageCVofUser> ManageCV(string id)
        {
            var result = from cv in _context.cv
                         join tem in _context.template on cv.templateId equals tem.Id
                         join typeCV in _context.types on tem.TypeID equals typeCV.Id
                         where cv.IsDefault == true && cv.UserID == id
                         select new ManageCVofUser
                         {
                             Id = cv.Id,
                             NameCV = cv.Title,
                             CreatedAt = cv.CreateAt,
                             UpdatedAt = cv.UpdateAt,
                         };
            return result.ToList();
        }

    }
}
