using Blog_Step1.Models;
using System.Collections.Generic;
namespace Blog_Step1.Repository
{
     /*
     This class contains the code for data storage interactions and methods 
     of this class will be used by other parts of the applications such
     as Controllers and Test Cases
     */
    public class BlogPostRepository : IBlogPostRepository
    {
        /* Declare a variable of List type to store all the blog posts. */        
        
        
        public BlogPostRepository() {

            //Initialize the list of blog posts           
            
        }

        /* This method should assign a unique ID to the blog post
        and add the blog post to the list */
        public void CreateBlogPost(BlogPost post)
        {         

            // Add the blog post to the list        
            
        }

        /* This method should delete the blog post for the given id */
        public bool DeleteBlogPost(int postId)
        {
            return false;
        }

        /* This method should find the blog post with the given id. */
        public BlogPost GetBlogPostById(int postId)
        {
            return null;
        }

        
        /* This method returns all the blogs. */
        public List<BlogPost> GetBlogPosts()
        {
            return null;
        }
    }
}
