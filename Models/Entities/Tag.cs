using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloggieMvc.Models.Entities
{
    public class Tag
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public ICollection<BlogPost> BlogPosts { get; set; }
    }
}