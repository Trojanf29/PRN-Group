using System.ComponentModel.DataAnnotations;

namespace Bloggie_Web.Models.Domain
{
    public class BlogPostLike
    {
        [Key]
        public Guid Id { get; set; }
        public Guid BlogPostId { get; set; }
        public Guid UserId { get; set; }
    }
}
