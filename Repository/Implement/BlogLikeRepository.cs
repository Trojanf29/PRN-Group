using Bloggie_Web.Data;
using Bloggie_Web.Models.Domain;
using Bloggie_Web.Repository.Contract;
using Microsoft.EntityFrameworkCore;

namespace Bloggie_Web.Repository.Implement
{
    public class BlogLikeRepository : IBlogLikeRepository
    {
        private readonly BloggieContext _context;

        public BlogLikeRepository(BloggieContext context)
        {
            _context = context;
        }

        public async Task AddBlogLike(Guid blogId, Guid userId)
        {
            var blogLike = new BlogPostLike
            {
                BlogPostId = blogId,
                UserId = userId,
            };

            await _context.Likes.AddAsync(blogLike);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<BlogPostLike>> GetLikesOfABlog(Guid blogId)
        {
            return await _context.Likes
                .Where(blogLike => blogLike.BlogPostId.Equals(blogId))
                .ToListAsync();
        }

        public async Task<int> GetTotalLikeOfABlog(Guid blogId)
        {
            return await _context.Likes
                .CountAsync(like => like.BlogPostId.Equals(blogId));
        }
    }
}
