using System.Collections.Generic;
using System.Linq;
using GenericServices;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace App.MvcPages.RequestLog
{
	public class RequestLogController : Controller
	{
		private readonly ICrudServices _service;

		public RequestLogController(ICrudServices _service)
		{
			this._service = _service;
		}

		public IActionResult Index()
		{
			IPagedList<RequestIndexModel> model = _service.ReadManyNoTracked<RequestIndexModel>()
				.OrderByDescending(i => i.RequestLogId)
				.ToPagedList(1, 30);
			
			return View(model);
		}

		[HttpPost]
		public IActionResult Index(string message)
		{
			return View();
		}
	}
}
