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
using Microsoft.AspNetCore.Mvc;
using Blog.Controllers;

namespace Commander.ControllerTests
{
    [TestCaseOrderer("Commander.PriorityOrderer", "commander")]
    public class TagControllerTest    
    {
        [Fact,TestPriority(1)]
        public void CreateTag_ShouldReturnOkResult()
        {
            // Arrange
            var tagRepositoryMock = new Mock<TagRepository>();          
            var controller = new TagController(tagRepositoryMock.Object);

            // Act
            var result = controller.CreateTag(1,"Test Tag");

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var tag = Assert.IsType<Tag>(okResult.Value);
            Assert.Equal("Test Tag", tag.Name);
        }

        [Fact,TestPriority(2)]
        public void GetTagById_TagExists_ShouldReturnTag()
        {
            // Arrange
            var tagRepositoryMock = new Mock<TagRepository>();
            var tag = new Tag(1, "Test Tag");          

            var controller = new TagController(tagRepositoryMock.Object);

            // Act
            controller.CreateTag(tag.TagId,tag.Name);
            var result = controller.GetTagById(tag.TagId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedTag = Assert.IsType<Tag>(okResult.Value);
            Assert.Equal(tag.Name, returnedTag.Name);
        }
        [Fact,TestPriority(3)]
        public void GetTagsByName_ShouldReturnTags()
        {
            // Arrange
            var tagName = "Test Tag";
            var tag1 = new Tag(1, tagName);
            var tag2 = new Tag(2, tagName);
            

            var tagRepositoryMock = new Mock<TagRepository>();
            var controller = new TagController(tagRepositoryMock.Object);
            controller.CreateTag(tag1.TagId,tag1.Name);
            controller.CreateTag(tag2.TagId,tag2.Name);
            // Act
            
            var result = controller.GetTagsByName(tagName);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedTags = Assert.IsAssignableFrom<List<Tag>>(okResult.Value);
            Assert.Equal(2, returnedTags.Count);
        }

        [Fact,TestPriority(4)]
        public void UpdateTag_ShouldReturnOkResult()
        {
            // Arrange
            var tagRepositoryMock = new Mock<TagRepository>();           

            var controller = new TagController(tagRepositoryMock.Object);

            // Act
            var result = controller.UpdateTag(1, "Updated Tag");

            // Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact,TestPriority(5)]
        public void DeleteTag_ShouldReturnOkResult()
        {
            // Arrange
            var tagRepositoryMock = new Mock<TagRepository>();            
            var controller = new TagController(tagRepositoryMock.Object);

            // Act
            var result = controller.DeleteTag(1);

            // Assert
            Assert.IsType<OkResult>(result);
        }
    }


}

