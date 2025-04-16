using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Entities;
using CustomExceptions;


namespace Service
{
    //BlogPostRepository class should implement the IRepository<BlogPost> interface methods.
    public class BlogPostRepository : IRepository<BlogPost>
    {
        // Create BlogDbContext object
        private readonly BlogDbContext _context;

        //Create BlogPostRepository constructor which takes BlogDbContext object as parameter
        //Use DI to inject the service 
        public BlogPostRepository(BlogDbContext context)
        {
            _context = context;
        }
        
        public BlogPost Create(BlogPost post)
        {
            // Check if a blog post with the same Id already exists
            if (_context.BlogPosts.Any(bp => bp.Id == post.Id))
            {
                throw new BlogPostAlreadyExistsException($"A blog post with the Id '{post.Id}' already exists.");
            }

            var blogPost = new BlogPost
            {
                Id=post.Id,
                Title = post.Title,
                Content = post.Content,
                CreatedAt = DateTime.Now,               
            };
            _context.BlogPosts.Add(blogPost);
            _context.SaveChanges();
            return blogPost;
        }

        public BlogPost GetById(int id)
        {
            var blogPost = _context.BlogPosts.Find(id);
            if(blogPost != null) {               
                return blogPost;
            }
            else
            {
                throw new BlogPostNotFoundException($"Blog post with ID {id} not found.");
            }          
           
        }

        public IEnumerable<BlogPost> GetAll()
        {
            return _context.BlogPosts.ToList();
        }

        public void Update(BlogPost post)
        {
            var blogPost = GetById(post.Id);
            if (blogPost != null)
            {
                blogPost.Title = post.Title;
                blogPost.Content = post.Content;                               
                _context.SaveChanges();
            }
            else
            {
                throw new BlogPostNotFoundException($"Blog post with ID '{post.Id}' not found.");
            }
        }

        public void Delete(int id)
        {
            var blogPost = GetById(id);
            if (blogPost != null)
            {
                _context.BlogPosts.Remove(blogPost);
                _context.SaveChanges();
            }
            else
            {
                throw new BlogPostNotFoundException($"Blog post with ID '{id}' not found.");
            }
        }

       
    }
}
