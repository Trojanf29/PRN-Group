using Bloggie_Web.Models.Domain;

namespace Bloggie_Web.Repository.Contract
{
    public interface IBlogCommentRepository
    {
        Task<BlogPostComment> AddCommentAsync(BlogPostComment comment);
        Task<IEnumerable<BlogPostComment>> GetCommentsAsync(Guid blogId);
    }
}
