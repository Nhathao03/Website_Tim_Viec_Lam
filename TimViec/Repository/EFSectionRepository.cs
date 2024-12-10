using Microsoft.EntityFrameworkCore;
using TimViec.Data;
using TimViec.Models;
using TimViec.Respository;
using TimViec.ViewModel;
using static System.Collections.Specialized.BitVector32;

namespace TimViec.Repository
{
    public class EFSectionRepository : ISectionRespository
    {
        private readonly ApplicationDbContext _context;
        public EFSectionRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Sections>> GetAllAsync()
        {
            return await _context.section.ToListAsync();
        }
        
        public async Task<Sections> GetByIdAsync(int Id)
        {
            return await _context.section.FindAsync(Id);            
        }
        public async Task AddAsync(Sections sections)
        {
            _context.section.Add(sections);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(Sections sections)
        {
            _context.section.Update(sections);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var sections = await _context.section.FindAsync(id);
            _context.section.Remove(sections);
            await _context.SaveChangesAsync();
        }
    }
}
