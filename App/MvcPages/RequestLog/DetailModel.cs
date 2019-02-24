using System;
using Db;
using GenericServices;

namespace App.MvcPages.RequestLog
{
	public class DetailModel : ILinkToEntity<Db.RequestLog>
	{
		public int RequestLogId { get; set; }
		public DateTime DateCreated { get; set; }
		public DateTime RequestBegin { get; set; }
		public string URL { get; set; }
		public string RequestMethod { get; set; }
		public string RequestContentType { get; set; }
		public int RequestSize { get; set; }
		public string RequestText { get; set; }
		public int? ResponseStatus { get; set; }
		public string ResponseContentType { get; set; }
		public int? ResponseSize { get; set; }
		public decimal? ResponseMs { get; set; }
		public ResponseTypes ResponseType { get; set; }
		public string ResponseText { get; set; }
		public string IP { get; set; }
		public string UserAgent { get; set; }
		public string Referer { get; set; }
		public string Location { get; set; }
	}
}
