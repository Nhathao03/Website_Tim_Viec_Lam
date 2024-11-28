using Microsoft.EntityFrameworkCore;
using TimViec.Data;
using TimViec.Models;

namespace TimViec.Repository
{
    public class EFCvEducationRepository : ICvEducationRepository
    {
        private readonly ApplicationDbContext _context;
        public EFCvEducationRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<CVEducation>> GetAllAsync()
        {
            return await _context.Educations.ToListAsync();
        }
        public async Task<CVEducation> GetByIdAsync(int Id)
        {
            return await _context.Educations.FindAsync(Id);
        }
        public async Task AddAsync(CVEducation cv)
        {
            _context.Educations.Add(cv);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(CVEducation cv)
        {
            _context.Educations.Update(cv);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var educations = await _context.Educations.FindAsync(id);
            _context.Educations.Remove(educations);
            await _context.SaveChangesAsync();
        }     
    }
}
