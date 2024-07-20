using Bloggie_Web.Models.Domain;
using Bloggie_Web.Models.ViewModels;
using Bloggie_Web.Repository.Contract;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace Bloggie_Web.Pages.Blog
{
    public class BlogDetailModel : PageModel
    {
        private readonly IBlogRepository _repository;
        private readonly IBlogLikeRepository _likeRepository;
        private readonly IBlogCommentRepository _commentRepository;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;

        public Models.Domain.BlogPost Blog { get; set; }
        public int TotalBlogLikes { get; set; }
        public bool IsUserLiked { get; set; }

        public ICollection<BlogCommentViewModel> Comments { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "The comment should not be leave empty!")]
        [MinLength(5)]
        [MaxLength(255)]
        public string CommentContent { get; set; }

        [BindProperty]
        public Guid BlogPostId { get; set; }

        public BlogDetailModel(
            IBlogRepository repository,
            IBlogLikeRepository likeRepository,
            IBlogCommentRepository commentRepository,
            SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager
        )
        {
            _repository = repository;
            _likeRepository = likeRepository;
            _commentRepository = commentRepository;
            _signInManager = signInManager;
            _userManager = userManager;
            Comments = new List<BlogCommentViewModel>();
        }
        public async Task<IActionResult> OnGet(string urlHandle)
        {
            await GetComments(urlHandle);
            return Page();
        }

        public async Task<IActionResult> OnPostComment(string urlHandle)
        {
            if (!ModelState.IsValid)
            {
                await GetComments(urlHandle);
                return Page();
            }

            if (_signInManager.IsSignedIn(User))
            {
                var userId = _userManager.GetUserId(User);
                var comment = new BlogPostComment
                {
                    BlogPostId = BlogPostId,
                    Content = CommentContent,
                    CommentDate = DateTime.Now,
                    UserId = Guid.Parse(userId)
                };
                await _commentRepository.AddCommentAsync(comment);
            }
            return RedirectToPage("/Blog/BlogDetail", new { urlHandle });
        }

        private async Task getComments()
        {
            var comments = await _commentRepository.GetCommentsAsync(Blog.Id);
            var commentVMs = new List<BlogCommentViewModel>();

            foreach (var comment in comments)
            {
                commentVMs.Add(new BlogCommentViewModel
                {
                    CommentDate = comment.CommentDate,
                    Content = comment.Content,
                    Username = (await _userManager.FindByIdAsync(comment.UserId.ToString())).UserName
                });
            }

            Comments = commentVMs;
        }
        private async Task GetComments(string urlHandle)
        {
            Blog = await _repository.GetBlogPostAsync(urlHandle);

            if (Blog != null)
            {
                BlogPostId = Blog.Id;

                if (_signInManager.IsSignedIn(User))
                {
                    var list = await _likeRepository.GetLikesOfABlog(Blog.Id);

                    var userId = _userManager.GetUserId(User);

                    IsUserLiked = list.Any(list => list.UserId.Equals(Guid.Parse(userId)));
                    await getComments();
                }
                TotalBlogLikes = await _likeRepository.GetTotalLikeOfABlog(Blog.Id);
            }
        }
    }
}
