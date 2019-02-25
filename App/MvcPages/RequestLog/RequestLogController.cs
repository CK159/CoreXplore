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

		public IActionResult Index(RequestLogIndexOptions options)
		{
			IPagedList<RequestLogIndexModel> pagedItems = _service.ReadManyNoTracked<RequestLogIndexModel>()
				.OrderByDescending(i => i.RequestLogId)
				.ToPagedList(options.CurrentPage, options.PageSize);


			var y = (options.DetailType == RequestLogDetailType.All ? "selected" : "");

			return View(new IndexViewModel
			{
				Options = options,
				PagedItems = pagedItems
			});
		}

		/*[HttpPost]
		public IActionResult Index(string message)
		{
			return View();
		}*/

		public IActionResult DetailPage(int requestLogId)
		{
			RerquestLogDetailModel model = _service.ReadSingle<RerquestLogDetailModel>(requestLogId);
			return View(model);
		}

		public IActionResult DetailModal(int requestLogId)
		{
			RerquestLogDetailModel model = _service.ReadSingle<RerquestLogDetailModel>(requestLogId);
			return View(model);
		}

		public IActionResult DetailPanel(int requestLogId)
		{
			RerquestLogDetailModel model = _service.ReadSingle<RerquestLogDetailModel>(requestLogId);
			return View(model);
		}

		public IActionResult SaveRequestLog(RerquestLogDetailModel item)
		{
			_service.UpdateAndSave(item);

			return null; //TODO: Return JSON of some sort here
		}
	}
}
