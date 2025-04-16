using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commander.RepositoryTests
{
    [TestCaseOrderer("Commander.PriorityOrderer", "commander")]
    public class TagRepositoryTest
    {
        [Fact, TestPriority(1)]
        public void CreateTag_ShouldAddTagToList()
        {
            // Arrange
            var tagRepository = new TagRepository();

            // Act
            var tag = tagRepository.CreateTag(1,"Test Tag");

            // Assert
            Assert.NotNull(tag);
            Assert.Equal("Test Tag", tag.Name);
        }

        [Fact, TestPriority(2)]
        public void GetTagById_ShouldReturnTag()
        {
            // Arrange
            var tagRepository = new TagRepository();
            var tag = tagRepository.CreateTag(1,"Test Tag");

            // Act
            var result = tagRepository.GetTagById(tag.TagId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Test Tag", result.Name);
        }

        [Fact, TestPriority(3)]
        public void GetTagById_TagNotFound_ShouldReturnNull()
        {
            // Arrange
            var tagRepository = new TagRepository();
            tagRepository.CreateTag(1, "Test Tag");

            // Act
            var result = tagRepository.GetTagById(999); // Non-existent tag ID

            // Assert
            Assert.Null(result);
        }

        [Fact, TestPriority(4)]
        public void GetTagsByName_ShouldReturnTags()
        {
            // Arrange
            var tagRepository = new TagRepository();
            tagRepository.CreateTag(1,"Test Tag 1");
            tagRepository.CreateTag(2,"Test Tag 2");

            // Act
            var result = tagRepository.GetTagsByName("Test Tag 1");

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Count);
            Assert.Equal("Test Tag 1", result[0].Name);
        }

        [Fact, TestPriority(5)]
        public void UpdateTag_ShouldUpdateTag()
        {
            // Arrange
            var tagRepository = new TagRepository();
            var tag = tagRepository.CreateTag(1, "Test Tag");

            // Act
            tagRepository.UpdateTag(tag.TagId, "Updated Tag");

            // Assert
            var updatedTag = tagRepository.GetTagById(tag.TagId);
            Assert.NotNull(updatedTag);
            Assert.Equal("Updated Tag", updatedTag.Name);
        }

        [Fact, TestPriority(6)]
        public void DeleteTag_ShouldRemoveTagFromList()
        {
            // Arrange
            var tagRepository = new TagRepository();
            var tag = tagRepository.CreateTag(1,"Test Tag");

            // Act
            tagRepository.DeleteTag(tag.TagId);

            // Assert
            var deletedTag = tagRepository.GetTagById(tag.TagId);
            Assert.Null(deletedTag);
        }
    }
}
