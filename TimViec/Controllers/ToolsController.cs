using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using TimViec.Models;
using TimViec.Repository;
using TimViec.Respository;
using TimViec.ViewModel;

namespace TimViec.Controllers
{

    public class ToolsController : Controller
	{
        private readonly ITemplateRepository _templateRepository;
        private readonly ITypeCVRepository _typeCVRepository;
        private readonly ISectionRespository _sectionRespository;
        private readonly ICVsRepository _cvRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        public ToolsController(ITemplateRepository templateRepository,
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
        // Get list template
        public async Task<IActionResult> ListTemplateCV()
		{
			var getAll_TemplateCV = _templateRepository.GetAllInformation_Table_Template_and_TypeCV();
            return View(getAll_TemplateCV);
		}

        [HttpGet]
		// Get template by category
		public async Task<IActionResult> Get_template_by_category(int category_id)
		{
			var getTemplate_by_category = _templateRepository.Get_Template_By_Categories(category_id);
			return PartialView("_Partial_GetTemplate_by_category",getTemplate_by_category);
			
		}

		
        [HttpGet]
        public async Task<IActionResult> RenderCreateCV(int id)
        {
			var renderCV = _cvRepository.GetTemplates_by_ID_CV(id);
			var sectionList = new List<Get_CV_ByCvid_ViewModelResult>();

			foreach (var item in renderCV)
			{
				sectionList.Add(new Get_CV_ByCvid_ViewModelResult()
				{
					Id = item.Id,
					TypeID = item.TypeID,
					TypeName = item.TypeName,
					Content = !string.IsNullOrEmpty(item.ContentJson) ? JsonConvert.DeserializeObject<dynamic>(item.ContentJson) : null,
					Style = !string.IsNullOrEmpty(item.StyleJson) ? JsonConvert.DeserializeObject<dynamic>(item.StyleJson) : null,
				});
			}
			return View(sectionList);
        }

        [HttpPost]
        public JsonResult SaveCV1([FromBody] SectionAvatar data)
        {
            return Json(new { Success = true });
        }

        // POST: CreateCVController/Create
        [HttpPost]
		public JsonResult SaveCV([FromBody] SaveCVRequest request)
		{
			try
			{

				var sectionProfile = new SectionProfile();
				//if(!string.IsNullOrEmpty(profile.Email)) {
				//	sectionProfile.Email = profile.Email;
				//}
				//if(!string.IsNullOrEmpty(profile.Phone)) {
				//	sectionProfile.Email = profile.Phone;
				//}
				//if(!string.IsNullOrEmpty(profile.Website)) {
				//	sectionProfile.Email = profile.Website;
				//}
				//if(!string.IsNullOrEmpty(profile.Address)) {
				//	sectionProfile.Email = profile.Address;
				//}


				var dataSection = new Sections();
				

				return Json(new { Success = true  });
			}
			catch
			{
				return Json(new { Success = false  });
			}
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

        [HttpPost]
        public async Task<JsonResult> UploadImage(IFormFile file)
        {
            var path = await SaveImage(file);

            if (!string.IsNullOrEmpty(path))
            {
                return Json(new { success = true, imageUrl = path });
            }
            return Json(new { success = false });
        }
    }
}
