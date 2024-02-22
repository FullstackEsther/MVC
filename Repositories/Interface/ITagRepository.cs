using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BloggieMvc.Models.Entities;

namespace BloggieMvc.Repositories.Interface
{
    public interface ITagRepository
    {
        public Task<IEnumerable<Tag>> GetAllAsync();
        public Task<Tag?> GetAsync(Guid id);
        public Task<Tag> AddAsync(Tag tag);
        public Task<Tag?> UpdateAsync(Tag tag);
        public Task<Tag?> DeleteAsync(Guid id);

    }
}