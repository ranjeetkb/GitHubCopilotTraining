using Microsoft.AspNetCore.Mvc;
using Blog_Step2.Models;
using Blog_Step2.Repository;

namespace Blog_Step1.Controllers
{
    public class BlogPostController : Controller
    {
        /*
          * From the problem statement, we can understand that the application
          * requires us to implement the following functionalities.
          * 
          * 1. Display the list of existing blog posts from the collection. 
          * 2. Add a new blog post.
          * 3. Update an existing blog post.
          * 4. Delete an existing blog post.
        */

        /* 
         * Retrieve the BlogPostRepository object from the dependency Container through 
         * constructor Injection.
        */

        /*
         * Define a handler method to read the existing blog posts by calling the 
         * GetBlogPosts() method of the BlogPostRepository class and pass to view. 
         * It should map to the default URL i.e. "/" 
        */

        /* Define a handler method to read blog post for a specific postid by calling the 
         * GetBlogPostById() method of the BlogPostRepository class
        */

        /*
         * Define a handler method which will read the post data from request parameters and
         * save the post by calling the CreateBlogPost() method of BlogPostRepository class.          
        */

        /* Define a handler method to update an existing post by calling the UpdateBlogPost()
        * method of the BlogPostRepository class
        */

        /* Define a handler method to check the existence of a specific blog by calling the BlogPostExists()
        * method of the BlogPostRepository class
        */

        /* Define a handler method to delete an existing post by calling the DeleteBlogPost()
         * method of the BlogPostRepository class
        */

        
        // GET: BlogPost/Details/{postId}
        
        // GET: BlogPosts/Create
       
        // POST: BlogPosts/Create
       
        // GET: BlogPosts/Delete/{postId}
      
        // GET: BlogPost/Edit/{postId}
       
        // POST: BlogPost/Edit
       
        // GET: BlogPost
        
    }
}
