using Bloggie_Web.Repository.Contract;
using CloudinaryDotNet;

namespace Bloggie_Web.Repository.Implement
{
    public class ImageRepository : IImageRepository
    {
        private readonly Account account;
        public ImageRepository(IConfiguration config)
        {
            account = new Account(
                config.GetSection("Cloudinary")["CloudName"],
                config.GetSection("Cloudinary")["ApiKey"],
                config.GetSection("Cloudinary")["SecretKey"]);
        }
        public async Task<string?> UploadAsync(IFormFile file)
        {
            var client = new Cloudinary(account);
            var uploadFileResult = await client.UploadAsync(
                new CloudinaryDotNet.Actions.ImageUploadParams()
                {
                    File = new FileDescription(file.FileName, file.OpenReadStream()),
                    DisplayName = file.FileName
                });

            if(uploadFileResult != null && uploadFileResult.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return uploadFileResult.SecureUrl.ToString();
            }
            return null;
        }
    }
}
