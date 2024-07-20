using System.ComponentModel.DataAnnotations;

namespace Bloggie_Web.Models.Domain
{
    public class BlogPostComment
    {
        [Key]
        public Guid Id { get; set; }
        public string Content { get; set; } = string.Empty;
        public DateTime CommentDate { get; set; }
        public Guid BlogPostId { get; set; }
        public Guid UserId { get; set; }
    }
}
