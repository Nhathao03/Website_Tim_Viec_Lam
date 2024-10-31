using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TimViec.Helpers;
using TimViec.Models;
using TimViec.Repository;
using TimViec.Respository;

namespace TimViec.Controllers
{
    [Authorize]
    [Authorize(Roles = "User")]
    public class StatusJobController : Controller
    {
        private readonly IJobRespository _jobRepository;
        private readonly ICompanyRespository _companyRepository;
        private readonly IStatusRepository _statusRepository;
        private readonly IRankRespository _rankRespository;
        private readonly ISkillRespository _skillRespository;
        private readonly IType_WorkRespository _WorkRespository;
        private readonly ICityRespository _cityRespository;
		private readonly UserManager<ApplicationUser> _userManager;

		public StatusJobController(ICompanyRespository companyRepository,
            IJobRespository jobRepository,
            IStatusRepository statusRepository,
            IRankRespository rankRespository,
            ISkillRespository skillRespository,
            IType_WorkRespository type_workRespository,
            ICityRespository cityRespository,
            UserManager<ApplicationUser> userManager)
        {
            _jobRepository = jobRepository;
            _companyRepository = companyRepository;
            _statusRepository = statusRepository;
            _skillRespository = skillRespository;
            _WorkRespository = type_workRespository;
            _cityRespository = cityRespository;
			_userManager = userManager;
			_rankRespository = rankRespository;
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

        //status job
        public IActionResult StatusJob()
        {

            var name = User.Identity.Name;
            var status = _statusRepository.GetListJobByEmail(email: name);

            foreach (var job in status)
            {
                job.StatusName = EnumExtension.GetEnumDescription((Constants.StatusJob)job.Status);
            }

            return View(status);
        }

        // Delete Status
        public async Task<IActionResult> Delete_Status(int ID)
        {
            await DisplayDropdown();
            var status = await _statusRepository.GetByIdAsync(ID);
            var getjobname = await _jobRepository.GetByIdAsync(status.JobID);
            var companyname = _companyRepository.Details_CPN(getjobname.CompanyID).FirstOrDefault();

            ViewBag.CompanyName = companyname;
            ViewBag.Jobname = getjobname.Title;

            if (status == null)
            {
                return NotFound();
            }
            return View(status);
        }


        // Process delete status
        [HttpPost, ActionName("Delete_Status")]
        public async Task<IActionResult> DeleteConfirmed_Company(int id)
        {
            await _statusRepository.DeleteAsync(id);
            return RedirectToAction(nameof(StatusJob));

        }

		//***************************************************************************************
		//create applications
		public async Task<IActionResult> CreateApplication(int id)
		{
			await DisplayDropdown();
			var user = await _userManager.GetUserAsync(User);
			ViewBag.Email = user.Email;
			var status = await _statusRepository.GetAllAsync();

			var job = await _jobRepository.GetByIdAsync(id);
			ViewBag.GetJob = job;

			if (status == null)
			{
				return NotFound();
			}
			return View();
		}
		// add status job
		[HttpPost]
		public async Task<IActionResult> CreateApplication(Models.StatusJob statusJob, IFormFile imgCV, int id)
		{
			statusJob.ID = 0;
			statusJob.JobID = id;
			if (imgCV != null)
			{
				// save imgae
				statusJob.imgCV = await SaveImage(imgCV);
			}

			statusJob.Status = (int)Constants.StatusJob.Inprogress;
			statusJob.Read = (int)Constants.ViewStatus.NoRead;

			await _statusRepository.AddAsync(statusJob);
			return RedirectToAction(nameof(StatusJob));

		}

		//Luu anh
		private async Task<string> SaveImage(IFormFile image)
		{
			var savePath = Path.Combine("wwwroot/LayoutTimViec/img", image.FileName);
			using (var fileStream = new FileStream(savePath, FileMode.Create))
			{
				await image.CopyToAsync(fileStream);
			}
			return "LayoutTimViec/img/" + image.FileName;
		}

	}
}
