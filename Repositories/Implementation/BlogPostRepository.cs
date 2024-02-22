using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BloggieMvc.Context;
using BloggieMvc.Models.Entities;
using BloggieMvc.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace BloggieMvc.Repositories.Implementation
{

    public class BlogPostRepository : IBlogPostRepository
    {
        private readonly BloggieContext _bloggieContext;
        public BlogPostRepository(BloggieContext bloggieContext)
        {
            _bloggieContext = bloggieContext;
        }
        public async Task<BlogPost> AddAsync(BlogPost blogPost)
        {
            await _bloggieContext.BlogPosts.AddAsync(blogPost);
            await _bloggieContext.SaveChangesAsync();
            return blogPost;
        }

        public async Task<BlogPost?> DeleteAsync(Guid id)
        {
           var existingBlog= await _bloggieContext.BlogPosts.FindAsync(id);
           if (existingBlog != null)
           {
             _bloggieContext.Remove(existingBlog);
             await _bloggieContext.SaveChangesAsync();
             return existingBlog;
           }
           return null;
        }

        public async Task<IEnumerable<BlogPost>> GetAllAsync()
        {
            return await _bloggieContext.BlogPosts.Include(a => a.Tags).ToListAsync();
        }

        public async Task<BlogPost?> GetAsync(Guid id)
        {
            return await _bloggieContext.BlogPosts.Include(x => x.Tags).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<BlogPost?> UpdateAsync(BlogPost blogPost)
        {
            var existingBlog = await _bloggieContext.BlogPosts.Include(x => x.Tags)
            .FirstOrDefaultAsync(x => x.Id == blogPost.Id);
            if (existingBlog != null)
            {
                existingBlog.Id = blogPost.Id;
                existingBlog.Author = blogPost.Author;
                existingBlog.ShortDescription = blogPost.ShortDescription;
                existingBlog.Content = blogPost.Content;
                existingBlog.PageTitle = blogPost.PageTitle;
                existingBlog.FeaturedImageUrl = blogPost.FeaturedImageUrl;
                existingBlog.Heading = blogPost.Heading;
                existingBlog.UrlHandle = blogPost.UrlHandle;
                existingBlog.PublishedDate = blogPost.PublishedDate;
                existingBlog.Visible = blogPost.Visible; 
                existingBlog.Tags = blogPost.Tags;
               await _bloggieContext.SaveChangesAsync();
               return existingBlog;

            }
            return null;
        }
    }
}