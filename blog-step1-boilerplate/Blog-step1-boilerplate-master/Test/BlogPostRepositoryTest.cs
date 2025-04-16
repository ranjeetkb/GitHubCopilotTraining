using Blog_Step1.Models;
using Blog_Step1.Repository;


namespace Test
{
    public class BlogPostRepositoryTest
    {
        private readonly BlogPostRepository blogpostRepository;

        public BlogPostRepositoryTest()
        {
            blogpostRepository = new BlogPostRepository();
        }

        [Fact]
        public void AddBlogPostShouldSuccess()
        {
            BlogPost blog = new BlogPost { PostId = 1, Title = "Sample Blog Post", Content = "This is a sample blog post content", CreatedAt = DateTime.Now, PublishedAt = DateTime.Now, Slug = "sample-blog-post" };
            blogpostRepository.CreateBlogPost(blog);            
            Assert.NotNull(blog);
        }
       
        [Fact]
        public void DeleteBlogPostShouldSuccess()
        {
            BlogPost blog = new BlogPost { PostId = 2, Title = "Another Sample Blog Post", Content = "This is a another sample blog post content", CreatedAt = DateTime.Now.AddDays(-2), PublishedAt = DateTime.Now, Slug = "another-sample-blog-post" };
            blogpostRepository.CreateBlogPost(blog);
            Assert.NotNull(blog);            

            bool res=blogpostRepository.DeleteBlogPost(blog.PostId);
            Assert.True(res);
        }

        [Fact]
        public void GetBlogPostShouldReturnList()
        {
            BlogPost post1 = new BlogPost { PostId = 1, Title = "Sample Blog Post", Content = "This is a sample blog post content", CreatedAt = DateTime.Now, PublishedAt = DateTime.Now, Slug = "sample-blog-post" };
            blogpostRepository.CreateBlogPost(post1);

            BlogPost post2 = new BlogPost { PostId = 2, Title = "Another Sample Blog Post", Content = "This is a another sample blog post content", CreatedAt = DateTime.Now.AddDays(-2), PublishedAt = DateTime.Now, Slug = "another-sample-blog-post" };
            blogpostRepository.CreateBlogPost(post2);


            var posts = blogpostRepository.GetBlogPosts();
            Assert.Equal(2, posts.Count);
        }

        [Fact]
        public void GetBlogPostByIDShouldReturnBlogPost()
        {
            BlogPost post3 = new BlogPost { PostId = 3, Title = "Another Sample Blog Post", Content = "This is a another sample blog post content", CreatedAt = DateTime.Now.AddDays(-1), PublishedAt = DateTime.Now, Slug = "another-sample-blog-post" };
            blogpostRepository.CreateBlogPost(post3);

            var posts = blogpostRepository.GetBlogPostById(post3.PostId);
            Assert.NotNull(posts);
        }
    }
}
