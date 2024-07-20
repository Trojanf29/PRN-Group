using Bloggie_Web.Models.Shared;
using Bloggie_Web.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bloggie_Web.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;

        [BindProperty]
        public RegisterViewModel Register { get; set; }
        public RegisterModel(UserManager<IdentityUser> userManager, ILogger<RegisterModel> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = new IdentityUser()
            {
                UserName = Register.Username,
                Email = Register.Email
            };
            var identityUser = await _userManager.CreateAsync(user, Register.Password);

            if (identityUser.Succeeded)
            { 
                var userAfterAddRole =  await _userManager.AddToRoleAsync(user, "User");
                if (userAfterAddRole.Succeeded)
                {
                    ViewData["Notification"] = new Notification()
                    {
                        Message = "Register successfully!",
                        Status = NotificationType.Success,
                    };
                    return Page();   
                }
            }

            ViewData["Notification"] = new Notification()
            {
                Message = "Register failed",
                Status = NotificationType.Error,
            };

            return Page();

        }
    }
}
