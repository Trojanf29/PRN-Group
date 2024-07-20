using Bloggie_Web.Models.Domain;
using Bloggie_Web.Pages.Admin.Blogs;
using Bloggie_Web.Repository.Contract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bloggie_Web.Pages.Tag
{
    public class DetailsModel : PageModel
    {
        private readonly IBlogRepository _blogRepository;
        public IEnumerable<BlogPost> Blogs { get; set; } = new List<BlogPost>();

        public DetailsModel(IBlogRepository blogRepository)
        {
            _blogRepository = blogRepository;
        }
        public async Task<IActionResult> OnGetAsync(string tagName)
        {
            Blogs = await _blogRepository.GetAllBlogWithTagNameAsync(tagName);
            return Page();
        }
    }
}
