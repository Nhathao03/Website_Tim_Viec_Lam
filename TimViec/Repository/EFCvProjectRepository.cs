using Microsoft.EntityFrameworkCore;
using TimViec.Data;
using TimViec.Models;

namespace TimViec.Repository
{
    public class EFCvProjectRepository : ICvProjectRepository
    {
        private readonly ApplicationDbContext _context;
        public EFCvProjectRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<CVProject>> GetAllAsync()
        {
            return await _context.Projects.ToListAsync();
        }
        public async Task<CVProject> GetByIdAsync(int Id)
        {
            return await _context.Projects.FindAsync(Id);
        }
        public async Task AddAsync(CVProject cv)
        {
            _context.Projects.Add(cv);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(CVProject cv)
        {
            _context.Projects.Update(cv);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var project = await _context.Projects.FindAsync(id);
            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();
        }     
    }
}
