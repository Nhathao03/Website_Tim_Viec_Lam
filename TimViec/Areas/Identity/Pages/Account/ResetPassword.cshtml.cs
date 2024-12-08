using System;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using TimViec.Models;

namespace TimViec.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class ResetPasswordModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager; // Sử dụng ApplicationUser thay vì IdentityUser

        public ResetPasswordModel(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

            public string Code { get; set; }
        }

        // Phương thức GET để nhận mã reset password
        public IActionResult OnGet(string code = null)
        {
            if (code == null)
            {
                return BadRequest("A code must be supplied for password reset.");
            }
            else
            {
                Input = new InputModel
                {
                    Code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code)) // Giải mã mã reset password
                };
                return Page();
            }
        }

        // Phương thức POST để reset password
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Tìm người dùng qua email
            var user = await _userManager.FindByEmailAsync(Input.Email);
            if (user == null)
            {
                // Không thông báo người dùng không tồn tại vì lý do bảo mật
                return RedirectToPage("./ResetPasswordConfirmation");
            }

            // Reset mật khẩu
            var result = await _userManager.ResetPasswordAsync(user, Input.Code, Input.Password);
            if (result.Succeeded)
            {
                return RedirectToPage("./ResetPasswordConfirmation");
            }

            // Nếu lỗi, hiển thị thông báo lỗi cho người dùng
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return Page();
        }
    }
}
