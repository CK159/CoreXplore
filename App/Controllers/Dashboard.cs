using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using App.Models;

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