using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;
using System.Net.Mail;
using TimViec.Models;
using TimViec.Repository;
using TimViec.Respository;
using TimViec.ViewModel;


namespace TimViec.Controllers
{
    [Authorize]
	[Authorize(Roles = "User")]
	public class HomeController : Controller
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

		public HomeController(ICompanyRespository companyRepository,
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

		// display item
		public async Task<IActionResult> Index()
		{
			var jobs = await _jobRepository.GetAllAsync();
			var companies = await _companyRepository.GetAllAsync();

			await DisplayDropdown();

			// get list companies

			//take n = 6 item in table job
			jobs = jobs.Where(x => x.Id == 25 || x.Id == 27 || x.Id == 35 || x.Id == 32 || x.Id == 39 || x.Id == 40);

			companies = companies.Take(3);
			HomeViewModel home = new HomeViewModel()
			{
				Jobs = jobs,
				Companies = companies,
			};

			return View(home);
		}		

		//*******************************************************************************************
		
		//feedback                               
		public async Task<IActionResult> feedback()
		{

			await DisplayDropdown();
			return View();
		}

        // add status job
        [HttpPost]
        public async Task<IActionResult> feedback(feedback feedback)
        {
            await _feedbackRepository.AddAsync(feedback);
            return RedirectToAction(nameof(Index));

        }
        //*******************************************************************************************

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}

        [AllowAnonymous]
        public async Task<IActionResult> Rules()
		{
			return View();
		}

        [AllowAnonymous]
        public async Task<IActionResult> Policy()
		{
			return View();
		}
	} 
}
