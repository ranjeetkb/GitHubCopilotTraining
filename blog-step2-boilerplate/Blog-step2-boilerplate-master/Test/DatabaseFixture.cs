using Blog_Step2.Models;
using Microsoft.EntityFrameworkCore;

namespace Test
{
    public class DatabaseFixture : IDisposable
    {
        public BlogContext context;

        public DatabaseFixture()
        {
            var options = new DbContextOptionsBuilder<BlogContext>()
                .UseInMemoryDatabase(databaseName: "BlogPostDB")
                .Options;

            //Initializing DbContext with InMemory
            context = new BlogContext(options);

            // Insert seed data into the database using one instance of the context
            _ = context.blogpost.Add(new BlogPost { PostId = 1, Title = "Sample Blog Post", Content = "This is a sample blog post content", CreatedAt = DateTime.Now, UpdatedAt=DateTime.Now,PublishedAt = DateTime.Now, Slug = "sample-blog-post" });
            context.SaveChanges();
            _ = context.blogpost.Add(new BlogPost { PostId = 2, Title = "Another Sample Blog Post", Content = "This is a another sample blog post content", CreatedAt = DateTime.Now.AddDays(-2), UpdatedAt=DateTime.Now,PublishedAt = DateTime.Now, Slug = "another-sample-blog-post" });
            context.SaveChanges();
        }
        public void Dispose()
        {
            context = null;
        }
    }
}
