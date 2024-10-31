using Azure.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Shared;
using Org.BouncyCastle.Crypto.Macs;
using System.Text;
using TimViec.Helpers;
using TimViec.Models;
using TimViec.Repository;
using TimViec.Respository;
using static System.Net.Mime.MediaTypeNames;
using static TimViec.Helpers.Constants;

namespace TimViec.Areas.CompanyManage.Controllers
{
    [Area("CompanyManage")]
    [Authorize(Roles = "Company")]  
    public class CompanyController : Controller
    {
        private readonly IJobRespository _jobRepository;
        private readonly ICompanyRespository _companyRepository;
        private readonly IStatusRepository _statusRepository;
        private readonly IApplicationUser _applicationUser;
        private readonly IType_WorkRespository _WorkRespository;
        private readonly IRankRespository _rankRespository;
        private readonly ISkillRespository _skillRespository;
        private readonly ICityRespository _cityRespository;
        private readonly UserManager<ApplicationUser> _userManagers;

        public CompanyController(ICompanyRespository companyRepository,
            IJobRespository jobRepository,
            IStatusRepository statusRepository,
            IApplicationUser userManager,
            IRankRespository rankRespository,
            ISkillRespository skillRespository,
            IType_WorkRespository type_workRespository,
            ICityRespository cityRespository,
            UserManager<ApplicationUser> userManagers)
        {
            _jobRepository = jobRepository;
            _companyRepository = companyRepository;
            _statusRepository = statusRepository;
            _applicationUser = userManager;
            _userManagers = userManagers;
            _WorkRespository = type_workRespository;
            _cityRespository = cityRespository;
            _skillRespository = skillRespository;
            _rankRespository = rankRespository;
        }

        //trang chu
        public async Task<IActionResult> Index()
        {
            var user = await _userManagers.GetUserAsync(User);
            string us = user.Email;

            //dem so luong cong viec
            var getinfor = _applicationUser.GetInforCompany(us);
            int id = getinfor.Select(x => x.Id).FirstOrDefault();
            var Count = _companyRepository.CountJobInCompanies(id).Count();

            var countstatus = _statusRepository.CompanyCheckStatus(user.Email).Count();

            var result = _statusRepository.CompanyCheckStatus(user.Email);

            ViewBag.Bell = result.Where(s => s.Read == 0).ToList();
            ViewBag.CountStatus = countstatus;
            ViewBag.Count = Count;

            return View(getinfor);
        }

        //**********************************************************************************************
        //Edit company
        public async Task<IActionResult> Edit_company()
        {
            var user = await _userManagers.GetUserAsync(User);
            var getinfor = await _companyRepository.GetByEmailAsync(user.Email);
            var editCPNa = await _companyRepository.GetByIdAsync(getinfor.Id);
            var type_work = await _WorkRespository.GetAllAsync();


            var result = _statusRepository.CompanyCheckStatus(user.Email);

            ViewBag.Bell = result.Where(s => s.Read == 0).ToList();

            ViewBag.Type = new SelectList(type_work, "Id", "Type");  

            var city = await _cityRespository.GetAllAsync();
            ViewBag.City = new SelectList(city, "Id", "Name_city");

            return View(editCPNa);
        }

        // Process the company update
        [HttpPost]
        public async Task<IActionResult> Edit_company(Company company, IFormFile Image)
        {
            //var user = await _userManagers.GetUserAsync(User);
            //var result = await _companyRepository.GetByEmailAsync(user.Email);
            if (Image != null)
            {
                company.Image = await SaveImageEdit(Image);
            }
            //else
            //{
            //    company.Image = result.Image;
            //}

            await _companyRepository.UpdateAsync(company);
            await Task.Delay(2000);
            return RedirectToAction("Edit_company");
        }

        //**********************************************************************************************
        //Luu anh
        private async Task<string> SaveImageEdit(IFormFile Image)
        {
            var savePath = Path.Combine("wwwroot/LayoutTimViec/img", Image.FileName);
            using (var fileStream = new FileStream(savePath, FileMode.Create))
            {
                await Image.CopyToAsync(fileStream);
            }
            return Image.FileName;
        }

        //**********************************************************************************************
        // display details company
        public async Task<IActionResult> AllJob()
        {
            var user = await _userManagers.GetUserAsync(User);
            string email = user.Email;
            var result = _companyRepository.GetJobByEmail(email);

            var resultBell = _statusRepository.CompanyCheckStatus(user.Email);
            ViewBag.Bell = resultBell.Where(s => s.Read == 0).ToList();

            if (result == null)
            {
                return NotFound();
            }
            return View(result);
        }

        // Delete Job
        public async Task<IActionResult> Delete_Job(int ID)
        {
            var user = await _userManagers.GetUserAsync(User);
            var result = await _jobRepository.GetByIdAsync(ID);
            var resultBell = _statusRepository.CompanyCheckStatus(user.Email);

            ViewBag.Bell = resultBell.Where(s => s.Read == 0).ToList();

            ViewBag.Rank = "abc";
            if (result.RankID == 1)
            {
                ViewBag.Rank = "Intern";
            }
            else if (result.RankID == 2)
            {
                ViewBag.Rank = "Fresher";
            }
            else if (result.RankID == 3)
            {
                ViewBag.Rank = "Junior";
            }
            else if (result.RankID == 4)
            {
                ViewBag.Rank = "Middle";
            }
            else if (result.RankID == 5)
            {
                ViewBag.Rank = "Senior";
            }
            else if (result.RankID == 6)
            {
                ViewBag.Rank = "Trưởng nhóm";
            }
            else if (result.RankID == 7)
            {
                ViewBag.Rank = "Trưởng phòng";
            }
            else
            {
                ViewBag.Rank = "All Levels";
            }

            ViewBag.Type = "abc";
            if (result.Type_workID == 1)
            {
                ViewBag.Type = "In Office";
            }
            else if (result.Type_workID == 2)
            {
                ViewBag.Type = "Hybird";
            }
            else if (result.Type_workID == 3)
            {
                ViewBag.Type = "Remote";
            }
            else if(result.Type_workID == 4)
            {
                ViewBag.Type = "Oversea";
            }

            if (result.R1_Language == null)
            {
                result.R1_Language = "Không yêu cầu";
            }
            else if (result.R1_Language == null)
            {
                result.R2_Language = "Không yêu cầu";
            }
            else if (result.R3_Language == null)
            {
                result.R3_Language = "Không yêu cầu";
            }

            if (result == null)
            {
                return NotFound();
            }
            return View(result);
        }

        //Edit Job
        public async Task<IActionResult> Edit_Job(int ID)
        {
            var job = await _jobRepository.GetByIdAsync(ID);

            var user = await _userManagers.GetUserAsync(User);
            var result = _statusRepository.CompanyCheckStatus(user.Email);
            ViewBag.Bell = result.Where(s => s.Read == 0).ToList();

            var rank = await _rankRespository.GetAllAsync();
            var skill = await _skillRespository.GetAllAsync();
            var type_work = await _WorkRespository.GetAllAsync();

            ViewBag.Skill = new SelectList(skill, "Id", "Skills");
            ViewBag.Type = new SelectList(type_work, "Id", "Type");
            ViewBag.Rank = new SelectList(rank, "Id", "rank");

            return View(job);
        }

        // Process the job update
        [HttpPost]   
        public async Task<IActionResult> Edit_Job(Job job, IFormFile img)
        {

            var user = await _userManagers.GetUserAsync(User);
            var result = await _companyRepository.GetByEmailAsync(user.Email);
            job.CompanyID = result.Id;
            if (img != null)
            {
                job.img = await SaveImageEdit(img);
            }
            else
            {
                job.img = result.Image;
            }
            await _jobRepository.UpdateAsync(job);
            return RedirectToAction("Edit_Job");
        }

        // Process delete job
        [HttpPost, ActionName("Delete_Job")]
        public async Task<IActionResult> DeleteConfirmed_Job(int id)
        {
            await _jobRepository.DeleteAsync(id);
            return RedirectToAction(nameof(AllJob));

        }
        //**********************************************************************************************
        // display details company
        public async Task<IActionResult> Create()
        {
            var rank = await _rankRespository.GetAllAsync();
            var skill = await _skillRespository.GetAllAsync();
            var type_work = await _WorkRespository.GetAllAsync();

            var user = await _userManagers.GetUserAsync(User);
            var result = _statusRepository.CompanyCheckStatus(user.Email);
            ViewBag.Bell = result.Where(s => s.Read == 0).ToList();

            ViewBag.Skill = new SelectList(skill, "Id", "Skills");
            ViewBag.Type = new SelectList(type_work, "Id", "Type");
            ViewBag.Rank = new SelectList(rank, "Id", "rank");


            return View();
        }
        //Luu anh
        private async Task<string> SaveImage(IFormFile img)
        {
            var savePath = Path.Combine("wwwroot/LayoutTimViec/img", img.FileName);
            using (var fileStream = new FileStream(savePath, FileMode.Create))
            {
                await img.CopyToAsync(fileStream);
            }
            return img.FileName;
        }

        // create 
        [HttpPost]
        public async Task<IActionResult> Create(Job job, IFormFile img)
        {

            var company = await _userManagers.GetUserAsync(User);
            var idcompany = await _companyRepository.GetByEmailAsync(company.Email);

            job.CompanyID = idcompany.Id;

                if (img != null)
                {
                    job.img = await SaveImage(img);  
                }


            await _jobRepository.AddAsync(job);
            return RedirectToAction(nameof(AllJob));
        }

        //*************************************************************************************
        //Applications Company
        public async Task<IActionResult> CompanyCheckStatus()
        {
            var user = await _userManagers.GetUserAsync(User);
            var result = _statusRepository.CompanyCheckStatus(user.Email);
            ViewBag.Bell = result.Where(s => s.Read == 0).ToList();

            foreach (var job in result)
            {
                job.Statusname = EnumExtension.GetEnumDescription((Constants.StatusJob)job.Status);
            }

            if (result == null)
            {
                return NotFound();
            }
            return View(result);
        }

        // Process the accept status
        [HttpGet]
        public async Task<IActionResult> Accept_Status(int ID)
        {
            var getid = await _statusRepository.GetByIdAsync(ID);
            getid.Status = (int)Constants.StatusJob.Completed;
            getid.Read = (int)Constants.ViewStatus.Read;
            await _statusRepository.UpdateAsync(getid);

            return RedirectToAction("CompanyCheckStatus");
        }

        // Process the refuse status
        [HttpGet]
        public async Task<IActionResult> Refuse_Status(int ID)
        {
            var getid = await _statusRepository.GetByIdAsync(ID);
            getid.Status = (int)Constants.StatusJob.Refuse;
            getid.Read = (int)Constants.ViewStatus.Read;
            await _statusRepository.UpdateAsync(getid);

            return RedirectToAction("CompanyCheckStatus");
        }

        // Process the  read status and detail
        [HttpGet]
        public async Task<IActionResult> Read_Status(int ID)
        {
            var getid = await _statusRepository.GetByIdAsync(ID);
            var getJob = await _jobRepository.GetByIdAsync(getid.JobID);

            ViewBag.JobName = getJob.Title;
            ViewBag.Fullname = getid.Fullname;
            getid.Read = (int)Constants.ViewStatus.Read;
            await _statusRepository.UpdateAsync(getid);
            var user = await _userManagers.GetUserAsync(User);
            var company = await _companyRepository.GetByEmailAsync(user.Fullname);

            ViewBag.NameCPN = company.Name_company;
            ViewBag.Email = user.Email;
            var result = _statusRepository.CompanyCheckStatus(user.Email);
            ViewBag.Bell = result.Where(s => s.Read == 0).ToList();

            if (getid == null)
            {
                return NotFound();
            }
            return View(getid);
            
        }

        // Process delete job
        [HttpGet]
        public async Task<IActionResult> DeleteConfirmed_Status(int id)
        {
            await _statusRepository.DeleteAsync(id);
            return RedirectToAction(nameof(CompanyCheckStatus));

        }

        //*************************************************************************************
		[HttpPost]
		public async Task<IActionResult> SendEmailByAdmin(EmailData data, int ID)
		{
            var user = await _userManagers.GetUserAsync(User);
            var company = await _companyRepository.GetByEmailAsync(user.Fullname);
            //lay du lieeu de hien thi
			var getNameUser = await _statusRepository.GetByIdAsync(ID);
            var getJob = await _jobRepository.GetByIdAsync(getNameUser.JobID);
			data.From = "nhathaoha11@gmail.com";		
            data.Body = System.IO.File.ReadAllText("C:/Users/yukun/source/repos/TimViec/TimViec/wwwroot/Template/BodyEmail.html");
            data.Body = data.Body.Replace("{{UserName}}", getNameUser.Fullname)
                                 .Replace("{{Position}}", getJob.Title)
                                 .Replace("{{CompanyName}}", company.Name_company)
								 .Replace("{{Email}}", user.Email);

			var result = await SendMail.SendGmail(data.From, data.To, data.Subject, data.Body, "nhathaoha11@gmail.com", "gheh wppp gokl rmrn");

			return RedirectToAction(nameof(CompanyCheckStatus)); ;
		}
		//*******************************************************************************************

	}
}
