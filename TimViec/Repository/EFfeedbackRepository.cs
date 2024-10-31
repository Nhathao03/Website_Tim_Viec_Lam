using Microsoft.EntityFrameworkCore;
using TimViec.Data;
using TimViec.Models;

namespace TimViec.Repository
{
    public class EFfeedbackRepository : IfeedbackRepository
    {
        private readonly ApplicationDbContext _context;
        public EFfeedbackRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<feedback>> GetAllAsync()
        {
            return await _context.feedbacks.ToListAsync();
        }
        public async Task<feedback> GetByIdAsync(int id)
        {
            return await _context.feedbacks.FindAsync(id);
        }
        public async Task AddAsync(feedback feedback)
        {
            _context.feedbacks.Add(feedback);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(feedback feedback)
        {
            _context.feedbacks.Update(feedback);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var feedback = await _context.feedbacks.FindAsync(id);
            _context.feedbacks.Remove(feedback);
            await _context.SaveChangesAsync();
        }
    }
}
