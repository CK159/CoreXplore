using System.Linq;
using Db;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace App.Controllers
{
	[Authorize]
	public class MessageController : Controller
	{
		readonly DbCore dbc;

		public MessageController(DbCore dbc)
		{
			this.dbc = dbc;
		}

		public IActionResult Index()
		{
			ViewBag.messages = dbc.Messages.OrderBy(x => x.DateCreated).ToList();
			ViewBag.info = "";

			return View();
		}

		[HttpPost]
		public IActionResult Index(string message)
		{
			if((message?.Trim() ?? "") == "")
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

			ViewBag.messages = dbc.Messages.OrderBy(x => x.DateCreated).ToList();

			return View();
		}
	}
}
