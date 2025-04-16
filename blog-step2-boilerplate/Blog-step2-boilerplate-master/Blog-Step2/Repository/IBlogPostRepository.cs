using Blog_Step2.Models;
namespace Blog_Step2.Repository
{
    public interface IBlogPostRepository
    {
        // Create a new blog post
        int CreateBlogPost(BlogPost post);

        // Retrieve a blog post by its ID
        BlogPost GetBlogPostById(int postId);   
        
        // Check if a blog post exists by its ID
        bool BlogPostExists(int postId);

        //Update a blog post by its ID
        int UpdateBlogPost(BlogPost post);

        // Delete a blog post by its ID
        int DeleteBlogPost(int postId);

        // Retreive all blog posts
        List<BlogPost> GetBlogPosts();
    }
}
