using Microsoft.EntityFrameworkCore;
using NuGet.Packaging.Signing;
using System.Security.Cryptography.Xml;
using TimViec.Data;
using TimViec.Models;
using TimViec.ViewModel;

namespace TimViec.Repository
{
    public class EFStatusJobRepository :IStatusRepository
    {
        private readonly ApplicationDbContext _context;
        public EFStatusJobRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<StatusJob>> GetAllAsync()
        {
            return await _context.StatusJobs.ToListAsync();
        }
        public async Task<StatusJob> GetByIdAsync(int Id)
        {
            return await _context.StatusJobs.FindAsync(Id);
        } 
        public async Task AddAsync(StatusJob statusJob)
        {
            _context.StatusJobs.Add(statusJob);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(StatusJob statusJob)
        {
            _context.StatusJobs.Update(statusJob);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var statusJob = await _context.StatusJobs.FindAsync(id);
            _context.StatusJobs.Remove(statusJob);
            await _context.SaveChangesAsync();
        }

        public List<StatusViewModel> GetListJobByEmail(string email)
        {
            var status = from j in _context.Jobs
                         join s in _context.StatusJobs on j.Id equals s.JobID
                         join c in _context.Companies on j.CompanyID equals c.Id
                         where (s.Email.Equals(email))
                         select new StatusViewModel
                         {
                             Name = s.Fullname,
                             NameJob = j.Title,
                             Email = s.Email,
                             imgCV = s.imgCV,
                             Note = s.Note,
                             Id = s.ID,
                             Status = s.Status,
                             CompanyID = j.CompanyID,
                             CompanyName = c.Name_company,
                         };
            return status.Where(x => x.Email == email).ToList();
        }

        public List<CompanyCheckStatusViewModel> CompanyCheckStatus(string email)
        {
            var status = from s in _context.StatusJobs
                         join j in _context.Jobs on s.JobID equals j.Id
                         join c in _context.Companies on j.CompanyID equals c.Id
                         where (c.Email.Equals(email))
                         select new CompanyCheckStatusViewModel
                         {
                             Email = s.Email,
                             imgCV = s.imgCV,
                             Note = s.Note,
                             Id = s.ID,
                             Status = s.Status,
                             NameUser = s.Fullname,
                             JobName = j.Title,
                             Read = s.Read,
                         };
            return status.ToList();
        }

    }
}
