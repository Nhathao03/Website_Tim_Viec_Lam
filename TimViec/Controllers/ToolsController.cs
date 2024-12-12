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
						TypeName = item.TypeName,
						Content = !string.IsNullOrEmpty(item.ContentJson) ? JsonConvert.DeserializeObject<dynamic>(item.ContentJson) : null,
						Style = !string.IsNullOrEmpty(item.StyleJson) ? JsonConvert.DeserializeObject<dynamic>(item.StyleJson) : null,
					});
				}
				return View(sectionList);
        }

        // POST: CreateCVController/Create
        [HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(IFormCollection collection)
		{
			try
			{
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}

		// GET: CreateCVController/Edit/5
		public ActionResult Edit(int id)
		{
			return View();
		}

		// POST: CreateCVController/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(int id, IFormCollection collection)
		{
			try
			{
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}

		// GET: CreateCVController/Delete/5
		public ActionResult Delete(int id)
		{
			return View();
		}

		// POST: CreateCVController/Delete/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Delete(int id, IFormCollection collection)
		{
			try
			{
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}
	}
}
