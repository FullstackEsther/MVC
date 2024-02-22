using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace BloggieMvc.Repositories.Interface
{
    public interface IImageRepository
    {
        Task<string> UploadAsync(IFormFile formfile);
    }
}