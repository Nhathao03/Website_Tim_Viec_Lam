using Microsoft.EntityFrameworkCore;
using TimViec.Data;
using TimViec.Models;

namespace TimViec.Repository
{
    public class EFCvHonor_awardRepository : ICvHonor_AwardRepository
    {
        private readonly ApplicationDbContext _context;
        public EFCvHonor_awardRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<CVHonors_Awards>> GetAllAsync()
        {
            return await _context.Honors_Awards.ToListAsync();
        }
        public async Task<CVHonors_Awards> GetByIdAsync(int Id)
        {
            return await _context.Honors_Awards.FindAsync(Id);
        }
        public async Task AddAsync(CVHonors_Awards cv)
        {
            _context.Honors_Awards.Add(cv);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(CVHonors_Awards cv)
        {
            _context.Honors_Awards.Update(cv);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var honors_Awards = await _context.Honors_Awards.FindAsync(id);
            _context.Honors_Awards.Remove(honors_Awards);
            await _context.SaveChangesAsync();
        }     
    }
}
