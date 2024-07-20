using System.ComponentModel.DataAnnotations;

namespace Bloggie_Web.Models.ViewModels
{
    public class BlogPost
    {
        [Required]
        public string Heading { get; set; } = string.Empty;
        [Required(ErrorMessage = "The title of the page is required")]
        public string PageTitle { get; set; } = string.Empty;
        [Required]
        public string Content { get; set; } = string.Empty;
        [Required(ErrorMessage = "Short description field is required")]
        public string ShortDesc { get; set; } = string.Empty;
        [Required(ErrorMessage = "Feature image URL field is required")]
        public string FeatureImgUrl { get; set; } = string.Empty;
        [Required(ErrorMessage = "URL handle field for blog post is required")]
        public string UrlHandle { get; set; } = string.Empty;
        [Required(ErrorMessage = "Published date field is required")]
        public DateTime PublishedDate { get; set; }
        [Required]
        public string Author { get; set; } = string.Empty;
        public bool IsVisible { get; set; }
    }
}