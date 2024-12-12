using Microsoft.EntityFrameworkCore;
using TimViec.Data;
using TimViec.Models;

namespace TimViec.Repository
{
    public class EFTypeCVRepository : ITypeCVRepository
    {
        private readonly ApplicationDbContext _context;
        public EFTypeCVRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<TypeCV>> GetAllAsync()
        {
            return await _context.types.ToListAsync();
        }
        public async Task<TypeCV> GetByIdAsync(int Id)
        {
            return await _context.types.FindAsync(Id);
        }
        public async Task AddAsync(TypeCV type)
        {
            _context.types.Add(type);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(TypeCV type)
        {
            _context.types.Update(type);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var type = await _context.types.FindAsync(id);
            _context.types.Remove(type);
            await _context.SaveChangesAsync();
        }     
    }
}
