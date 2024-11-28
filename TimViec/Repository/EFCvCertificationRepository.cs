using Microsoft.EntityFrameworkCore;
using TimViec.Data;
using TimViec.Models;

namespace TimViec.Repository
{
    public class EFCvCertificationRepository : ICvCertificationRepository
    {
        private readonly ApplicationDbContext _context;
        public EFCvCertificationRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<CVCertifications>> GetAllAsync()
        {
            return await _context.Certifications.ToListAsync();
        }
        public async Task<CVCertifications> GetByIdAsync(int Id)
        {
            return await _context.Certifications.FindAsync(Id);
        }
        public async Task AddAsync(CVCertifications cv)
        {
            _context.Certifications.Add(cv);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(CVCertifications cv)
        {
            _context.Certifications.Update(cv);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var certifications = await _context.Certifications.FindAsync(id);
            _context.Certifications.Remove(certifications);
            await _context.SaveChangesAsync();
        }     
    }
}
