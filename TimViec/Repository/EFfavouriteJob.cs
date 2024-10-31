using Microsoft.EntityFrameworkCore;
using TimViec.Data;
using TimViec.Models;

namespace TimViec.Repository
{
    public class EFfavouriteJob : IfavouriteJob
    {
        private readonly ApplicationDbContext _context;
        public EFfavouriteJob(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<favourite_job>> GetAllAsync()
        {
            return await _context.favourite_Jobs.ToListAsync();
        }
        public async Task<favourite_job> GetByIdAsync(int Id)
        {
            return await _context.favourite_Jobs.FindAsync(Id);
        }
        public async Task AddAsync(favourite_job favourite_Job)
        {
            _context.favourite_Jobs.Add(favourite_Job);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(favourite_job favourite_Job)
        {
            _context.favourite_Jobs.Update(favourite_Job);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var favourite_job = await _context.favourite_Jobs.FindAsync(id);
            _context.favourite_Jobs.Remove(favourite_job);
            await _context.SaveChangesAsync();
        }     
    }
}
