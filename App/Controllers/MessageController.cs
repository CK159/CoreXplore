using System.Linq;
using Db;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace App.Controllers
{
	[Authorize]
	public class MessageController : Controller
	{
		private readonly DbCore dbc;
		private readonly ILogger<MessageController> logger;

		public MessageController(DbCore dbc, ILogger<MessageController> logger)
		{
			this.dbc = dbc;
			this.logger = logger;
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
			if ((message?.Trim() ?? "") == "")
			{
				ViewBag.info = "Enter a message.";
			}
			else
			{
				Message msg = new Message {MessageText = message};
				dbc.Messages.Add(msg);
				dbc.SaveChanges();
				
				logger.LogInformation("New message {MessageId} created", msg.MessageId);

				ViewBag.info = $"Message '{message}' saved.";
			}

			ViewBag.messages = dbc.Messages.OrderBy(x => x.DateCreated).ToList();

			return View();
		}
	}
}
