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

		public IActionResult Index(IndexOptions options)
		{
			IPagedList<RequestIndexModel> pagedItems = _service.ReadManyNoTracked<RequestIndexModel>()
				.OrderByDescending(i => i.RequestLogId)
				.ToPagedList(options.CurrentPage, options.PageSize);

			return View(new IndexViewModel
			{
				Options = options,
				PagedItems = pagedItems
			});
		}

		[HttpPost]
		public IActionResult Index(string message)
		{
			return View();
		}

		public IActionResult DetailPage(int requestLogId)
		{
			DetailModel model = _service.ReadSingle<DetailModel>(requestLogId);
			return View(model);
		}
	}
}
