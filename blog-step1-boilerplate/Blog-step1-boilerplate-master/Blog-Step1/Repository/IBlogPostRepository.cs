using Blog_Step1.Models;
namespace Blog_Step1.Repository
{
    public interface IBlogPostRepository
    {
        // Create a new blog post
        void CreateBlogPost(BlogPost post);

        // Retrieve a blog post by its ID
        BlogPost GetBlogPostById(int postId);             

        // Delete a blog post by its ID
        bool DeleteBlogPost(int postId);

        // Retreive all blog posts
        List<BlogPost> GetBlogPosts();
    }
}
