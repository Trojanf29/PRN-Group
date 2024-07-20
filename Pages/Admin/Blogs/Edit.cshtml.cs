using Bloggie_Web.Models.Shared;
using Bloggie_Web.Models.ViewModels;
using Bloggie_Web.Repository.Contract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace Bloggie_Web.Pages.Admin.Blogs
{
    [Authorize(Roles = "Admin")]
    public class EditModel : PageModel
    {
        private readonly IBlogRepository _repository;

        [BindProperty]
        public EditBlogPostViewModel EditBlog { get; set; }
        public IFormFile FeatureImg { get; set; }

        [BindProperty]
        [Required]
        public string Tags { get; set; }

        public EditModel(IBlogRepository repository)
        {
            _repository = repository;
        }

        public async Task<IActionResult> OnGet([FromRoute] Guid blogId)
        {
            var blogPost = await _repository.GetBlogPostAsync(blogId);

            if (blogPost != null && blogPost.Tags != null)
            {
                EditBlog = new EditBlogPostViewModel
                {
                    Id = blogPost.Id,
                    Heading = blogPost.Heading,
                    Content = blogPost.Content,
                    Author = blogPost.Author,
                    FeatureImgUrl = blogPost.FeatureImgUrl,
                    IsVisible = blogPost.IsVisible,
                    PageTitle = blogPost.PageTitle,
                    PublishedDate = blogPost.PublishedDate,
                    ShortDesc = blogPost.ShortDesc,
                    UrlHandle = blogPost.UrlHandle,
                };
                Tags = string.Join(",", blogPost.Tags.Select(tag => tag.Name));
            }
            return Page();
        }

        public async Task<IActionResult> OnPostEdit()
        {
            try
            {
                var blog = new Models.Domain.BlogPost
                {
                    Id = EditBlog.Id,
                    Heading = EditBlog.Heading,
                    Content = EditBlog.Content,
                    Author = EditBlog.Author,
                    FeatureImgUrl = EditBlog.FeatureImgUrl,
                    IsVisible = EditBlog.IsVisible,
                    PageTitle = EditBlog.PageTitle,
                    PublishedDate = EditBlog.PublishedDate,
                    ShortDesc = EditBlog.ShortDesc,
                    UrlHandle = EditBlog.UrlHandle,
                    Tags = new List<Models.Domain.Tag>(Tags.Split(",")
                    .Select(tag => new Models.Domain.Tag() { Name = tag.Trim() }))
            };

                await _repository.UpdateBlogPostAsync(blog);
                ViewData["Notification"] = new Notification
                {
                    Message = "Updated blog successfully!",
                    Status = NotificationType.Success
                };
            }
            catch (Exception)
            {
                ViewData["Notification"] = new Notification
                {
                    Message = "Update blog failed!",
                    Status = NotificationType.Error
                };
            }
            return Page();
        }

        public async Task<IActionResult> OnPostDelete()
        {
             try
            {
                await _repository.DeleteBlogPostAsync(EditBlog.Id);
                ViewData["Notification"] = new Notification
                {
                    Message = "Delete blog successfully!",
                    Status = NotificationType.Success
                };
            }
            catch (Exception)
            {
                ViewData["Notification"] = new Notification
                {
                    Message = "Delete blog failed!",
                    Status = NotificationType.Error
                };
            }
            return Page();
        }
    }
}