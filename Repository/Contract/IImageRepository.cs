namespace Bloggie_Web.Repository.Contract
{
    public interface IImageRepository
    {
        Task<string> UploadAsync(IFormFile file);
    }
}
