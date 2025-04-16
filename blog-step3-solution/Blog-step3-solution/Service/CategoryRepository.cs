using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomExceptions;
using Entities;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;

namespace Service
{
    public class CategoryRepository : IRepository<Category>
    {
        private readonly BlogDbContext _context;

        public CategoryRepository(BlogDbContext context)
        {
            _context = context;
        }

        public Category Create(Category category)
        {
            // Check if a category with the same id already exists
            if (_context.Categories.Any(ct => ct.Id == category.Id))
            {
                throw new CategoryAlreadyExistsException($"Category with the Id '{category.Id}' already exists.");
            }

            _context.Categories.Add(category);
            _context.SaveChanges();
            return category;
        }

        public Category GetById(int id)
        {
            var category = _context.Categories.Find(id);
            if (category != null)
            {
                return category;
            }
            else
            {
                throw new CategoryNotFoundException($"Category with ID {id} not found.");
            }
        }

        public IEnumerable<Category> GetAll()
        {
            return _context.Categories.ToList();
        }

        public void Update(Category category)
        {
            var categoryfound = GetById(category.Id);
            if (categoryfound != null)
            {
                 categoryfound.Name = category.Name;                
                _context.SaveChanges();
            }
            else
            {
                throw new CategoryNotFoundException($"Category with ID '{category.Id}' not found.");
            }
        }

        public void Delete(int id)
        {
            var category = _context.Categories.FirstOrDefault(c => c.Id == id);
            if (category != null)
            {
                _context.Categories.Remove(category);
                _context.SaveChanges();
            }
            else
            {
                throw new CategoryNotFoundException($"Category with ID '{category.Id}' not found.");
            }
        }
    }
}
