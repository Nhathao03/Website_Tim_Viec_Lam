using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TimViec.Models;
using TimViec.Repository;
using TimViec.Respository;
using static TimViec.Helpers.Constants;

namespace TimViec.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class TypeCVController : Controller
    {
        private readonly ITypeCVRepository _typeCVRepository;

        public TypeCVController(ITypeCVRepository typeCVRepository)
        {
            _typeCVRepository = typeCVRepository;
        }

        //Get all category in table
        [HttpGet]
        public async Task<IActionResult> ListNew_TypeCV()
        {
            var getList_TypeCV = await _typeCVRepository.GetAllAsync();
            return PartialView("_PartialAddNew_TypeCV", getList_TypeCV);
        }

        //Add new category cv
        [HttpPost]
        public async Task<IActionResult> AddNew_TypeCV(TypeCV type)
        {
            try
            {
                await _typeCVRepository.AddAsync(type);
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex.Message });
            }
        }

        //Delete category cv
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _typeCVRepository.DeleteAsync(id);
                return Json(new { success = true });
            }catch(Exception ex)
            {
                return Json(new { success = false, error = ex.Message });
            }           
        }

        //Save image 
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
