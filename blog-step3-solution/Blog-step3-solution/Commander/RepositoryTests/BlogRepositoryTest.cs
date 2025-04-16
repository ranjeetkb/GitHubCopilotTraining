using Entities;
using Microsoft.EntityFrameworkCore;
using Moq;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.InMemory;
using CustomExceptions;
using System.Reflection.Metadata;

namespace Commander.RepositoryTests
{
    [TestCaseOrderer("Commander.PriorityOrderer", "commander")]
    public class BlogRepositoryTest
    {
        private DbContextOptions<BlogDbContext> CreateNewContextOptions()
        {
            return new DbContextOptionsBuilder<BlogDbContext>()
                .UseInMemoryDatabase("BlogDatabase")
                .Options;
        }

        [Fact, TestPriority(1)]
        public void Create_ValidBlogPost_ReturnsCreatedPost()
        {
            using (var mockcontext = new BlogDbContext(CreateNewContextOptions()))
            {

                // Arrange              
                var repository = new BlogPostRepository(mockcontext);
                var post = new BlogPost
                {
                    Id = 1,
                    Title = "Test Post 1",
                    Content = "Test Content 1",                    
                };

                // Act
                var createdPost = repository.Create(post);
                mockcontext.SaveChanges();

                // Assert
                Assert.NotNull(createdPost);
                Assert.Equal(post.Title, createdPost.Title);
                Assert.Equal(post.Content, createdPost.Content);
                
            }
        }

        [Fact, TestPriority(2)]
        public void GetById_ExistingId_ReturnsCorrectPost()
        {
            // Arrange
            using (var mockcontext = new BlogDbContext(CreateNewContextOptions()))
            {

                // Arrange              
                var repository = new BlogPostRepository(mockcontext);
                var postid = 1;

                // Act
                var retrievedPost = repository.GetById(postid);

                // Assert
                Assert.NotNull(retrievedPost);
                Assert.Equal("Test Post 1", retrievedPost.Title);
            }
        }

        [Fact, TestPriority(3)]
        public void GetById_ExistingId_ReturnsPostNotFound_ForBlogNotExixts()
        {
            // Arrange
            using (var mockcontext = new BlogDbContext(CreateNewContextOptions()))
            {

                // Arrange              
                var repository = new BlogPostRepository(mockcontext);
                var postid = 100;

                // Act and Asset
                var exception = Assert.Throws<BlogPostNotFoundException>(() => repository.GetById(postid));
                Assert.Equal("Blog post with ID 100 not found.", exception.Message);

            }
        }

        [Fact, TestPriority(4)]
        public void GetAll_ReturnsAllPosts()
        {
            using (var mockcontext = new BlogDbContext(CreateNewContextOptions()))
            {

                // Arrange              
                var repository = new BlogPostRepository(mockcontext);

                var post2 = new BlogPost
                {
                    Id = 2,
                    Title = "Test Post 2",
                    Content = "Test Content 2",
                   
                };
                var post3 = new BlogPost
                {
                    Id = 3,
                    Title = "Test Post 3",
                    Content = "Test Content 3",
                    
                };
                var post4 = new BlogPost
                {
                    Id = 4,
                    Title = "Test Post 4",
                    Content = "Test Content 4",
                    
                };

                repository.Create(post2);
                repository.Create(post3);
                repository.Create(post4);

                mockcontext.SaveChanges();

                // Act
                var count = mockcontext.BlogPosts.Count();
                var allPosts = repository.GetAll();

                // Assert
                Assert.Equal(count, allPosts.Count());
            }
        }

        [Fact, TestPriority(5)]
        public void Update_ExistingPost_ModifiesPost()
        {
            using (var mockcontext = new BlogDbContext(CreateNewContextOptions()))
            {

                // Arrange              
                var repository = new BlogPostRepository(mockcontext);
                var postid = 1;

                var updatedPost = new BlogPost { Id = 1, Title = "Updated Title" };


                // Act
                repository.Update(updatedPost);
                mockcontext.SaveChanges();

                var existingPost = mockcontext.BlogPosts.Find(postid);

                // Assert                
                Assert.Equal(updatedPost.Title, existingPost.Title);
            }
        }

        [Fact, TestPriority(6)]
        public void Delete_ExistingPost_DeletesPost()
        {
            using (var mockcontext = new BlogDbContext(CreateNewContextOptions()))
            {

                // Arrange              
                var repository = new BlogPostRepository(mockcontext);

                var postid = 1;

                // Act
                repository.Delete(postid);
                mockcontext.SaveChanges();

                // Assert
                var deletedBlogPost = mockcontext.BlogPosts.FirstOrDefault(ps => ps.Id == postid);
                Assert.Null(deletedBlogPost);


            }
        }
    }
}
