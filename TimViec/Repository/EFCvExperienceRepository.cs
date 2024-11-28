using Microsoft.EntityFrameworkCore;
using TimViec.Data;
using TimViec.Models;

namespace TimViec.Repository
{
    public class EFCvExperienceRepository : ICvExperienceRepository
    {
        private readonly ApplicationDbContext _context;
        public EFCvExperienceRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<CVExperience>> GetAllAsync()
        {
            return await _context.Experiences.ToListAsync();
        }
        public async Task<CVExperience> GetByIdAsync(int Id)
        {
            return await _context.Experiences.FindAsync(Id);
        }
        public async Task AddAsync(CVExperience cv)
        {
            _context.Experiences.Add(cv);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(CVExperience cv)
        {
            _context.Experiences.Update(cv);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var experiences = await _context.Experiences.FindAsync(id);
            _context.Experiences.Remove(experiences);
            await _context.SaveChangesAsync();
        }     
    }
}
