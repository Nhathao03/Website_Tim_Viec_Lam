using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TimViec.Models;
using TimViec.Repository;
using TimViec.Respository;

namespace TimViec.Controllers
{
    public class SearchController : Controller
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

        public SearchController(ICompanyRespository companyRepository,
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
        // search
        [HttpGet]
        public async Task<IActionResult>Search(string stringSearch, int id)
        {
            await DisplayDropdown();
            try
            {
                if (!string.IsNullOrEmpty(stringSearch) || id != 0)  { 
                    var result = _jobRepository.Search(stringSearch,id);
                    ViewBag.CountResultSearch = result.Count();
                    return View(result);
				}
                else{
                    return NotFound();
                }
			}catch  (Exception ex)
            {
                return Json(new {success = false , error = ex.Message});
            }  
        }


		//**********************************************************************************************
		//Search in Dropdown
		[HttpGet]
		public async Task<IActionResult> ChoeseSearchSkill(int ID)
        {
            await DisplayDropdown();

            var skill = _jobRepository.ChoeseSearchSkills(ID);
            var Count_Jobs = _jobRepository.ChoeseSearchSkills(ID).Count();
            ViewBag.Count_Jobs = Count_Jobs;
            return View(skill);
        }

		[HttpGet]
		public async Task<IActionResult> ChoeseSearchType(int ID)
        {
            await DisplayDropdown();

            var type = _jobRepository.ChoeseSearchType(ID);
			var Count_Jobs = _jobRepository.ChoeseSearchType(ID).Count();
			ViewBag.Count_Jobs = Count_Jobs;
			return View(type);
        }

		[HttpGet]
		public async Task<IActionResult> ChoeseSearchRank(int ID)
        {
            await DisplayDropdown();

            var rank = _jobRepository.ChoeseSearchRank(ID);
			var Count_Jobs = _jobRepository.ChoeseSearchRank(ID).Count();
			ViewBag.Count_Jobs = Count_Jobs;
			return View(rank);
        }

		[HttpGet]
		public async Task<IActionResult> ChoeseSearchLocation(int ID)
        {
            await DisplayDropdown();

            var location = _jobRepository.ChoeseSearchLocation(ID);
			var Count_Jobs = _jobRepository.ChoeseSearchLocation(ID).Count();
			ViewBag.Count_Jobs = Count_Jobs;
			return View(location);
        }
    }
}
