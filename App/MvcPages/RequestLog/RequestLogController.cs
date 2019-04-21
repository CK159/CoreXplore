using GenericServices;
using Microsoft.AspNetCore.Mvc;

namespace App.MvcPages.RequestLog
{
	public class RequestLogController : Controller
	{
		private readonly ICrudServices _service;
		private readonly RequestLogService _reqSvc;

		public RequestLogController(ICrudServices service, RequestLogService reqSvc)
		{
			_service = service;
			_reqSvc = reqSvc;
		}

		public IActionResult Index(RequestLogIndexOptions options)
		{
			return View(new IndexViewModel
			{
				Options = options,
				PagedItems = _reqSvc.IndexFilter(options)
			});
		}

		[HttpPost]
		public IActionResult IndexResultReload(RequestLogIndexOptions options)
		{
			return View("_IndexResultTable", _reqSvc.IndexFilter(options));
		}

		public IActionResult DetailPage(int requestLogId)
		{
			RequestLogDetailModel model = _service.ReadSingle<RequestLogDetailModel>(requestLogId);
			return View(model);
		}

		public IActionResult DetailModal(int requestLogId)
		{
			RequestLogDetailModel model = _service.ReadSingle<RequestLogDetailModel>(requestLogId);
			return View(model);
		}

		public IActionResult DetailPanel(int requestLogId)
		{
			RequestLogDetailModel model = _service.ReadSingle<RequestLogDetailModel>(requestLogId);
			return View(model);
		}

		public IActionResult SaveRequestLog(RequestLogDetailModel item)
		{
			_service.UpdateAndSave(item);

			return null; //TODO: Return JSON of some sort here
		}
	}
}
