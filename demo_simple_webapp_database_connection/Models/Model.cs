using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace demo_simple_webapp_database_connection.Models
{
    public class WebAppContext : DbContext
    {
        public WebAppContext(DbContextOptions<WebAppContext> options) : base(options)
        { }

        public DbSet<Blog> Blogs { get; set; }

        public DbSet<Post> Posts { get; set; }
    }

    public class Blog
    {
        public int BlogId { get; set; }

        public string Url { get; set; }

        public List<Post> Posts { get; set; }
    }

    public class Post
    {
        public int PostId { get; set; }

        public string Title { get; set; }
        
        public string Content { get; set; }

        public int BlogId { get; set; }
        
        public Blog Blog { get; set; }
    }
}