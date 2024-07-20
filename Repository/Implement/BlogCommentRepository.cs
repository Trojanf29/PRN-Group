using Bloggie_Web.Data;
using Bloggie_Web.Models.Domain;
using Bloggie_Web.Repository.Contract;
using Microsoft.EntityFrameworkCore;

namespace Bloggie_Web.Repository.Implement
{
    public class BlogCommentRepository : IBlogCommentRepository
    {
        private readonly BloggieContext _context;

        public BlogCommentRepository(BloggieContext context)
        {
            _context = context;
        }
        public async Task<BlogPostComment> AddCommentAsync(BlogPostComment comment)
        {
            await _context.Comments.AddAsync(comment);
            await _context.SaveChangesAsync();
            return comment;
        }

        public async Task<IEnumerable<BlogPostComment>> GetCommentsAsync(Guid blogId)
        {
            return await _context.Comments.Where(comment => comment.BlogPostId.Equals(blogId))
                .ToListAsync();
        }
    }
}
