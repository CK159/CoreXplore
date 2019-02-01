using System.Linq;
using Db;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace App.Controllers
{
	[Authorize]
	public class MessageController : Controller
	{
		readonly Dbc context;

		public MessageController(Dbc context)
		{
			this.context = context;
		}

		public IActionResult Index()
		{
			ViewBag.messages = context.Messages.OrderBy(x => x.DateCreated).ToList();
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
				context.Messages.Add(new Message
				{
					MessageText = message
				});
				context.SaveChanges();

				ViewBag.info = $"Message '{message}' saved.";
			}

			ViewBag.messages = context.Messages.OrderBy(x => x.DateCreated).ToList();

			return View();
		}
	}
}
