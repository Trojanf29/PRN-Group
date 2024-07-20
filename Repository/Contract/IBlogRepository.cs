using Bloggie_Web.Models.Domain;

namespace Bloggie_Web.Repository.Contract
{
    public interface IBlogRepository
    {
        Task<ICollection<BlogPost>> GetAllBlogsAsync();
        
        Task<IEnumerable<BlogPost> > GetAllBlogWithTagNameAsync(string tagName);

        Task<BlogPost> GetBlogPostAsync(Guid id);

        Task<BlogPost> GetBlogPostAsync(string urlHandle);

        Task<BlogPost> AddBlogPostAsync(BlogPost blog);

        Task<BlogPost> UpdateBlogPostAsync(BlogPost blog);

        Task<bool> DeleteBlogPostAsync(Guid id);
    }
}