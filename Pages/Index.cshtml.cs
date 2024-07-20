using Bloggie_Web.Models.Domain;
using Bloggie_Web.Repository.Contract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bloggie_Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IBlogRepository _repository;

        public ICollection<BlogPost> BlogPosts { get; set; } = new List<BlogPost>();

        public IndexModel(ILogger<IndexModel> logger, IBlogRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task<IActionResult> OnGet()
        {
            BlogPosts = await _repository.GetAllBlogsAsync();
            return Page();
        }
    }
}