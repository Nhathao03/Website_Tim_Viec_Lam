using TimViec.Data;
using TimViec.Models;
using Microsoft.EntityFrameworkCore;

namespace TimViec.Respository
{
    public class EFCityRespository : ICityRespository
    {
        private readonly ApplicationDbContext _context;
        public EFCityRespository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<City>> GetAllAsync()
        {
            return await _context.Cities.ToListAsync();
        }
        public async Task<City> GetByIdAsync(int Id)
        {
            return await _context.Cities.FindAsync(Id);
        }
        public async Task AddAsync(City city)
        {
            _context.Cities.Add(city);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(City city)
        {
            _context.Cities.Update(city);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var city = await _context.Cities.FindAsync(id);
            _context.Cities.Remove(city);
            await _context.SaveChangesAsync();
        }
    }
}
