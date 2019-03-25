using System.Linq;
using App.Util;
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
			IQueryable<RequestLogIndexModel> items = _service.ReadManyNoTracked<RequestLogIndexModel>();
			IPagedList<RequestLogIndexModel> pagedItems = IndexFilter(items, options);

			return View(new IndexViewModel
			{
				Options = options,
				PagedItems = pagedItems
			});
		}

		[HttpPost]
		public IActionResult IndexResultReload(RequestLogIndexOptions options)
		{
			IQueryable<RequestLogIndexModel> items = _service.ReadManyNoTracked<RequestLogIndexModel>();
			IPagedList<RequestLogIndexModel> pagedItems = IndexFilter(items, options);

			return View("_IndexResultTable", pagedItems);
		}

		//TODO: Move to RequestLogService
		private IPagedList<RequestLogIndexModel> IndexFilter(IQueryable<RequestLogIndexModel> model, RequestLogIndexOptions options)
		{
			if (options.RequestLogId != null)
			{
				model = model.Where(m => m.RequestLogId == options.RequestLogId);
			}
			if (options.StartDate != null)
			{
				model = model.Where(m => m.RequestBegin >= options.StartDate);
			}
			if (options.EndDate != null)
			{
				model = model.Where(m => m.RequestBegin <= options.EndDate);
			}
			if (options.InProgress)
			{
				model = model.Where(m => m.ResponseMs == null);
			}
			if (!options.URL.IsNullOrWhitespace())
			{
				model = model.Where(m => m.URL.Contains(options.URL));
			}
			if (!options.IP.IsNullOrWhitespace())
			{
				model = model.Where(m => m.IP.Contains(options.IP));
			}
			if (!options.RequestMethod.IsNullOrWhitespace())
			{
				model = model.Where(m => m.RequestMethod.Contains(options.RequestMethod));
			}
			if (options.ResponseStatus != null)
			{
				model = model.Where(m => m.ResponseStatus == options.ResponseStatus);
			}

			model = model.OrderByDescending(i => i.RequestLogId);
			
			return model.ToPagedList(options.CurrentPage, options.PageSize);
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
