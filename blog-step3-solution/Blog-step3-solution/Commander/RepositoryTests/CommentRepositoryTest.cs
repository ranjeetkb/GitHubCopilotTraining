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

namespace Commander.RepositoryTests
{
    [TestCaseOrderer("Commander.PriorityOrderer", "commander")]
    public class CommentRepositoryTest
    {
        [Fact, TestPriority(1)]
        public void CreateComment_ShouldAddCommentToList()
        {
            // Arrange
            var commentRepository = new CommentRepository();
            var newcomment = new Comment(1, "Test Comment 1", DateTime.Now, 1);

            // Act
            var comment = commentRepository.CreateComment(newcomment);
            // Assert
            Assert.NotNull(comment);
            Assert.Equal(newcomment.Content, comment.Content);
            Assert.Equal(newcomment.PostId, comment.PostId);
        }

        [Fact, TestPriority(2)]
        public void GetCommentById_CommentExists_ShouldReturnComment()
        {
            // Arrange
            var commentRepository = new CommentRepository();
            var newcomment = new Comment(1, "Test Comment 1", DateTime.Now, 1);

            var Id = 1;

            // Act
            var comment = commentRepository.CreateComment(newcomment);
            var result = commentRepository.GetCommentById(Id);

            // Assert
            Assert.NotNull(result);
          
        }

        [Fact, TestPriority(3)]
        public void GetCommentsForPost_ShouldReturnCommentsForPost()
        {
            // Arrange
            var commentRepository = new CommentRepository();
            var postId = 1;
            var expectedComments = new List<Comment>
            {
            commentRepository.CreateComment(new Comment(2,"Test Comment 2",DateTime.Now.AddDays(-2),1)),
            commentRepository.CreateComment(new Comment(3, "Test Comment 3", DateTime.Now.AddDays(-1), 1))
            };

            //Act
            var result = commentRepository.GetCommentsForPost(postId);

            //Assert
            Assert.Equal(expectedComments.Count, result.Count);            
        }

        [Fact, TestPriority(4)]
        public void UpdateComment_CommentExists_ShouldUpdateCommentContent()
        {
            //Arrange
            var commentRepository = new CommentRepository();
            var newcomment = new Comment(1, "Test Comment 1", DateTime.Now, 1);

            var newContent = "Updated Test Comment 1";

            // Act
            var comment = commentRepository.CreateComment(newcomment);
            commentRepository.UpdateComment(newcomment.Id, newContent);
            var updatedComment = commentRepository.GetCommentById(newcomment.Id);

            // Assert
            Assert.NotNull(updatedComment);
            Assert.Equal(newContent, updatedComment.Content);
        }

        [Fact, TestPriority(5)]
        public void DeleteComment_CommentExists_ShouldRemoveCommentFromList()
        {
            // Arrange
            var commentRepository = new CommentRepository();
            var newcomment = new Comment(1, "Test Comment 1", DateTime.Now, 1);


            // Act
            commentRepository.DeleteComment(newcomment.Id);
            var result = commentRepository.GetCommentById(newcomment.Id);

            // Assert
            Assert.Null(result);
        }

    }

}