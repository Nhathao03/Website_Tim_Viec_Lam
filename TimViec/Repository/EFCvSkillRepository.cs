using Microsoft.EntityFrameworkCore;
using TimViec.Data;
using TimViec.Models;

namespace TimViec.Repository
{
    public class EFCvSkillRepository : ICvSkillRepository
    {
        private readonly ApplicationDbContext _context;
        public EFCvSkillRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<CVSkill>> GetAllAsync()
        {
            return await _context.CV_Skills.ToListAsync();
        }
        public async Task<CVSkill> GetByIdAsync(int Id)
        {
            return await _context.CV_Skills.FindAsync(Id);
        }
        public async Task AddAsync(CVSkill cv)
        {
            _context.CV_Skills.Add(cv);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(CVSkill cv)
        {
            _context.CV_Skills.Update(cv);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var cv_skill = await _context.CV_Skills.FindAsync(id);
            _context.CV_Skills.Remove(cv_skill);
            await _context.SaveChangesAsync();
        }     
    }
}
