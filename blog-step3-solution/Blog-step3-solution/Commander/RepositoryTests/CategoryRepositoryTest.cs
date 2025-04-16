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
    public class CategoryRepositoryTest
    {
        private DbContextOptions<BlogDbContext> CreateNewContextOptions()
        {
            return new DbContextOptionsBuilder<BlogDbContext>()
                .UseInMemoryDatabase("BlogDatabase")
                .Options;
        }

        [Fact, TestPriority(1)]
        public void Create_ValidCategory_ReturnsCreatedCategory()
        {
            using (var mockcontext = new BlogDbContext(CreateNewContextOptions()))
            {

                // Arrange              
                var repository = new CategoryRepository(mockcontext);
                var category = new Category
                {
                    Id = 1,
                    Name = "Test Category 1",
                };

                // Act
                var createdCategory = repository.Create(category);
                mockcontext.SaveChanges();

                // Assert
                Assert.NotNull(createdCategory);
                Assert.Equal(category.Name, createdCategory.Name);

            }
        }

        [Fact, TestPriority(2)]
        public void GetById_ExistingId_ReturnsCorrectCategory()
        {
            // Arrange
            using (var mockcontext = new BlogDbContext(CreateNewContextOptions()))
            {

                // Arrange              
                var repository = new CategoryRepository(mockcontext);
                var catid = 1;

                // Act
                var retrievedCategory = repository.GetById(catid);

                // Assert
                Assert.NotNull(retrievedCategory);
                Assert.Equal("Test Category 1", retrievedCategory.Name);
            }
        }

        [Fact, TestPriority(3)]
        public void GetAll_ReturnsAllCategories()
        {
            using (var mockcontext = new BlogDbContext(CreateNewContextOptions()))
            {

                // Arrange              
                var repository = new CategoryRepository(mockcontext);

                var cat2 = new Category
                {
                    Id = 2,
                    Name = "Test Category 2",

                };
                var cat3 = new Category
                {
                    Id = 3,
                    Name = "Test Category 3",

                };

                repository.Create(cat2);
                repository.Create(cat3);


                mockcontext.SaveChanges();

                // Act
                var count = mockcontext.Categories.Count();
                var allCategories = repository.GetAll();

                // Assert
                Assert.Equal(count, allCategories.Count());
            }
        }

        [Fact, TestPriority(4)]
        public void Update_ExistingCategory_ModifiesCategory()
        {
            using (var mockcontext = new BlogDbContext(CreateNewContextOptions()))
            {

                // Arrange              
                var repository = new CategoryRepository(mockcontext);
                var catid = 1;

                var updatedCategory = new Category { Id = 1, Name = "Updated Test Category 1" };


                // Act
                repository.Update(updatedCategory);
                mockcontext.SaveChanges();

                var existingCategory = mockcontext.Categories.Find(catid);

                // Assert                
                Assert.Equal(updatedCategory.Name, existingCategory.Name);
            }
        }

        [Fact, TestPriority(5)]
        public void Delete_ExistingPost_DeletesPost()
        {
            using (var mockcontext = new BlogDbContext(CreateNewContextOptions()))
            {

                // Arrange              
                var repository = new CategoryRepository(mockcontext);

                var catid = 1;

                // Act
                repository.Delete(catid);
                mockcontext.SaveChanges();

                // Assert
                var deletedCategory = mockcontext.Categories.FirstOrDefault(c => c.Id == catid);
                Assert.Null(deletedCategory);


            }
        }
    }
}
