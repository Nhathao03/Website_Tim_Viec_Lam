using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using TimViec.Models;

namespace TimViec.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LogoutModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<LogoutModel> _logger;
        private readonly UserManager<ApplicationUser> _userManagers;

        public LogoutModel(SignInManager<ApplicationUser> signInManager, ILogger<LogoutModel> logger, UserManager<ApplicationUser> userManagers)
        {
            _signInManager = signInManager;
            _logger = logger;
            _userManagers = userManagers;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost(string returnUrl = null)
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("Logout.");

            if (returnUrl != null)
            {
                return LocalRedirect(Url.Content("/Identity/Account/Login"));
            }
            else
            {
                return RedirectToPage();
            }
            
        }
    }
}
