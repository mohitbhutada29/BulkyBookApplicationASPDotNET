//(101) We have moved this from BulkyBookWeb.Models to BulkyBook.Models to make it a class library project.
//    This is done to separate the models from the web application and make it easier to manage the code.

using System.ComponentModel.DataAnnotations;

namespace BulkyBook.Models
{
    // (16) This particular class is a placeholder for the Category model in the BulkyBookWeb application.
    // We will use entity framework to create a database table for categories.
    public class Category
    {
        //(17) //[Key] // This attribute specifies that the CategoryIdentification property is the primary key for the
        //      // Category table in the database.
        //      // Not needed if the property name is Id
        //public int CategoryIdentification { get; set; }

        // (20) We had a db with name CategoryIdentification column
        // We changed it to Id
        // To add this in the db add a new migration:In package manager console: add-migration UpdateIdInCategories
        // And then update-database
        public int Id { get; set; }

        //(57) Data annotations: Required, StringLength, Range, RegularExpression, Compare, EmailAddress, Phone,
        //Url, CreditCard, CustomValidation
        // (58) Add migration AddValidation after adding the data annotations to the model and then
        // update-database to apply the changes to the db
        [Required]
        [StringLength(100)]
        [Display(Name = "Category Name")] // (116) with this annotation, no need to give the names explicitly in the form See 118
        public string Name { get; set; } = string.Empty;

        // (21) How to add a table in SQL server after creating this class
        // 1. Add Entity Framework Core and EF Core Tools NuGet packages to the project.
        // 2. Add a DbSet property for the Category model in the ApplicationDbContext class.
        // 3. Open the Package Manager Console and run the following command to create a migration:
        //    Add-Migration InitialCreate
        // 4. Run the following command to apply the migration and create the database table:
        //   Update-Database
        // 5. The Category table will be created in the database with the specified properties.
        // 6. You can now use the Category model to perform CRUD operations on the Category table in the database.
        // 7. If you make changes to the Category model, you can create a new migration and update the database again.
        // 8. To create a new migration, run the following command in the Package Manager Console:
        //   Add-Migration MigrationName
        // 9. To update the database with the new migration, run the following command:
        //  Update-Database

        //(63) We are getting an error for display Value too. For that we add Range data annotation since it is an integer.
        // It will display the error if the value is not between 0 and 100
        // You can add custom validation error message in the annotation itself.
        // Tihs messages are displayed because of div added in (60)
        [Required]
        [Range(0, 100, ErrorMessage = "Range must be between 0 and 100.")]
        [Display(Name = "Display order")] // (117) with this annotation, no need to give the names explicitly
                                          // in the form see 118 as we have provided asp-for=
        public int DisplayOrder { get; set; }

        //(56) Server side validations: To add the validations we use Data Annotations
    }
}