using TimViec.Data;
using TimViec.Models;
using Microsoft.EntityFrameworkCore;

namespace TimViec.Respository
{
    public class EFSkillRespository : ISkillRespository
    {
        private readonly ApplicationDbContext _context;
        public EFSkillRespository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Skill>> GetAllAsync()
        {
            return await _context.Skills.ToListAsync();
        }
        public async Task<Skill> GetByIdAsync(int Id)
        {
            return await _context.Skills.FindAsync(Id);
        }
        public async Task AddAsync(Skill skills)
        {
            _context.Skills.Add(skills);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(Skill skills)
        {
            _context.Skills.Update(skills);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var skill = await _context.Skills.FindAsync(id);
            _context.Skills.Remove(skill);
            await _context.SaveChangesAsync();
        }

		
	}
}
