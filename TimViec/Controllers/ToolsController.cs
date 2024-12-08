using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TimViec.Models;
using TimViec.Repository;
using TimViec.Respository;

namespace TimViec.Controllers
{

    public class ToolsController : Controller
	{
        private readonly ITemplateRepository _templateRepository;
        private readonly ITypeCVRepository _typeCVRepository;
        private readonly ISectionRespository _sectionRespository;
        private readonly UserManager<ApplicationUser> _userManager;
        public ToolsController(ITemplateRepository templateRepository,
            ITypeCVRepository typeCVRepository,
			ISectionRespository sectionRespository,
			UserManager<ApplicationUser> userManager)
        {
            _templateRepository = templateRepository;
            _typeCVRepository = typeCVRepository;
			_sectionRespository = sectionRespository;
			_userManager = userManager;
        }
        // GET: CreateCVController
        public async Task<IActionResult> ListTemplateCV()
		{
			var getAll_TemplateCV = _templateRepository.GetAllInformation_Table_Template_and_TypeCV();
            var getHTML_Template = await _templateRepository.GetByIdAsync(14);
            ViewBag.HTML = getHTML_Template.HtmlTemplate;
            var getStyle_Template = await _sectionRespository.GetByIdAsync(2);
            ViewBag.Style = getStyle_Template.StyleJson;
            return View(getAll_TemplateCV);
		}
		

		// GET: CreateCVController/Details/5
		public ActionResult Details(int id)
		{
			return View();
		}

		// GET: CreateCVController/Create
		public async Task<IActionResult> CreateCV()
		{
			var getUser = await _userManager.GetUserAsync(User);
			var getHTML_Template = await _templateRepository.GetByIdAsync(14);
			ViewBag.HTML = getHTML_Template.HtmlTemplate;
			var getStyle_Template = await _sectionRespository.GetByIdAsync(2);
			ViewBag.Style = getStyle_Template.StyleJson;
            return PartialView("_PartialCreateCV",getUser);
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
