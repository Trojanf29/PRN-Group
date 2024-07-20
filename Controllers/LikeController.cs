using Bloggie_Web.Models.ViewModels;
using Bloggie_Web.Repository.Contract;
using Microsoft.AspNetCore.Mvc;

namespace Bloggie_Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LikeController : Controller
    {
        private readonly IBlogLikeRepository _repository;

        public LikeController(IBlogLikeRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [Route("{blogId:Guid}/total-likes")]
        public async Task<IActionResult> GetTotalLike([FromRoute] Guid blogId)
        {
            var totalLikes = await _repository.GetTotalLikeOfABlog(blogId);
            return Ok(totalLikes);
        }


        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> AddBlogLike([FromBody] AddBlogLikeRequest request)
        {
            await _repository.AddBlogLike(request.BlogId, request.UserId);
            return Ok();
        }
    }
}
