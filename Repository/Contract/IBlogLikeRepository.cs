using Bloggie_Web.Models.Domain;

namespace Bloggie_Web.Repository.Contract
{
    public interface IBlogLikeRepository
    {
        Task<int> GetTotalLikeOfABlog(Guid blogId);
        Task AddBlogLike(Guid blogId, Guid userId);
        Task<IEnumerable<BlogPostLike>> GetLikesOfABlog(Guid blogId);
    }
}
