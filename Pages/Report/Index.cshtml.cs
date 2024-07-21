using Bloggie_Web.Data;
using Bloggie_Web.Models.Domain;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Bloggie_Web.Pages.Report
{
    public class IndexModel : PageModel
    {
        private readonly BloggieContext _context;

        public IndexModel(BloggieContext context)
        {
            _context = context;
        }

        public int TotalBlogPosts { get; set; }
        public int TotalLikes { get; set; }
        public int TotalComments { get; set; }
        public Dictionary<Models.Domain.Tag, int> PostsByTag { get; set; } = new Dictionary<Models.Domain.Tag, int>();
        public List<BlogPost> RecentPosts { get; set; }

        public async Task OnGetAsync()
        {
            TotalBlogPosts = await _context.BlogPosts.CountAsync();
            TotalLikes = _context.BlogPosts.Select(b => b.Likes.Count).ToList().Sum();
            TotalComments = _context.BlogPosts.Select(b => b.Comments.Count).ToList().Sum();

            foreach (var blog in _context.BlogPosts.Include(_ => _.Tags))
            {
                foreach (var tag in blog.Tags)
                {
                    if (PostsByTag.ContainsKey(tag))
                    {
                        PostsByTag[tag]++;
                    }
                    else
                    {
                        PostsByTag.Add(tag, 1);
                    }
                }
            }

            RecentPosts = await _context.BlogPosts
                .OrderByDescending(b => b.PublishedDate)
                .Take(5)
                .ToListAsync();
        }
    }
}
