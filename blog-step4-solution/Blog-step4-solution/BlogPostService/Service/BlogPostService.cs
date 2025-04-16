using BlogPostService.Models;
using BlogPostService.Repository;

namespace BlogPostService.Service
{
    public class BlogPostService:IBlogPostService
    {
        private readonly IBlogPostRepository _blogPostRepository;

        public BlogPostService(IBlogPostRepository blogPostRepository)
        {
            _blogPostRepository = blogPostRepository;
        }

        public IEnumerable<BlogPost> GetAllBlogPosts()
        {
            return  _blogPostRepository.GetAllBlogPosts();
        }

        public BlogPost GetBlogPostById(int id)
        {
            return _blogPostRepository.GetBlogPostById(id);
        }
              

        public BlogPost CreateBlogPost(BlogPost blogPost)
        {
            _blogPostRepository.CreateBlogPost(blogPost);
            return blogPost;
        }

        public bool UpdateBlogPost(int id, BlogPost blogPost)
        {
            return  _blogPostRepository.UpdateBlogPost(id, blogPost);
        }

        public bool DeleteBlogPost(int id)
        {
            return _blogPostRepository.DeleteBlogPost(id);
        }
    }
}
