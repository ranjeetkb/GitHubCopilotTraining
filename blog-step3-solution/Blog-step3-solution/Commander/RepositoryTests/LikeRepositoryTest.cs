using Entities;
using Microsoft.EntityFrameworkCore;
using Moq;
using Service;
using System;
using Microsoft.EntityFrameworkCore.InMemory;
using CustomExceptions;

namespace Commander.RepositoryTests
{
    [TestCaseOrderer("Commander.PriorityOrderer", "commander")]
    public class LikeRepositoryTest
    {
        [Fact, TestPriority(1)]
        public void CreateLike_ShouldAddLikeToList()
        {
            // Arrange
            var likeRepository = new LikeRepository();
            var userId = 1;
            var postId = 10;

            // Act
            likeRepository.CreateLike(userId, postId);

            // Assert
            var likes = likeRepository.GetLikesForPost(postId);
            Assert.Single(likes);
            Assert.Equal(userId, likes[0].UserId);
            Assert.Equal(postId, likes[0].PostId);
        }

        [Fact, TestPriority(2)]
        public void GetLikesForPost_ShouldReturnLikesForPost()
        {
            // Arrange
            var likeRepository = new LikeRepository();
            likeRepository.CreateLike(1, 10);
            likeRepository.CreateLike(2, 10);
            likeRepository.CreateLike(3, 20);

            // Act
            var likes = likeRepository.GetLikesForPost(10);

            // Assert
            Assert.Equal(2, likes.Count);
            Assert.Equal(1, likes[0].UserId);
            Assert.Equal(2, likes[1].UserId);
        }

        [Fact, TestPriority(3)]
        public void GetLikesByUser_ShouldReturnLikesByUser()
        {
            // Arrange
            var likeRepository = new LikeRepository();
            likeRepository.CreateLike(1, 10);
            likeRepository.CreateLike(1, 20);
            likeRepository.CreateLike(2, 10);

            // Act
            var likes = likeRepository.GetLikesByUser(1);

            // Assert
            Assert.Equal(2, likes.Count);
            Assert.Equal(10, likes[0].PostId);
            Assert.Equal(20, likes[1].PostId);
        }

        [Fact, TestPriority(4)]
        public void DeleteLike_ShouldRemoveLikeFromList()
        {
            // Arrange
            var likeRepository = new LikeRepository();
            likeRepository.CreateLike(1, 10);
            likeRepository.CreateLike(2, 20);

            // Act
            likeRepository.DeleteLike(1, 10);

            // Assert
            var likes = likeRepository.GetLikesForPost(10);
            Assert.Empty(likes);
        }

    }
}