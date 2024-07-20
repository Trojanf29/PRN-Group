using Bloggie_Web.Models.ViewModels;
using Bloggie_Web.Repository.Contract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bloggie_Web.Pages.Admin.Users
{
    [Authorize(Roles = "SuperAdmin")]
    public class IndexModel : PageModel
    {
        private readonly IUserRepository _repository;

        public ICollection<UserViewModel> Users { get; set; } = new List<UserViewModel>();

        [BindProperty]
        public UserAddViewModal AddedUser { get; set; }

        [BindProperty]
        public Guid SeletedUserId { get; set; }

        public IndexModel(IUserRepository repository)
        {
            _repository = repository;
        }
        public async Task<IActionResult> OnGet()
        {
            await getUsers();
            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                await getUsers();
                return Page();
            }
            var roles = new List<string> { "User" };
            if (AddedUser.IsAdmin)
            {
                roles.Add("Admin");
            }

            var isSucced = await _repository.AddUser(
            new IdentityUser
            {
                UserName = AddedUser.Username,
                Email = AddedUser.Email
            }, AddedUser.Password, roles);

            if (isSucced)
            {
                return RedirectToPage("/Admin/Users/Index");
            }
            return Page();
        }

        public async Task<IActionResult> OnPostDelete()
        {
            await _repository.DeleteUser(SeletedUserId);
            return RedirectToPage("/Admin/Users/Index");
        }

        private async Task getUsers()
        {
            var users = await _repository.GetAllUser();

            Users = new List<UserViewModel>();

            foreach (var user in users)
            {
                Users.Add(new UserViewModel
                {
                    Id = Guid.Parse(user.Id.ToString()),
                    Username = user.UserName,
                    Email = user.Email
                });
            }
        }
    }
}
