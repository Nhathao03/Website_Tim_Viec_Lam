using TimViec.Data;
using TimViec.Models;
using Microsoft.EntityFrameworkCore;

namespace TimViec.Respository
{
    public class EFType_WorkRespository : IType_WorkRespository
    {
        private readonly ApplicationDbContext _context;
        public EFType_WorkRespository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Type_work>> GetAllAsync()
        {
            return await _context.Type_Works.ToListAsync();
        }
        public async Task<Type_work> GetByIdAsync(int Id)
        {
            return await _context.Type_Works.FindAsync(Id);
        }
        public async Task AddAsync(Type_work type_Work)
        {
            _context.Type_Works.Add(type_Work);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(Type_work type_Work)
        {
            _context.Type_Works.Update(type_Work);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var type_work = await _context.Type_Works.FindAsync(id);
            _context.Type_Works.Remove(type_work);
            await _context.SaveChangesAsync();
        }


    }
}
