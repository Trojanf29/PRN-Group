namespace Bloggie_Web.Models.Domain
{
    public class BlogPost
    {
        public Guid Id { get; set; }
        public string Heading { get; set; } = string.Empty;
        public string PageTitle { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public string ShortDesc { get; set; } = string.Empty;
        public string FeatureImgUrl { get; set; } = string.Empty;
        public string UrlHandle { get; set; } = string.Empty;
        public DateTime PublishedDate { get; set; }
        public string Author { get; set; } = string.Empty;
        public bool IsVisible { get; set; }

        //n-n relationship between Tag
        public ICollection<Tag> Tags { get; set; }

        //1-n
        public ICollection<BlogPostLike> Likes { get; set; }

        //1-n
        public ICollection<BlogPostComment> Comments { get; set; }
    }
}