using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TimViec.Models;
using TimViec.Repository;
using TimViec.Respository;

namespace TimViec.Controllers
{
	[Authorize]
	[Authorize(Roles = "User")]
	public class CompanyController : Controller
	{
		private readonly IJobRespository _jobRepository;
		private readonly ICompanyRespository _companyRepository;
		private readonly IStatusRepository _statusRepository;
		private readonly IRankRespository _rankRespository;
		private readonly ISkillRespository _skillRespository;
		private readonly IType_WorkRespository _WorkRespository;
		private readonly ICityRespository _cityRespository;
		private readonly IfavouriteJob _favouriteJob;
		private readonly IfeedbackRepository _feedbackRepository;
		private readonly UserManager<ApplicationUser> _userManager;

		public CompanyController(ICompanyRespository companyRepository,
			IJobRespository jobRepository,
			IStatusRepository statusRepository,
			IRankRespository rankRespository,
			ISkillRespository skillRespository,
			IType_WorkRespository type_workRespository,
			ICityRespository cityRespository,
			IfavouriteJob favouriteJob,
			IfeedbackRepository feedback,
			UserManager<ApplicationUser> userManager)
		{
			_jobRepository = jobRepository;
			_companyRepository = companyRepository;
			_statusRepository = statusRepository;
			_userManager = userManager;
			_skillRespository = skillRespository;
			_WorkRespository = type_workRespository;
			_cityRespository = cityRespository;
			_rankRespository = rankRespository;
			_favouriteJob = favouriteJob;
			_feedbackRepository = feedback;
		}

		private async Task DisplayDropdown()
		{
			var rank = await _rankRespository.GetAllAsync();
			var skill = await _skillRespository.GetAllAsync();
			var type_work = await _WorkRespository.GetAllAsync();
			var city = await _cityRespository.GetAllAsync();

			ViewBag.Skill = skill.ToList();
			ViewBag.Type = type_work.ToList();
			ViewBag.Rank = rank.ToList();
			ViewBag.Location = city.ToList();
		}

		//all company
		public async Task<IActionResult> Company()
		{
			await DisplayDropdown();

			var companies = await _companyRepository.GetAllAsync();
			return View(companies);
		}

		// display details company
		public async Task<IActionResult> Details_CPN(int id)
		{
			await DisplayDropdown();

			try
			{
				var company = _companyRepository.Details_CPN(id);
				return PartialView("_PartialDetails_Copmpany",company);
			}catch(Exception ex)
			{
				return Json(new { success = false, message = ex.Message });
			}
		}
	}
}
