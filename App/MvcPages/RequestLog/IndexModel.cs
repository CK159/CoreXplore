using System;
using GenericServices;

namespace App.MvcPages.RequestLog
{
	public class RequestIndexModel : ILinkToEntity<Db.RequestLog>
	{
		public int RequestLogId { get; set; }
		public DateTime RequestBegin { get; set; }
		public string RequestMethod { get; set; }
		public string URL { get; set; }
		public string IP { get; set; }
		public int? ResponseStatus { get; set; }
		public decimal? ResponseMs { get; set; }
	}
}
