using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TimViec.Models;
using TimViec.Repository;

namespace TimViec.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class TemplateController : Controller
    { 
        private readonly ITemplateRepository _templateRepository;
        private readonly ITypeCVRepository _typeCVRepository;
        public TemplateController(ITemplateRepository templateRepository,
            ITypeCVRepository typeCVRepository)
        {
            _templateRepository = templateRepository;
            _typeCVRepository = typeCVRepository;
        }
        // GET: TemplateController
        public ActionResult Index()
        {
            return View();
        }

        // GET: TemplateController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //Save image 
        private async Task<string> SaveImage(IFormFile image)
        {
            var savePath = Path.Combine("wwwroot/CV/background_image", image.FileName);
            using (var fileStream = new FileStream(savePath, FileMode.Create))
            {
                await image.CopyToAsync(fileStream);
            }
            return "CV/background_image/" + image.FileName;
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
        // GET: CVController/Create
        public async Task<IActionResult> AddNewTemplateSample()
        {
            var getList_category = await _typeCVRepository.GetAllAsync();
            ViewBag.List_category = new SelectList(getList_category, "Id", "Name");
            return PartialView("_PartialAddNewTemplateSampleCV");
        }

        //Add new template sample
        public async Task<IActionResult> AddNewTemplate(Template template)
        {
            try
            {
                await _templateRepository.AddAsync(template);
                return Json(new { success = true });
            }catch(Exception ex)
            {
                return Json ( new {success = false, error = ex.Message});
            }
        }

        // POST: TemplateController/Create
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

        // GET: TemplateController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: TemplateController/Edit/5
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

        // GET: TemplateController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: TemplateController/Delete/5
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
