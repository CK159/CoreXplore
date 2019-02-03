using Db;
using Microsoft.AspNetCore.Mvc;

namespace App.Controllers
{
	public class RequestLogController : Controller
	{
		readonly DbCore dbc;

		public RequestLogController(DbCore dbc)
		{
			this.dbc = dbc;
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
