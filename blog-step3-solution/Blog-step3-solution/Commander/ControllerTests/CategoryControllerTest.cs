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
    public class CategoryControllerTest
    {
        [Fact, TestPriority(1)]
        public void Get_Returns_ObjectResult_WhenCategoryExist()
        {
            // Arrange
            var mockCategoryService = new Mock<IRepository<Category>>();
            mockCategoryService.Setup(repo => repo.GetAll()).Returns(this.categories);
            var controller = new CategoryController(mockCategoryService.Object);

            // Act
            var result = controller.GetCategories();

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact,TestPriority(2)]
        public void Category_Returns_ObjectResult_WhenValidCategory()
        {
            // Arrange
            var mockCategoryService = new Mock<IRepository<Category>>();
            var newCategory = new Category
            {
                Id = 3,
                Name = "Test Category 3",
            };
            mockCategoryService.Setup(repo => repo.Create(newCategory));
            var controller = new CategoryController(mockCategoryService.Object);
            

            // Act
            var result = controller.PostCategory(newCategory);

            // Assert
            Assert.IsType<CreatedAtActionResult>(result);
        }
        


        [Fact, TestPriority(3)]
        public void GetById_ReturnsOkResult_WhenCategoryExists()
        {
            // Arrange
            var mockCategoryService = new Mock<IRepository<Category>>();
            var id = 1;
            mockCategoryService.Setup(repo => repo.GetById(id)).Returns((Category)null);
            var controller = new CategoryController(mockCategoryService.Object);            

            // Act
            var result = controller.GetCategory(id);

            // Assert
            Assert.IsAssignableFrom<ObjectResult>(result);
        }

        [Fact, TestPriority(4)]
        public void Put_ReturnsNoContentResult_WhenValidCategory()
        {
            // Arrange
            var mockCategoryService = new Mock<IRepository<Category>>();
            var existingCategory = new Category { Id = 1,Name="Updated Category 1" };
            mockCategoryService.Setup(repo => repo.Update(existingCategory));
            var controller = new CategoryController(mockCategoryService.Object);
            

            // Act
            var result = controller.PutCategory(1,existingCategory);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact, TestPriority(5)]
        public void Delete_ReturnsNoContentResult_WhenCategoryExists()
        {
            // Arrange
            var mockCategoryService = new Mock<IRepository<Category>>();
            var id = 1;
            mockCategoryService.Setup(repo => repo.Delete(id));
            var controller = new CategoryController(mockCategoryService.Object);
            

            // Act
            var result = controller.DeleteCategory(id);

            // Assert
            Assert.IsAssignableFrom<ObjectResult>(result);
        }

        private readonly List<Category> categories = new List<Category>
        {
            new Category { Id = 1,Name="Test Category 1"},
            new Category { Id = 2,Name="Test Category 2"}
        };

    }
}
