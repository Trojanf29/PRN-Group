using Bloggie_Web.Repository.Contract;
using Microsoft.AspNetCore.Mvc;

namespace Bloggie_Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ImagesController : Controller
    {   
        private readonly IImageRepository _repository;

        public ImagesController(IImageRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        public async Task<IActionResult> UploadAsync(IFormFile file)
        {
            var imgUrl = await _repository.UploadAsync(file);
            if (imgUrl == null)
            {
                return BadRequest("Something went wrong...");
            }
            return Json(new {link =  imgUrl});
        }
    }
}
