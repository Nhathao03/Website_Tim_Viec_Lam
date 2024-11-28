using Microsoft.EntityFrameworkCore;
using TimViec.Data;
using TimViec.Models;

namespace TimViec.Repository
{
    public class EFCvDetailsRepository : ICvDetailsRepository
    {
        private readonly ApplicationDbContext _context;
        public EFCvDetailsRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<CVDetails>> GetAllAsync()
        {
            return await _context.CV.ToListAsync();
        }
        public async Task<CVDetails> GetByIdAsync(int Id)
        {
            return await _context.CV.FindAsync(Id);
        }
        public async Task AddAsync(CVDetails cv)
        {
            _context.CV.Add(cv);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(CVDetails cv)
        {
            _context.CV.Update(cv);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var cv = await _context.CV.FindAsync(id);
            _context.CV.Remove(cv);
            await _context.SaveChangesAsync();
        }     
    }
}
