using Bloggie_Web.Models.Shared;
using Bloggie_Web.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bloggie_Web.Pages
{
    public class LoginModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;

        [BindProperty]
        public LoginViewModel Login { get; set; }

        public LoginModel(SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost(string? ReturnUrl)
        {

            if (!ModelState.IsValid)
            {
                return Page();
            }

            var signInResult = await _signInManager
                .PasswordSignInAsync(Login.Username, Login.Password, false, false);    

            if (signInResult.Succeeded)
            {
                if (!string.IsNullOrEmpty(ReturnUrl))
                {
                    return RedirectToPage(ReturnUrl);

                }
                return RedirectToPage("Index");
            }

            ViewData["Notification"] = new Notification
            {
                Message = "Login failed",
                Status = NotificationType.Error
            };
            return Page();
        }
    }
}
