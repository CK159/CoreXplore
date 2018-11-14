using System.Collections.Generic;
using System.Linq;
using Db;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace App.Controllers
{
    public class MessageController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.messages = new Dbc().Messages.OrderBy(x => x.DateCreated).ToList();
            ViewBag.info = "";

            return View();
        }

        [HttpPost]
        public IActionResult Index(string message)
        {
            Dbc dbc = new Dbc();
            if ((message?.Trim() ?? "") == "")
            {
                ViewBag.info = "Enter a message.";
            }
            else
            {
                dbc.Messages.Add(new Message
                {
                    MessageText = message
                });
                dbc.SaveChanges();

                ViewBag.info = $"Message '{message}' saved.";
            }

            ViewBag.messages = new Dbc().Messages.OrderBy(x => x.DateCreated).ToList();

            return View();
        }
    }
}