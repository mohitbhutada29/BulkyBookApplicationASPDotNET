using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Controllers
{
    //(114) After adding Area, things still dont work. It gives page not found error,
    // This is because (115). But now the layout is not copied
    // This is because the two files _ViewImports and _ViewStart are not avaialable to the area.
    // We will add this files in the Customer and Admin folder too.
    [Area("Customer")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}