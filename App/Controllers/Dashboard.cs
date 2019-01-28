using Microsoft.AspNetCore.Mvc;

namespace App.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        
        public IActionResult Sample1()
        {
            return View();
        }
        
        public IActionResult Sample2()
        {
            return View();
        }
    }
}