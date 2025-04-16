using BlogPostService.Models;
using Commander.TestSetup;
using CommentService.Models;
using CommentService.Repository;
using CommentService.Service;
using MongoDB.Bson;
using MongoDB.Driver;
using Moq;



namespace ServiceTests
{
    [TestCaseOrderer("TestSetup.PriorityOrderer", "commander")]
    public class CommentServiceTest
    {
       
        private Mock<ICommentRepository> commentRepositoryMock;        
        private ICommentService commentService;       
        

        public CommentServiceTest()
        {
            commentRepositoryMock = new Mock<ICommentRepository>();
            commentService =new CommentService.Service.CommentService(commentRepositoryMock.Object);            

        }

        #region CommentServiceTests    

        [Fact,TestPriority(1)]      
        public void CreateComment_ShouldCreateNewComment()
        {

            // Arrange         
            var comment = new Comment { CommentId = 1, BlogPostId = 1, Content = "First comment for blog post 1", CreatedAt = DateTime.Now };
            commentRepositoryMock.Setup(repo => repo.CreateComment(comment)).Returns(comment);

            // Act
            var result = commentService.CreateComment(comment);

            // Assert
            Assert.NotNull(result);
            Assert.IsAssignableFrom<Comment>(result);
            commentRepositoryMock.Verify(repo => repo.CreateComment(comment), Times.Once);
        }

        [Fact, TestPriority(2)]
        public void GetAllComments_ReturnsAllComments()
        {
            // Arrange

            var expectedComments = new List<Comment>
            {
            new Comment { CommentId = 1, BlogPostId = 1, Content = "First comment for blog post 1", CreatedAt = DateTime.Now },
            new Comment { CommentId = 2, BlogPostId = 1, Content = "Second comment for blog post 1", CreatedAt = DateTime.Now },
            };
            commentRepositoryMock.Setup(repo => repo.GetAllComments()).Returns(expectedComments);

            // Act
            var result = commentService.GetAllComments();

            // Assert
            Assert.IsAssignableFrom<List<Comment>>(result);
            Assert.Equal(expectedComments, result);
            Assert.Equal(2, expectedComments.Count);

        }

        [Fact, TestPriority(3)]
        public void GetAllCommentsByBlogPostId_ReturnsAllCommentsByBlogPostId()
        {
            // Arrange
            int id = 1;
            var expectedComments = new List<Comment>()
            {
                 new Comment { CommentId = 1, BlogPostId = 1, Content = "First comment for blog post 1", CreatedAt = DateTime.Now },
                 new Comment { CommentId = 2, BlogPostId = 1, Content = "Second comment for blog post 1", CreatedAt = DateTime.Now },
            };

            commentRepositoryMock.Setup(repo => repo.GetCommentsByBlogPostId(id)).Returns(expectedComments);


            // Act
            var result = commentService.GetCommentsByBlogPostId(id);

            // Assert
            Assert.IsAssignableFrom<IEnumerable<Comment>>(result);
            Assert.Equal(expectedComments, result);
            Assert.Equal(2,result.Count());
        }

        [Fact, TestPriority(4)]
        public void GetCommentById_ReturnsCommentById()
        {
            // Arrange
            int id = 1;
            var expectedComment = new Comment { CommentId = 1, BlogPostId = 1, Content = "First comment for blog post 1", CreatedAt = DateTime.Now.AddDays(-1) };

            commentRepositoryMock.Setup(repo => repo.GetCommentById(id)).Returns(expectedComment);


            // Act
            var result = commentService.GetCommentById(id);

            // Assert
            Assert.IsAssignableFrom<Comment>(result);
            Assert.Equal(expectedComment, result);
            Assert.Equal("First comment for blog post 1", result.Content);
        }

        [Fact, TestPriority(5)]
        public void UpdateComment_ReturnsTrueOnSuccess()
        {
            // Arrange
            int id = 1;
            var updatedcomment = new Comment { CommentId = 1, BlogPostId = 1, Content = "Updated first comment for blog post 1", CreatedAt = DateTime.Now };

            commentRepositoryMock.Setup(repo => repo.UpdateComment(id, updatedcomment)).Returns(true);

            // Act
            var result = commentService.UpdateComment(id, updatedcomment);

            // Assert
            Assert.True(result);
            commentRepositoryMock.Verify(repo => repo.UpdateComment(id, updatedcomment), Times.Once);
        }

        [Fact, TestPriority(6)]
        public void DeleteComment_ReturnsTrueOnSuccess()
        {
            // Arrange
            int id = 1;
            commentRepositoryMock.Setup(repo => repo.DeleteComment(id)).Returns(true);

            // Act
            var result = commentService.DeleteComment(id);

            // Assert
            Assert.True(result);
            commentRepositoryMock.Verify(repo => repo.DeleteComment(id), Times.Once);
        }
        #endregion CommentServiceTests

    }
}
