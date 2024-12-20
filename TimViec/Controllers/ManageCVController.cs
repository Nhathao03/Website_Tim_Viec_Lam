using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TimViec.Models;
using TimViec.Repository;
using TimViec.Respository;
using TimViec.ViewModel;

namespace TimViec.Controllers
{
    public class ManageCVController : Controller
    {
        private readonly ITemplateRepository _templateRepository;
        private readonly ITypeCVRepository _typeCVRepository;
        private readonly ISectionRespository _sectionRespository;
        private readonly ICVsRepository _cvRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public ManageCVController(ITemplateRepository templateRepository,
            ITypeCVRepository typeCVRepository,
            ISectionRespository sectionRespository,
            ICVsRepository cVsRepository,
            UserManager<ApplicationUser> userManager)
        {
            _templateRepository = templateRepository;
            _typeCVRepository = typeCVRepository;
            _sectionRespository = sectionRespository;
            _cvRepository = cVsRepository;
            _userManager = userManager;
        }

        [HttpGet]
        [Authorize(Roles = "User")]
        public IActionResult ManageCV()
        {
            var getUser = _userManager.GetUserAsync(User);
            var getallCV = _cvRepository.ManageCV(getUser.Result.Id);       
            return View(getallCV);
        }

        [HttpGet]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> GetCVbyID(int id)
        {
            var renderCV = _cvRepository.GetTemplates_by_ID_CV(id);
            var sectionList = new List<Get_CV_ByCvid_ViewModelResult>();
            foreach (var item in renderCV)
            {
                sectionList.Add(new Get_CV_ByCvid_ViewModelResult()
                {
                    Id = item.Id,
                    CVID = item.CViD,
                    TypeID = item.TypeID,
                    Background = item.Background,
                    TypeName = item.TypeName,
                    Content = !string.IsNullOrEmpty(item.ContentJson) ? JsonConvert.DeserializeObject<dynamic>(item.ContentJson) : null,
                    Style = !string.IsNullOrEmpty(item.StyleJson) ? JsonConvert.DeserializeObject<dynamic>(item.StyleJson) : null,
                });
            }
            ViewBag.Background = sectionList.First().Background;
            return View(sectionList);
        }

       

        [HttpGet]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> UpdateCV(int id)
        {
            var renderCV = _cvRepository.GetTemplates_by_ID_CV(id);
            var sectionList = new List<Get_CV_ByCvid_ViewModelResult>();
            var getUsername = await _userManager.GetUserAsync(User);
            foreach (var item in renderCV)
            {
                sectionList.Add(new Get_CV_ByCvid_ViewModelResult()
                {
                    Id = item.Id,
                    CVID = item.CViD,
                    TypeID = item.TypeID,
                    Background = item.Background,
                    UserName = getUsername.Fullname,
                    TypeName = item.TypeName,
                    NameCV = item.NameCV,
                    Content = !string.IsNullOrEmpty(item.ContentJson) ? JsonConvert.DeserializeObject<dynamic>(item.ContentJson) : null,
                    Style = !string.IsNullOrEmpty(item.StyleJson) ? JsonConvert.DeserializeObject<dynamic>(item.StyleJson) : null,
                });
            }
            ViewBag.Background = sectionList.First().Background;
            return View(sectionList);
        }

        public async Task<IActionResult> DeleteCV(int id)
        {
            await _cvRepository.DeleteAsync(id);
            return RedirectToAction("ManageCV");
        }

    }
}
