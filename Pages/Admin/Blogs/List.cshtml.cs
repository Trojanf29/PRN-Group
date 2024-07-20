using Bloggie_Web.Models.Domain;
using Bloggie_Web.Models.Shared;
using Bloggie_Web.Repository.Contract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace Bloggie_Web.Pages.Admin.Blogs
{
    [Authorize(Roles = "Admin")]
    public class ListModel : PageModel
    {
        private readonly IBlogRepository _repository;

        public ICollection<BlogPost> BlogPosts { get; set; } = new List<BlogPost>();

        public ListModel(IBlogRepository repository)
        {
            _repository = repository;
        }

        public async Task<IActionResult> OnGet()
        {
            var notificationJson = (string)TempData["NotificationJson"];
            if (notificationJson != null)
            {
                ViewData["Notification"] = JsonSerializer.Deserialize<Notification>(notificationJson);
            }
            BlogPosts = await _repository.GetAllBlogsAsync();
            return Page();
        }
    }
}