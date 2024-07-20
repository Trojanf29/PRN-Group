using Bloggie_Web.Models.Shared;
using Bloggie_Web.Repository.Contract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace Bloggie_Web.Pages.Admin.Blogs
{
    [Authorize(Roles = "Admin")]
    public class AddModel : PageModel
    {
        private readonly IBlogRepository _repository;

        [BindProperty]
        public Models.ViewModels.BlogPost BlogPostRequest { get; set; }

        [BindProperty]
        public IFormFile FeatureImg { get; set; }

        [Required]
        [BindProperty]
        public string Tags { get; set; }

        public AddModel(IBlogRepository repository)
        {
            _repository = repository;
        }

        public async Task<IActionResult> OnPost()
        {
            ValidatePublishDate();
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var addedBlog = new Models.Domain.BlogPost
            {
                Heading = BlogPostRequest.Heading,
                Content = BlogPostRequest.Content,
                Author = BlogPostRequest.Author,
                FeatureImgUrl = BlogPostRequest.FeatureImgUrl,
                IsVisible = BlogPostRequest.IsVisible,
                PageTitle = BlogPostRequest.PageTitle,
                PublishedDate = BlogPostRequest.PublishedDate,
                ShortDesc = BlogPostRequest.ShortDesc,
                UrlHandle = BlogPostRequest.UrlHandle,
                Tags = new List<Models.Domain.Tag>(Tags.Split(',')
                .Select(tag => new Models.Domain.Tag { Name = tag.Trim()}))
            };

            if (await _repository.AddBlogPostAsync(addedBlog) != null)
            {
                var notificationMsg = new Notification
                {
                    Message = "New Blog Post Added Successfully!",
                    Status = NotificationType.Success
                };

                TempData["NotificationJson"] = JsonSerializer.Serialize(notificationMsg);
                return RedirectToPage("/Admin/Blogs/List");
            }
            return RedirectToPage("/Admin/Blogs/List");
        }

        private void ValidatePublishDate()
        {
            if (BlogPostRequest.PublishedDate.Date < DateTime.Now.Date)
            {
                ModelState.AddModelError("BlogPostRequest.PublishedDate",
                    $"The Published date can only be today or further!");
            }
        }
    }
}