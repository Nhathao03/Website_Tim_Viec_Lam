using TimViec.Data;
using TimViec.Models;
using Microsoft.EntityFrameworkCore;
using TimViec.ViewModel;

namespace TimViec.Respository
{
    public class EFJobRespository : IJobRespository
    {
        private readonly ApplicationDbContext _context;
        public EFJobRespository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Job>> GetAllAsync()
        {
            return await _context.Jobs.ToListAsync();
        }
        public async Task<Job> GetByIdAsync(int id)
        {
            return await _context.Jobs.FindAsync(id);
        }
        public async Task AddAsync(Job job)
        {
            _context.Jobs.Add(job);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(Job job)
        {
            _context.Jobs.Update(job);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var job = await _context.Jobs.FindAsync(id);
            _context.Jobs.Remove(job);
            await _context.SaveChangesAsync();
        }

		public List<SearchViewModel> Search(string stringsearch, int id)
		{
            var result = from j in _context.Jobs
                         join c in _context.Companies on j.CompanyID equals c.Id
                         join ct in _context.Cities on c.CityID equals ct.Id
                         where ((j.Title.Contains(stringsearch) || j.R1_Language.Equals(stringsearch) || j.R2_Language.Equals(stringsearch) || j.R3_Language.Equals(stringsearch) || c.Name_company.Contains(stringsearch) && (ct.Id.Equals(id))) 
                                || (j.Title.Contains(stringsearch)|| j.R1_Language.Equals(stringsearch)|| j.R2_Language.Equals(stringsearch)|| j.R3_Language.Equals(stringsearch)|| c.Name_company.Contains(stringsearch)) 
                                || (ct.Id.Equals(id)))
						 select new SearchViewModel
                         {  
                             Id = j.Id,
                             JobName = j.Title,
                             R1_Language = j.R1_Language,
                             R2_Language = j.R2_Language,
                             R3_Language = j.R3_Language,
                             Image = j.img,
                         };
		    return result.ToList();
        }

		public List<ChoeseSearchSkill> ChoeseSearchSkills(int ID)
		{
			var result = from s in _context.Skills
				         join j in _context.Jobs  on s.Id equals j.SkillID
						 where (j.SkillID.Equals(ID))
						 select new ChoeseSearchSkill
						 {
							 R1_Language = j.R1_Language,
							 R2_Language = j.R2_Language,
							 R3_Language = j.R3_Language,
							 Id = j.Id,
                             NameJob = j.Title,
                             Image = j.img,
                             SkillName = s.Skills,
						 };
			return result.ToList();
		}

		public List<ChoeseSearchType> ChoeseSearchType(int ID)
		{
			var result = from t in _context.Type_Works
						 join j in _context.Jobs on t.Id equals j.Type_workID
						 where (j.Type_workID.Equals(ID))
						 select new ChoeseSearchType
						 {
							 R1_Language = j.R1_Language,
							 R2_Language = j.R2_Language,
							 R3_Language = j.R3_Language,
							 Id = j.Id,
							 NameJob = j.Title,	
                             Type = t.Type,
							 Image = j.img, 
						 };
			return result.ToList();
		}

		public List<ChoeseSearchRank> ChoeseSearchRank(int ID)
		{
			var result = from r in _context.Ranks
						 join j in _context.Jobs on r.Id equals j.RankID
						 where (r.Id.Equals(ID))
						 select new ChoeseSearchRank
						 {
							 R1_Language = j.R1_Language,
							 R2_Language = j.R2_Language,
							 R3_Language = j.R3_Language,
							 Id = j.Id,
							 NameJob = j.Title,
							 Rankname = r.rank,
							 Image = j.img,
						 };
			return result.ToList();
		}

		public List<ChoeseSearchLocation> ChoeseSearchLocation(int ID)
		{
			var result = from j in _context.Jobs
                         join c in _context.Companies on j.CompanyID equals c.Id
						 join city in _context.Cities on c.CityID equals city.Id
						 where (c.CityID.Equals(ID))
						 select new ChoeseSearchLocation
						 {
							 R1_Language = j.R1_Language,
							 R2_Language = j.R2_Language,
							 R3_Language = j.R3_Language,
							 Id = j.Id,
							 NameJob = j.Title,
							 Cityname = city.Name_city,
							 Image = j.img,
						 };
			return result.ToList();
		}

        public List<Details_Job> Details_Job(int ID)
        {
            var result = from j in _context.Jobs
                         join c in _context.Companies on j.CompanyID equals c.Id
                         join r in _context.Ranks on j.RankID equals r.Id
                         join t in _context.Type_Works on j.Type_workID equals t.Id
                         join city in _context.Cities on c.CityID equals city.Id
                         where (j.Id.Equals(ID))
                         select new Details_Job
                         {
                             Name = j.Title,
                             Id = j.Id,
                             CompanyID = c.Id,
                             CompanyName = c.Name_company,
                             Description = j.Description,
                             R1_Language = j.R1_Language,
                             R2_Language = j.R2_Language,
                             R3_Language = j.R3_Language,
                             Location = j.Location,
                             Logo = j.img,
                             Salary = j.Salary,
                             rank = r.rank,
                             type = t.Type,
                             size = c.Company_size,
                             descriptioncpn = c.Description,
                             work_responsitory = j.Work_responsibility,
                             treatment = j.treatment,
                             city = city.Name_city,
                             advanced_skill = j.advanced_skill
                         };
            return result.ToList();
        }

        public List<SearchJobViewModel> SearchJob (string stringSearch, int ID)
        {
            var result = from j in _context.Jobs
                         join cpn in _context.Companies on j.CompanyID equals cpn.Id
                         join city in _context.Cities on cpn.CityID equals city.Id
                         where (j.Title.Contains(stringSearch) && city.Id.Equals(ID))
                         select new SearchJobViewModel
                         {
                             Id = j.Id,
                             Image = j.img,
                             R1 = j.R1_Language,
                             R2 = j.R2_Language,
                             R3 = j.R3_Language,
                         };
            return result.ToList();
        }

    }
}
