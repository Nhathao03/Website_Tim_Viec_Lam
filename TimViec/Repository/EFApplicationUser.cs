using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TimViec.Data;
using TimViec.Models;
using TimViec.ViewModel;

namespace TimViec.Repository
{
	public class EFApplicationUser : IApplicationUser
	{
		private readonly ApplicationDbContext _context;
		public EFApplicationUser(ApplicationDbContext context)
		{
			_context = context;
		}
		public async Task<IEnumerable<ApplicationUser>> GetAllAsync()
		{
			return await _context.applicationUsers.ToListAsync();
		}

		public async Task<ApplicationUser> GetByIdAsync(int Id)
		{
			return await _context.applicationUsers.FindAsync(Id);
		}
		public async Task AddAsync(ApplicationUser applicationUser)
		{
			_context.applicationUsers.Add(applicationUser);
			await _context.SaveChangesAsync();
		}
		public async Task UpdateAsync(ApplicationUser applicationUser)
		{
			_context.applicationUsers.Update(applicationUser);
			await _context.SaveChangesAsync();
		}
		public async Task DeleteAsync(int id)
		{
			var user = await _context.applicationUsers.FindAsync(id);
			_context.applicationUsers.Remove(user);
			await _context.SaveChangesAsync();
		}

        public async Task DeleteStringAsync(string id)
        {
            var user = await _context.applicationUsers.FindAsync(id);
            _context.applicationUsers.Remove(user);
            await _context.SaveChangesAsync();
        }

        public List<ViewAccountUserModel> GetAllUser(string role)
		{
			var result = from a in _context.applicationUsers
						 join ur in _context.UserRoles on a.Id equals ur.UserId
                         join r in _context.Roles on ur.RoleId equals r.Id
                         where (r.Name.Contains(role))
						 select new ViewAccountUserModel
						 {
							 Id  = ur.UserId,
							 fullname = a.Fullname,
							 email = a.Email,
							 Birth = a.Birth,
							 phonenumber = a.PhoneNumber,
						 };
			return result.ToList();
		}

        public List<AccountCompanyViewModel> GetAllCompany(string role)
        {
            var result = from a in _context.applicationUsers
                         join ur in _context.UserRoles on a.Id equals ur.UserId
                         join r in _context.Roles on ur.RoleId equals r.Id
						 join c in _context.Companies on a.Email equals c.Email
                         where (r.Name.Contains(role))
                         select new AccountCompanyViewModel
                         {
							 Id = c.Id,
                             NameCompany = c.Name_company,
                             EmailCompany = a.Email,
                             CompanySize = c.Company_size,
                             Location = c.Location,
                         };
            return result.ToList();
        }

        public List<ViewInforCompany> GetInforCompany(string email)
        {
			var result = from a in _context.applicationUsers
						 join c in _context.Companies on a.Email equals c.Email
						 join city in _context.Cities on c.CityID equals city.Id
						 where (c.Email.Equals(email))
						 select new ViewInforCompany
						 {
							 Email = c.Email,
							 Description = c.Description,
							 Date = c.Date,
							 CityName = city.Name_city,
							 Location = c.Location,
							 logo = c.Image,
							 Size = c.Company_size,
							 TypeName = c.Company_type,
							 Name = c.Name_company,
							 Id = c.Id,
                         };
            return result.ToList();
        }

        public async Task<ApplicationUser> GetByStringId(string ID)
        {
            return await _context.applicationUsers.FindAsync(ID);
        }
	}
}
