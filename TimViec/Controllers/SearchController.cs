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
        public async Task<IActionResult> Search(string stringSearch)
        {
            await DisplayDropdown();
            var result = _jobRepository.Search(stringSearch);

            return View(result);
        }


        //**********************************************************************************************
        //Search in Dropdown
        public async Task<IActionResult> ChoeseSearchSkill(int ID)
        {
            await DisplayDropdown();

            var skill = _jobRepository.ChoeseSearchSkills(ID);

            return View(skill);
        }

        public async Task<IActionResult> ChoeseSearchType(int ID)
        {
            await DisplayDropdown();

            var type = _jobRepository.ChoeseSearchType(ID);

            return View(type);
        }

        public async Task<IActionResult> ChoeseSearchRank(int ID)
        {
            await DisplayDropdown();

            var rank = _jobRepository.ChoeseSearchRank(ID);

            return View(rank);
        }

        public async Task<IActionResult> ChoeseSearchLocation(int ID)
        {
            await DisplayDropdown();

            var location = _jobRepository.ChoeseSearchLocation(ID);

            return View(location);
        }
    }
}
