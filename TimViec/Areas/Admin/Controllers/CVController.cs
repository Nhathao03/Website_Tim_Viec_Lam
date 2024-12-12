using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TimViec.Models;
using TimViec.Repository;

namespace TimViec.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class CVController : Controller
	{
		private readonly ITemplateRepository _templateRepository;
		private readonly ITypeCVRepository _typeCVRepository;
        public CVController(ITemplateRepository templateRepository,
            ITypeCVRepository typeCVRepository)
        {
            _templateRepository = templateRepository;
            _typeCVRepository = typeCVRepository;
        }
        // GET: CVController 
        public IActionResult ListTemplateCV()
		{
			var getLis_TemplateCV = _templateRepository.Get_ListTemplates();
			return View(getLis_TemplateCV);
		}

		// GET: CVController/Details/5
		public ActionResult Details(int id)
		{
			return View();
		}

		

		// POST: CVController/Create
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

		// GET: CVController/Edit/5
		public ActionResult Edit(int id)
		{
			return View();
		}

		// POST: CVController/Edit/5
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

		// GET: CVController/Delete/5
		public ActionResult Delete(int id)
		{
			return View();
		}

		// POST: CVController/Delete/5
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
