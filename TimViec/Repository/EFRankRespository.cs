using TimViec.Data;
using TimViec.Models;
using Microsoft.EntityFrameworkCore;

namespace TimViec.Respository
{
    public class EFRankRespository : IRankRespository
    {
        private readonly ApplicationDbContext _context;
        public EFRankRespository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Rank>> GetAllAsync()
        {
            return await _context.Ranks.ToListAsync();
        }
        public async Task<Rank> GetByIdAsync(int Id)
        {
            return await _context.Ranks.FindAsync(Id);
        }
        public async Task AddAsync(Rank rank)
        {
            _context.Ranks.Add(rank);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(Rank rank)
        {
            _context.Ranks.Update(rank);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var rank = await _context.Ranks.FindAsync(id);
            _context.Ranks.Remove(rank);
            await _context.SaveChangesAsync();
        }
    }
}
