using System;
using GenericServices;
using X.PagedList;

namespace App.MvcPages.RequestLog
{
	public class RequestLogIndexModel : ILinkToEntity<Db.RequestLog>
	{
		public int RequestLogId { get; set; }
		public DateTime RequestBegin { get; set; }
		public string RequestMethod { get; set; }
		public string URL { get; set; }
		public string IP { get; set; }
		public int? ResponseStatus { get; set; }
		public decimal? ResponseMs { get; set; }
	}
	
	public class IndexViewModel
	{
		public RequestLogIndexOptions Options { get; set; }
		public IPagedList<RequestLogIndexModel> PagedItems { get; set; }
	}
}
