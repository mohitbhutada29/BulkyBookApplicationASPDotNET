// (105) Implementing ICategoryService interface in CategoryService class

using BulkyBook.BusinessAccess.Services.IServices;
using BulkyBook.DataAccess.Data;
using BulkyBook.Models;
using Microsoft.EntityFrameworkCore;

namespace BulkyBook.BusinessAccess.Services
{
    public class CategoryService : ICategoryService
    {
        private ApplicationDbContext _context;

        // (106) We would use this CategoryService in the Program.cs file to register it with the dependency injection container,
        // so that it can be injected into controllers or other services that require it.
        // We then implement the methods defined in the ICategoryService interface to perform CRUD operations on the Category model.
        // Read and understand this complete class.

        public CategoryService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<Category?> GetCategoryByIdAsync(int id)
        {
            return await _context.Categories.FindAsync(id);
        }

        public async Task<Category> CreateCategoryAsync(Category category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task DeleteCategoryAsync(int id)
        {
            var category = _context.Categories.Find(id);
            if (category == null)
            {
                throw new KeyNotFoundException($"Category id {id} not found");
            }
            _context.Remove(category);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCategoryAsync(Category category)
        {
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
        }

        //(110)
        public async Task<bool> IsCategoryNameUniqueAsync(string name, int? id)
        {
            if (id.HasValue)
            {
                return !await _context.Categories.AnyAsync(x => x.Name.ToLower() == name.ToLower() && x.Id != id.Value);
            }
            else
            {
                return !await _context.Categories.AnyAsync(x => x.Name.ToLower() == name.ToLower());
            }
        }
    }
}