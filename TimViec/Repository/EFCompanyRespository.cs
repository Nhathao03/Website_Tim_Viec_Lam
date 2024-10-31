using TimViec.Data;
using TimViec.Models;
using Microsoft.EntityFrameworkCore;
using TimViec.ViewModel;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace TimViec.Respository
{
    public class EFCompanyRespository : ICompanyRespository
    {
        private readonly ApplicationDbContext _context;
        public EFCompanyRespository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Company>> GetAllAsync()
        {
            return await _context.Companies.ToListAsync();
        }
        public async Task<Company> GetByIdAsync(int Id)
        {
            return await _context.Companies.FindAsync(Id);
        }
        public async Task AddAsync(Company company)
        {
            _context.Companies.Add(company);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(Company company)
        {
            _context.Companies.Update(company);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var company = await _context.Companies.FindAsync(id);
            _context.Companies.Remove(company);
            await _context.SaveChangesAsync();
        }

		public List<Details_CPN> Details_CPN(int ID)
		{
			var result = from j in _context.Jobs
						 join c in _context.Companies on j.CompanyID equals c.Id
                         join t in _context.Type_Works on j.Type_workID equals t.Id
                         join ct in _context.Cities on c.CityID equals ct.Id 

						 where (c.Id.Equals(ID))
						 select new Details_CPN
						 {
							 CompanyName1 = c.Name_company,
							 JobName = j.Title,
							 Email = c.Email,
							 Company_Size = c.Company_size,
							 Company_Type = t.Type,
							 R1_Language = j.R1_Language,
							 R2_Language = j.R2_Language,
							 R3_Language = j.R3_Language,
							 Description = c.Description,
							 DescriptionJob = j.Description,
							 Location = c.Location,
							 LocationJob = j.Location,
							 Image = c.Image,
							 ImageJob = j.img,
							 IDJob = j.Id,
                             city = ct.Name_city,
                             About_Me = c.About_Me
						 };
			return result.ToList();
		}

        public async Task<Company> GetByEmailAsync(string email)
        {
            return await _context.Companies.FirstOrDefaultAsync(x => x.Email == email);
        }

        public List<CountJobInCompany> CountJobInCompanies(int ID)
        {
            var result = from j in _context.Jobs
                         join c in _context.Companies on j.CompanyID equals c.Id
                         where (j.CompanyID.Equals(ID))
                         select new CountJobInCompany
                         {
                             Count = j.CompanyID,
                         };
            return result.ToList();
        }

        public List<GetJobByEmail> GetJobByEmail(string email)
        {
            var result = from j in _context.Jobs
                         join c in _context.Companies on j.CompanyID equals c.Id
                         join r in _context.Ranks on j.RankID equals r.Id
                         join t in _context.Type_Works on j.Type_workID equals t.Id
                         join s in _context.Skills on j.SkillID equals s.Id
                         where (c.Email.Equals(email))
                         select new GetJobByEmail
                         {
                             Salary = j.Salary,
                             Id = j.Id,
                             Skill = j.SkillID,
                             JobName = j.Title,
                             R1_Language = j.R1_Language,
                             R2_Language = j.R2_Language,
                             R3_Language = j.R3_Language,
                             Rank = j.RankID,
                             Type = j.Type_workID,
                             Rankname = r.rank,
                             Typename = t.Type,
                             Skillname = s.Skills,
                         };
            return result.ToList();
        }
    }
}
