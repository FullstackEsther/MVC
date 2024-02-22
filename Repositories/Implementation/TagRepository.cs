using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BloggieMvc.Context;
using BloggieMvc.Models.Entities;
using BloggieMvc.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BloggieMvc.Repositories.Implementation
{
    public class TagRepository : ITagRepository
    {
        private readonly BloggieContext _bloggieContext;
        public TagRepository(BloggieContext bloggieContext)
        {
            _bloggieContext = bloggieContext;
        }
        public async Task<Tag> AddAsync(Tag tag)
        {
           await _bloggieContext.Tags.AddAsync(tag);
           await _bloggieContext.SaveChangesAsync();
           return tag;
        }

        public async Task<Tag?> DeleteAsync(Guid id)
        {
           var existingTag = await _bloggieContext.Tags.FindAsync(id);
           if (existingTag!= null)
           {
             _bloggieContext.Tags.Remove(existingTag);
             _bloggieContext.SaveChangesAsync();
             return existingTag;
           }
           return null;
            
        }

        public async Task<IEnumerable<Tag>> GetAllAsync()
        {
            return  await _bloggieContext.Tags.ToListAsync();
        }

        public async Task<Tag?> GetAsync(Guid id)
        {
            return await _bloggieContext.Tags.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Tag?> UpdateAsync(Tag tag)
        {
            var initialTag =await _bloggieContext.Tags.FindAsync(tag.Id);
           if (initialTag != null)
           {
                initialTag.Name = tag.Name;
                initialTag.DisplayName = tag.DisplayName;
                await _bloggieContext.SaveChangesAsync();
                return initialTag;
           }
           return null;
        }
    }
}