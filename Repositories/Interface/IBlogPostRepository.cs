using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BloggieMvc.Models.Entities;

namespace BloggieMvc.Repositories.Interface
{
    public interface IBlogPostRepository
    {
        Task<BlogPost?> GetAsync(Guid id);
        Task<IEnumerable<BlogPost>> GetAllAsync();
        Task<BlogPost> AddAsync(BlogPost blogPost);
        Task<BlogPost?> UpdateAsync(BlogPost blogPost);
        Task<BlogPost?> DeleteAsync(Guid id);
        
    }
}