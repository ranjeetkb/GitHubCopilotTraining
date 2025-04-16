using Commander.TestSetup;
using CommentService.Controllers;
using CommentService.Models;
using CommentService.Repository;
using CommentService.Service;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace ControllerTests
{

    [TestCaseOrderer("TestSetUp.PriorityOrderer", "commander")]
    public class CommentControllerTest:IClassFixture<CommentControllerFixture>
    {
       
        private ICommentRepository commentRepository;
        private ICommentService commentService;
        private IMongoCollection<Comment> commentcollection;
        CommentController controller;
        public CommentControllerTest(CommentControllerFixture fixture)
        {
            commentRepository = new CommentRepository(fixture.commentcontext);
            commentService = new CommentService.Service.CommentService(commentRepository);
            commentcollection = fixture.commentcontext.commentCollection;
            controller = new CommentController(commentService);
        }

        #region CommentController Tests
        [Fact, TestPriority(1)]
        public void CreateComment_ReturnsCreatedAtActionResult_WithNewComment()
        {
            // Arrange        

            var comment=new Comment() { CommentId=101,BlogPostId=101,Content="Comment for blog post 101",CreatedAt=DateTime.Now};

            // Act
            var result = controller.CreateComment(comment) as CreatedAtActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<CreatedAtActionResult>(result);
            var createdcomment = commentcollection.Find(x => x.CommentId == 101).FirstOrDefault();
            Assert.NotNull(createdcomment);

        }

        [Fact, TestPriority(2)]
        public void GetAllComments_ReturnsOkResult_WithListOfComments()
        {

            // Act
            var result = controller.GetAllComments();

            // Assert
            Assert.NotNull(result);
            Assert.IsAssignableFrom<ActionResult<IEnumerable<Comment>>>(result);
            Assert.IsType<ActionResult<IEnumerable<Comment>>>(result);
        }

        [Fact, TestPriority(3)]
        public void GetCommentForValidID_ReturnsOkResult_WithComments()
        {
            //Arrange
            var commentid = 1;

            // Act
            var result = controller.GetCommentById(commentid);

            // Assert
            Assert.NotNull(result);
            Assert.IsAssignableFrom<ActionResult<Comment>>(result);
            Assert.IsType<ActionResult<Comment>>(result);
            var foundcomment = commentcollection.Find(x => x.CommentId == commentid).FirstOrDefault();
            Assert.NotNull(foundcomment);
        }

        [Fact, TestPriority(4)]
        public void GetCommentForValidBlogPostID_ReturnsOkResult_WithComments()
        {
            //Arrange
            var blogpostid = 101;

            // Act
            var result = controller.GetCommentsByBlogPostId(blogpostid);

            // Assert
            Assert.NotNull(result);
            Assert.IsAssignableFrom<ActionResult<IEnumerable<Comment>>>(result);
            Assert.IsType<ActionResult<IEnumerable<Comment>>>(result);
        }

        [Fact, TestPriority(5)]
        public void UpdateComment_WithValidId_ReturnsNoContent()
        {
            //Arrange
            var commentid = 1;
            var commentToUpdate = new Comment() { CommentId = 1, BlogPostId = 1, Content = "Updated Comment for blog post 1", CreatedAt = DateTime.Now };

            // Act
            var result = controller.UpdateComment(commentid, commentToUpdate);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<NoContentResult>(result);

        }

        [Fact, TestPriority(6)]
        public void DeleteComment_WithValidId_ReturnsNoContent()
        {
            //Arrange
            var comemntid = 3;


            // Act
            var result = controller.DeleteComment(comemntid);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<NoContentResult>(result);

        }
        #endregion CommentController Tests
    }
}
