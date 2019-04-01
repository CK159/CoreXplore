using System.Linq;
using App.Util;
using GenericServices;
using X.PagedList;

namespace App.MvcPages.RequestLog
{
	public class RequestLogService
	{
		private readonly ICrudServices _service;

		public RequestLogService(ICrudServices service)
		{
			_service = service;
		}

		public IPagedList<RequestLogIndexModel> IndexFilter(RequestLogIndexOptions options)
		{
			IQueryable<RequestLogIndexModel> items = _service.ReadManyNoTracked<RequestLogIndexModel>();

			if (options.RequestLogId != null)
			{
				items = items.Where(m => m.RequestLogId == options.RequestLogId);
			}

			if (options.StartDate != null)
			{
				items = items.Where(m => m.RequestBegin >= options.StartDate);
			}

			if (options.EndDate != null)
			{
				items = items.Where(m => m.RequestBegin <= options.EndDate);
			}

			if (options.InProgress)
			{
				items = items.Where(m => m.ResponseMs == null);
			}

			if (!options.URL.IsNullOrWhitespace())
			{
				items = items.Where(m => m.URL.Contains(options.URL));
			}

			if (!options.IP.IsNullOrWhitespace())
			{
				items = items.Where(m => m.IP.Contains(options.IP));
			}

			if (!options.RequestMethod.IsNullOrWhitespace())
			{
				items = items.Where(m => m.RequestMethod.Contains(options.RequestMethod));
			}

			if (options.ResponseStatus != null)
			{
				items = items.Where(m => m.ResponseStatus == options.ResponseStatus);
			}

			items = items.OrderByDescending(i => i.RequestLogId);

			return items.ToPagedList(options.CurrentPage, options.PageSize);
		}
	}
}
