using Bloggie_Web.Models.Shared;
using Bloggie_Web.Models.ViewModels;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Authentication.Google;

namespace Bloggie_Web.Pages
{
    public class LoginModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;

        [BindProperty]
        public LoginViewModel Login { get; set; }

        public LoginModel(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostLoginAsync(string? ReturnUrl)
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

        public IActionResult OnPostOAuth()
        {
            return Challenge(
                new AuthenticationProperties() { RedirectUri = $"/api/auth/signin-google" },
                GoogleDefaults.AuthenticationScheme
            );
        }
    }
}
