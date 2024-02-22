using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BloggieMvc.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace BloggieMvc.Context
{
    public class BloggieContext : DbContext
    {
        public BloggieContext(DbContextOptions options) : base(options)
        {
            
        }
        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<Tag> Tags { get; set; }
    }
}