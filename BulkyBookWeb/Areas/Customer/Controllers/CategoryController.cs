//using BulkyBook.DataAccess.Data;
//using BulkyBook.Models;
//using Microsoft.AspNetCore.Mvc;

//namespace BulkyBookWeb.Controllers
//{
//    // (24)
//    public class CategoryController : Controller
//    {
//        private ApplicationDbContext _context;

//        //(28) This ApplicationDbContext dependency is injected in the Program.cs
//        // using builder.Services.AddDbContext<ApplicationDbContext>
//        // Here we will use this db context to fetch data from db
//        public CategoryController(ApplicationDbContext context)
//        {
//            _context = context;
//        }

//        public IActionResult Index()
//        {
//            // (29) This retrieves all the categories from the table using the db context
//            // This is the power of Entity Framework core
//            var categories = _context.Categories.ToList();

//            // (30) To pass on this categories retrieved from the db,
//            // we pass the model to the view and then we can display it in the view.
//            // Right click on the Index and select "Go To View" and it will take you to the correct view.
//            return View(categories);
//        }

//        //(44) Adding an Action method to create the categories, this will return TagHelperServicesExtensions CreateCategory View
//        public IActionResult Create()
//        {
//            return View();
//        }

//        // (55) When you are on a hacky website which calls this post method it may corrupt our db
//        // to prevent this we have a ValidateAntiForgeryToken. What happends here is, the post method from the Create.cshtml
//        // sends a secret code anti forgery token which is first validated here. This means that only the request that are sent by the Create.cshtml of
//        // this porject will be accepted by the controller. This is just an overview of what this token does.
//        // Thus whenever you post anything from the .cshtml view, make sure you have ValidateAntiForgeryToken in the post end point
//        [ValidateAntiForgeryToken]
//        //(51) WE will create a post method that is hit when we submit the from from the (50) in Create.cshtml
//        // This can be done by giving the ActionName("Create") so that when you POST anything in Create view,
//        // that Post method calls this provided you have HttpPost.
//        // Thus if anything is posted on the Create.cshtml view, it will call this method and
//        // we can handle the data posted from the form in this method.
//        [HttpPost]
//        [ActionName("Create")]
//        public IActionResult CreatePOST(Category category)
//        {
//            // (59) To check if the validation is working, we can check if the ModelState is valid or not.
//            // If it is not valid, we will return the same view with the model so that the user can correct the errors.
//            // If the Name property is not inputted, we get the error "Name field is required".
//            // However error message is not seen here.
//            // In order to see the error message go to (60)

//            //(61) If we select asp-validation-for in (60) as model only
//            if (!(string.IsNullOrEmpty(category.Name)) && _context.Categories.Any(x => x.Name.ToLower() == category.Name.ToLower()))
//            {
//                ModelState.AddModelError("", "Category name already exists");
//            }

//            if (ModelState.IsValid)
//            {
//                //(52) When you submit the form on the Create.cshtml this method will be called. Now you want to retrieve the data from the view.
//                //    for that in this method we include the type of the parameter model sent from the view.
//                //    in Create.cshtml the model is CategoryController, that is what will be received here from the View.
//                _context.Categories.Add(category);

//                // (53) Here the category is just added to the db context and not to the table.
//                // We just tell EF core to remember that we need to add this category to the table
//                // To actually add it in the db, we need to call _context.SaveChanges method
//                _context.SaveChanges();

//                //(83)
//                //TempData is temporary data that is stored in the session and is available for the next request.
//                //It is used to pass data from one action to another. It is a dictionary object that can store key-value pairs.
//                //The data stored in TempData is available for the next one request only and is cleared after that.
//                //It is useful for passing data between actions when redirecting.
//                TempData["success"] = "Category created successfully";

//                // (54) After doing the changes I would like to go back the the Index.cshtml view.
//                // We here have RedirectToAction. Since we are in the same controller we need to just add the name of the action method
//                // we want to redirect to. If we were in a different controller, we would have to add the name of the controller as well.
//                // If we use return View("Index") method here it wont work because the Index.cshtml view
//                // expects a model to be passed to it. But we are not passing any model here.
//                return RedirectToAction("Index");
//            }

//            //(59)
//            return View();

//            // (62) Thus if data annotations errors are there like Name field is required,
//            // they can be displayed on the Create.cshtml view by using asp-validation-for tag helper set to "All".
//            // If it is set to "ModelOnly" then only the model errors will be displayed and not the data annotations errors.
//        }

//        // (69) Adding Update and Delete functionality
//        // WE need to check which id we are updating (read complete method here)
//        public IActionResult Update(int? id)
//        {
//            if (id == null || id == 0)
//            {
//                return NotFound(); // this is 404 not found error
//            }

//            var category = _context.Categories.Find(id); // finds the id in the primary table
//            if (category == null)
//            {
//                return NotFound();
//            }
//            return View(category); // We will be creating a Update view in (70)

//            //(71) We have not passed in the parameter from the page, will do that in (71)
//        }

//        //(73) Creating this UpdatePOST method for form posted in Update.cshtml.
//        //Validations remain same.
//        [ValidateAntiForgeryToken]
//        [HttpPost]
//        [ActionName("Update")]
//        public IActionResult UpdatePOST(Category category)
//        {
//            if (!(string.IsNullOrEmpty(category.Name)) &&
//                _context.Categories.Any(x => x.Name.ToLower() == category.Name.ToLower() &&
//                x.Id != category.Id))
//            {
//                ModelState.AddModelError("", "Category name already exists");
//            }
//            if (ModelState.IsValid)
//            {
//                _context.Categories.Update(category);
//                // (74) Entity Framework is very powerful. We do not have to write any update statement in the powershell to update the db.
//                // The update statement takes care of it.
//                _context.SaveChanges();

//                //(84) Once the update is succesful, we can show a success message to the user. This is done using TempData.
//                TempData["success"] = "Category updated successfully";

//                return RedirectToAction("Index");
//            }
//            return View();
//        }

//        // (76) Creating an endpoint for delete
//        public IActionResult Delete(int? id)
//        {
//            if (id == null || id == 0)
//            {
//                return NotFound();
//            }

//            var category = _context.Categories.Find(id);
//            if (category == null)
//            {
//                return NotFound();
//            }
//            return View(category); // We will be creating a Update view in (78)
//        }

//        //(77) Creating this UpdateDelete method
//        //Validations remain same.
//        [ValidateAntiForgeryToken]
//        [HttpPost]
//        [ActionName("Delete")]
//        public IActionResult DeletePOST(int? id)
//        {
//            var category = _context.Categories.Find(id);
//            if (category == null)
//            {
//                return NotFound();
//            }
//            _context.Categories.Remove(category);
//            //  Entity Framework is very powerful. We do not have to write any update statement in the powershell to update the db.
//            // The update statement takes care of it.
//            _context.SaveChanges();

//            //(85) Once the deletion is succesful, we can show a success message to the user. This is done using TempData.
//            // After posting all these we are redirecting to the Index action method which will show the list of categories.
//            // We can show a success message on that page in (86).
//            TempData["success"] = "Category deleted successfully";

//            return RedirectToAction("Index");
//        }
//    }
//}

// (108) I have commented the earlier code at this point
// Now as in 107 we have registered the CategoryService in the Program.cs file, we can use it in the CategoryController class.
// We will inject the ICategoryService interface in the constructor of the CategoryController class and use it to
// perform CRUD operations on the Category model.

using BulkyBook.BusinessAccess.Services.IServices;
using BulkyBook.Models;
using Microsoft.AspNetCore.Mvc;

//(113) Give the exact name of the area here
//Same for Home controller as done in (114)
[Area("Customer")]
public class CategoryController : Controller
{
    private ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    public async Task<IActionResult> Index()
    {
        //(111) Remember to put async before await keywords.
        //If you dont await, categories is passed on to the view even before it is initialised
        var categories = await _categoryService.GetAllCategoriesAsync();
        return View(categories);
    }

    public IActionResult Create()
    {
        return View();
    }

    [ValidateAntiForgeryToken]
    [HttpPost]
    [ActionName("Create")]
    public async Task<IActionResult> CreatePOST(Category category)
    {
        //(61) If we select asp-validation-for in (60) as model only
        if (!(string.IsNullOrEmpty(category.Name)) && !await _categoryService.IsCategoryNameUniqueAsync(category.Name, category.Id))
        {
            ModelState.AddModelError("", "Category name already exists");
        }

        if (ModelState.IsValid)
        {
            await _categoryService.CreateCategoryAsync(category);

            TempData["success"] = "Category created successfully";

            return RedirectToAction("Index");
        }

        return View();
    }

    public async Task<IActionResult> Update(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound(); // this is 404 not found error
        }
        var category = await _categoryService.GetCategoryByIdAsync(id.Value);
        if (category == null)
        {
            return NotFound();
        }
        return View(category);
    }

    [ValidateAntiForgeryToken]
    [HttpPost]
    [ActionName("Update")]
    public async Task<IActionResult> UpdatePOST(Category category)
    {
        if (!(string.IsNullOrEmpty(category.Name)) &&
            !await _categoryService.IsCategoryNameUniqueAsync(category.Name, category.Id))
        {
            ModelState.AddModelError("", "Category name already exists");
        }
        if (ModelState.IsValid)
        {
            await _categoryService.UpdateCategoryAsync(category);
            TempData["success"] = "Category updated successfully";

            return RedirectToAction("Index");
        }
        return View();
    }

    public async Task<ActionResult> Delete(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }

        var category = await _categoryService.GetCategoryByIdAsync(id.Value);
        return View(category);
    }

    [ValidateAntiForgeryToken]
    [HttpPost]
    [ActionName("Delete")]
    public async Task<IActionResult> DeletePOST(int? id)
    {
        await _categoryService.DeleteCategoryAsync(id.Value);

        TempData["success"] = "Category deleted successfully";

        return RedirectToAction("Index");
    }
}