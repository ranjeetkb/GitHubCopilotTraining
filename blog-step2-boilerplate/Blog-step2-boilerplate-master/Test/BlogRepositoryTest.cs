using Blog_Step2.Models;
using Blog_Step2.Repository;

namespace Test
{
    [TestCaseOrderer("Test.PriorityOrderer", "test")]
    public class BlogRepositoryTest:IClassFixture<DatabaseFixture>
    {
        private readonly BlogPostRepository blogpostRepository;

        public BlogRepositoryTest(DatabaseFixture fixture)
        {
            blogpostRepository = new BlogPostRepository(fixture.context);
        }

        [Fact, TestPriority(0)]
        public void GetBlogPostsShouldReturnList()
        {
            var posts = blogpostRepository.GetBlogPosts();
            Assert.Equal(2, posts.Count);
        }
        [Fact, TestPriority(1)]
        public void AddBlogPostShouldSuccess()
        {
            var post = new BlogPost { PostId = 3, Title = "Sample Blog Post 3", Content = "This is a sample blog post content 3", CreatedAt = DateTime.Now.AddDays(-2), UpdatedAt = DateTime.Now.AddDays(-1), PublishedAt = DateTime.Now, Slug = "sample-blog-post-3" };
            int result= blogpostRepository.CreateBlogPost(post);
            Assert.NotEqual(0, result);
            Assert.True(blogpostRepository.BlogPostExists(post.PostId));
            Assert.Equal(3, post.PostId);
        }

        [Fact, TestPriority(2)]
        public void DeleteBlogPostShouldSuccess()
        {
            int id = 2;
            int result= blogpostRepository.DeleteBlogPost(id);
            Assert.NotEqual(0, result);
            Assert.False(blogpostRepository.BlogPostExists(id));
        }
       
        [Fact, TestPriority(3)]
        public void GetBlogPostByIdShouldReturnABlogPost()
        {
            int id = 3;
            var result = blogpostRepository.GetBlogPostById(id);
            Assert.IsAssignableFrom<BlogPost>(result);
            Assert.Equal("Sample Blog Post 3", result.Title);
        }

        [Fact, TestPriority(4)]
        public void UpdateBlogPostShouldSuccess()
        {
            var blog= blogpostRepository.GetBlogPostById(3);
            blog.Title = "Tech Blog Sample";
            blog.Content = "This is tech blog sample";

            var result = blogpostRepository.UpdateBlogPost(blog);
            Assert.NotEqual(0, result);

            var updatedBlog= blogpostRepository.GetBlogPostById(blog.PostId);
            Assert.Equal("Tech Blog Sample", updatedBlog.Title);
            Assert.Equal("This is tech blog sample", updatedBlog.Content);
        }

        [Fact, TestPriority(5)]
        public void ExistsBlogPostShouldSuccess()
        {
            int id = 3;

            var result = blogpostRepository.BlogPostExists(id);
            Assert.Equal(true, result);

            
        }
    }
}
