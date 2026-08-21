// (3) This file is like a control room for your application.
// It sets up the web application, configures services, and defines how requests are handled.
// There are two jobs this file perform:
// 1. It configures services that the application will use, such as controllers and views.
// 2. It defines the middleware pipeline that processes incoming HTTP requests and generates responses.


// Setting up the web application builder with command-line arguments.
using BulkyBook.BusinessAccess.Services;
using BulkyBook.BusinessAccess.Services.IServices;
using BulkyBook.DataAccess.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// (4) Add services to the container.
// Some examples of services that can be added include database
// contexts, authentication services, and MVC controllers with views.
builder.Services.AddControllersWithViews();

// (This step is to be done only after adding EF core and EF Core tools nuget packages)
// Register EF core in Services.
// We need to tell the application that we want to use EntityFramework and the SQL Server database provider.
// This is done by adding the ApplicationDbContext to the service container
// and configuring it to use SQL Server with a connection string from the configuration.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    //This retrieves the value of the connection string that we have in appSettings.json
    //options.UseSqlServer(builder.Configuration.GetSection("ConnectionStrings:SQLConnection").Value);

    // Can also be done by following since "ConnectionStrings" is a special section.
    // This is a shorthand for above GetSection command
    options.UseSqlServer(builder.Configuration.GetConnectionString("SQLConnection"));

}
);

// (107) Registering Category service
builder.Services.AddScoped<ICategoryService, CategoryService>();

var app = builder.Build();

// (5) Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// (6) Middleware to redirect HTTP requests to HTTPS.
app.UseHttpsRedirection();

// (7) Matches incoming urls to right controllers and action methods.
// It is responsible for routing requests to the appropriate endpoints based on the URL and HTTP method.
app.UseRouting();

app.UseAuthorization();

// (8) MapStaticAssets is a custom extension method that maps static assets to the application.
// This allows the application to serve static files like images, CSS, and JavaScript from a specified directory.
app.MapStaticAssets();


// (112) Adding areas to project
// In the web project-> add a new folder named Areas -> right click add MVC area
// After this we move all the controllers from Shared -> Controllers in Customer area.
// And views fro Shared folder -> Views
// If we build now, it wont work.
// For this we need to associate the controller with the Area attribute as done in 113
app.MapControllerRoute(
    name: "MyArea",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


// (9) MapControllerRoute defines a route for the application.
// WithStaticAssets is a custom extension method that adds static asset handling to the route.

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}",
    defaults: new { area = "Customer" })
    .WithStaticAssets();

//(115) In above code, we have not defined Views for default mode, but it does not exists.
// So we add a line that says if the default view exists return taht or return the Customer area
// defaults: new { area = "Customer" }

// (10) Run the application and start listening for incoming HTTP requests.
app.Run();


// Last index or bullet point: 