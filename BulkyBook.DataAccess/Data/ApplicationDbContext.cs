//(102) We have moved this from BulkyBookWeb.Data to BulkyBook.Data to make it a class library project.
// And adding relevant references to the project.
// This is done to separate the data access layer from the web application and make it easier to manage the code.
//After this we move the Migrations folder to the BulkyBook.DataAccess project and add the necessary references to it.


using BulkyBook.Models;
using Microsoft.EntityFrameworkCore;

namespace BulkyBook.DataAccess.Data
{
    /// <summary>
    /// (18) This class represents the application's database context, which is responsible for managing the connection
    /// to the database and providing access to the application's data models. Acts as a bridge between the C# code and
    /// the database
    /// </summary>
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // (19) Whenever we have to create a table, we will create a DbSet property for that table in this class.
        //DbSet represents a table in the database and provides methods for querying and manipulating data in that table.
        // The table name will be the same as the DbSet property name, in this case, "Categories".
        public DbSet<Category> Categories { get; set; }

        // (22) Helps us to seed in the default data in the database.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Action", DisplayOrder = 1 },
                new Category { Id = 2, Name = "SciFi", DisplayOrder = 2 },
                new Category { Id = 3, Name = "History", DisplayOrder = 3 }
                );

            // (23) Again to add this in the dn
            //1. Add migration: add-migration SeedCategoryTable
            //2. update-database


            //(26) Adding DisplayOrder to the Category model and seeding data for it.
            //We need to create a new migration and update the database to apply these changes.
            //1. Add migration: add-migration AddDisplayOrderToSeedData
            //2. update-database
        }
    }
}