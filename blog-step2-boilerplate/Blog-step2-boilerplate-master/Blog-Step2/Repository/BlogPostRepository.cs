 using Blog_Step2.Models;
using System.Collections.Generic;
namespace Blog_Step2.Repository
{
     /*
     This class contains the code for data storage interactions and methods 
     of this class will be used by other parts of the applications such
     as Controllers and Test Cases
     */
    public class BlogPostRepository : IBlogPostRepository
    {
        /* Declare a readonly variable of the context */        
        
        

        /* Use the DI to inject the context */
       
        
        /* This method should check if the blog post exists for the given id 
         * Can be used as a helper method for controller
        */
        public bool BlogPostExists(int postId)
        {
            return false;
        }

        /* This method must save the new blog post to the database (blogpost) table */
        public int CreateBlogPost(BlogPost post)
        {
            return 0;
        }

        /* This method should delete the blog post for the given id from the 
         * database (blogpost) table. 
        */
        public int DeleteBlogPost(int postId)
        {
            return 0;
        }

        /* This method should retrieve specific blog post from the database(blogpost) table. */
        public BlogPost GetBlogPostById(int postId)
        {
            return null;
        }


        /* This method returns all the blogs. */
        public List<BlogPost> GetBlogPosts()
        {
            return null;
        }

        /* This method should update the blog post for the given id in the 
         * database (blogpost) table. 
        */
        public int UpdateBlogPost(BlogPost post)
        {
           return 0;
        }
    }
}
