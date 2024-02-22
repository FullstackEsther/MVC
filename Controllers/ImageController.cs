using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using BloggieMvc.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;

namespace BloggieMvc.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ImageController : ControllerBase
    {
        private readonly IImageRepository _imageRepository;
        public ImageController(IImageRepository imageRepository)
        {
            _imageRepository = imageRepository;
        }
        [HttpPost]
        public async Task<IActionResult> UploadImages(IFormFile formFile)
        {
            //call a repository
            var imageURL = await _imageRepository.UploadAsync(formFile);
            if (imageURL == null)
            {
                return Problem("Something went wrong!", null, (int)HttpStatusCode.InternalServerError);
            }
            return new JsonResult(new{link = imageURL});
        }
    }
}