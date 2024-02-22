using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BloggieMvc.Repositories.Interface;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

namespace BloggieMvc.Repositories.Implementation
{
    public class CloudinaryImageRepository : IImageRepository
    {
        private readonly IConfiguration _configuration;
        private readonly Account _account;
        public CloudinaryImageRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _account = new Account(
            _configuration.GetSection("Cloudinary")["CloudName"],
            _configuration.GetSection("Cloudinary")["ApiKey"],
            _configuration.GetSection("Cloudinary")["ApiSecret"]);
        }
        public async Task<string> UploadAsync(IFormFile formfile)
        {
            var client = new Cloudinary(_account);
            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(formfile.FileName, formfile.OpenReadStream()),
                DisplayName = formfile.FileName
            };
            var uploadResult = await client.UploadAsync(uploadParams);

            if (uploadResult != null && uploadResult.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return uploadResult.SecureUri.ToString();
            }
            return null;
        }
    }
}