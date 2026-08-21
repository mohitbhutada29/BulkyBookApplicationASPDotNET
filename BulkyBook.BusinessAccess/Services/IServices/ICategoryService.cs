//(103) The CRUD operations performed in the CategoryController are tightly coupled with the Entity Framework and the database context.
//This makes it difficult to test the controller in isolation, as it requires a database connection and specific data to be
//present in the database. To address this issue, we can introduce a service layer that abstracts the data access logic and
//provides a clean interface for the controller to interact with. This allows us to easily mock the service layer during testing,
//enabling us to test the controller's behavior without relying on a real database.

using BulkyBook.Models;

namespace BulkyBook.BusinessAccess.Services.IServices
{
    public interface ICategoryService
    {
        //(104) Methods will be all the methods that we will use in the CategoryController
        //to perform CRUD operations on the Category model.
        // For eg GetAllCategories, GetCategoryById, CreateCategory, UpdateCategory, DeleteCategory
        // But when we are working with .NET it is recommended to use async Endpoints.
        // We will return a Task here. When we add a task before the returntype it becomes an endpoint
        // that can be called asynchronously. 


        Task<Category?> GetCategoryByIdAsync(int id);

        Task<IEnumerable<Category>> GetAllCategoriesAsync();

        Task<Category> CreateCategoryAsync(Category category);

        Task UpdateCategoryAsync(Category category);

        Task DeleteCategoryAsync(int id);

        // (109) We need to check if the category is unique or not.
        Task<bool> IsCategoryNameUniqueAsync(string name, int? id = null);

    }
}
