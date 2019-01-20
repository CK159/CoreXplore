using System.Linq;
using Db;
using Microsoft.AspNetCore.Mvc;

namespace App.Controllers
{
    public class RequestLogController : Controller
    {
        readonly Dbc context;
 
        public RequestLogController(Dbc context)
        {
            this.context = context;
        }
        
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(string message)
        {
            return View();
        }
    }
}