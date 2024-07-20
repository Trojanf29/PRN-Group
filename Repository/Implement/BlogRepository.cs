using Bloggie_Web.Data;
using Bloggie_Web.Models.Domain;
using Bloggie_Web.Repository.Contract;
using Microsoft.EntityFrameworkCore;

namespace Bloggie_Web.Repository.Implement
{
    public class BlogRepository : IBlogRepository
    {
        private readonly BloggieContext _context;

        public BlogRepository(BloggieContext context)
        {
            _context = context;
        }

        public async Task<BlogPost?> AddBlogPostAsync(BlogPost blog)
        {
            var isExist = await GetBlogPostAsync(blog.Id);
            if (isExist == null)
            {
                await _context.AddAsync(blog);
                await _context.SaveChangesAsync();
                return blog;
            }
            return null;
        }

        public async Task<bool> DeleteBlogPostAsync(Guid id)
        {
            var deleteBlog = await GetBlogPostAsync(id);
            if (deleteBlog != null)
            {
                _context.BlogPosts.Remove(deleteBlog);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<ICollection<BlogPost>> GetAllBlogsAsync()
        {
            return await _context.BlogPosts
                .Include(nameof(BlogPost.Tags))
                .ToListAsync();
        }

        public async Task<IEnumerable<BlogPost>> GetAllBlogWithTagNameAsync(string tagName)
        {
            return await _context.BlogPosts
                .Include(nameof(BlogPost.Tags))
                .Where(blog => blog.Tags.Any(tag => tag.Name.Equals(tagName)))
                .ToListAsync();
        }

        public async Task<BlogPost> GetBlogPostAsync(Guid id)
        {
            return await _context.BlogPosts
                .Include(nameof(BlogPost.Tags))
                .FirstOrDefaultAsync(blog => blog.Id.Equals(id));
        }

        public async Task<BlogPost> GetBlogPostAsync(string urlHandle)
        {
            return await _context.BlogPosts
                .Include(nameof(BlogPost.Tags))
                .FirstOrDefaultAsync(blog => blog.UrlHandle.Equals(urlHandle));
        }

        public async Task<BlogPost> UpdateBlogPostAsync(BlogPost blog)
        {

            var existingBlog = await _context.BlogPosts
                .Include(nameof(BlogPost.Tags))
                .FirstOrDefaultAsync(b => b.Id.Equals(blog.Id));

            if (existingBlog != null)
            {
                existingBlog.Heading = blog.Heading;
                existingBlog.PageTitle = blog.PageTitle;
                existingBlog.ShortDesc = blog.ShortDesc;
                existingBlog.Author = blog.Author;
                existingBlog.PublishedDate = blog.PublishedDate;
                existingBlog.IsVisible = blog.IsVisible;
                existingBlog.FeatureImgUrl = blog.FeatureImgUrl;
                existingBlog.UrlHandle = blog.UrlHandle;
                existingBlog.Content = blog.Content;

                if (blog.Tags != null && blog.Tags.Any())
                {
                    //var tagIds = blog.Tags.Select(tag => tag.Id).ToList(); //not a good idea since it pull all the tags id from the db
                    //var tagsToRemove = _context.Tags.Where(tag => tag.BlogPostId == blog.Id && !tagIds.Contains(tag.Id));
                    _context.Tags.RemoveRange(existingBlog.Tags);

                    blog.Tags.ToList().ForEach(tag => tag.BlogPostId = existingBlog.Id);
                    await _context.Tags.AddRangeAsync(blog.Tags);
                }
            }
            await _context.SaveChangesAsync();

            return existingBlog;
        }
    }
}