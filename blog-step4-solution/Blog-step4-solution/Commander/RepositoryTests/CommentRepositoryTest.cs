using Commander.TestSetup;
using CommentService.Exceptions;
using CommentService.Models;
using CommentService.Repository;
using MongoDB.Driver;


namespace RepositoryTests
{
    [TestCaseOrderer("TestSetup.PriorityOrderer", "commander")]
    public class CommentRepositoryTest:IClassFixture<CommentDbFixture>
    {

        private ICommentRepository commentRepository;
        private IMongoCollection<Comment> commentcollection;

        public CommentRepositoryTest(CommentDbFixture commentDbFixture)
        {
            commentRepository = new CommentRepository(commentDbFixture.context);
            commentcollection = commentDbFixture.context.commentCollection;
        }

        #region Positive CommentRepositoryTests      
        [Fact, TestPriority(1)]
        public void CreateComment_ShouldCreateNewComment()
        {
            // Arrange           

            var comment4 = new Comment { CommentId = 4, BlogPostId = 3, Content = "First comment for blog post 3", CreatedAt = DateTime.Now };

            // Act 
            var result=commentRepository.CreateComment(comment4);

            //Assert
            Assert.NotNull(result);
            Assert.IsAssignableFrom<Comment>(result);
            Assert.Equal(4, result.CommentId);
            Assert.Equal("First comment for blog post 3", result.Content);
            
        }

        [Fact, TestPriority(2)]
        public void GetAllComments_ReturnsAllComments()
        {
            // Act
            var comments = commentRepository.GetAllComments();

            // Assert
            Assert.NotNull(comments);
            Assert.Equal(commentcollection.CountDocuments(FilterDefinition<Comment>.Empty), comments.Count());

        }
        [Fact, TestPriority(3)]
        public void GetCommentById_ReturnsCommentById()
        {
            int id = 3;
            // Act
            var comments = commentRepository.GetCommentById(id);

            // Assert
            Assert.NotNull(comments);
            Assert.IsAssignableFrom<Comment>(comments);
            Assert.Contains("First comment for blog post 3", comments.Content);

        }
        [Fact, TestPriority(4)]
        public void GetAllCommentsByBlogPostId_ReturnsAllCommentsByBlogPostId()
        {
            int id = 1;
            // Act
            var blogcomments = commentRepository.GetCommentsByBlogPostId(id);

            // Assert
            Assert.NotNull(blogcomments);
            var findcomments = commentcollection.Find(Builders<Comment>.Filter.Eq("BlogPostId", id));
            Assert.Equal(findcomments.CountDocuments(), blogcomments.Count());

        }
        [Fact, TestPriority(5)]
        public void UpdateComment_ReturnsTrueOnSuccess()
        {
            //Arrange
            int Id = 1;
            var commentToUpdate = new Comment() { CommentId = 1, Content = "Updated comment for blog post 1", BlogPostId = 1, CreatedAt = DateTime.Now };

            // Act
            var result = commentRepository.UpdateComment(Id, commentToUpdate);

            // Assert
            Assert.True(result);
            var updatedcomment = commentcollection.Find(c => c.CommentId == Id).FirstOrDefault();
            Assert.Contains("Updated comment for blog post 1", updatedcomment.Content);

        }
        [Fact, TestPriority(6)]
        public void DeleteComment_ReturnsTrueOnSuccess()
        {
            //Arrange
            int Id = 2;
            
            // Act
            var result = commentRepository.DeleteComment(Id);    

            // Assert
            Assert.True(result);
            var deletedcomment = commentcollection.Find(c=>c.CommentId == Id).FirstOrDefault();
            Assert.Null(deletedcomment);
        }
        #endregion Positive CommentRepositoryTests

        #region Negative CommentRepositoryTests 
        [Fact, TestPriority(7)]

        public void CreateCommentWithExistingId_ShouldThrowException()
        {
            // Arrange         
            var newcomment = new Comment { CommentId = 1, BlogPostId = 1, Content = "New comment for blog post 1", CreatedAt = DateTime.Now };

            // Act && Assert            
            Assert.Throws<CommentAlreadyExistsException>(() =>
            {
                commentRepository.CreateComment(newcomment);
            });

        }
        [Fact, TestPriority(8)]
        public void GetCommentByInvalidId_ReturnsException()
        {
            // Arrange
            var commentId = 100;

            // Act && Assert
            Assert.Throws<CommentNotFoundException>(() =>
            {
                commentRepository.GetCommentById(commentId);
            });
        }
       
        #endregion Negative CommentRepositoryTests

    }
}
