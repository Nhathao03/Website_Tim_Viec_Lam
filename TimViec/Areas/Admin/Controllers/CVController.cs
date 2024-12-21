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
	}
}
