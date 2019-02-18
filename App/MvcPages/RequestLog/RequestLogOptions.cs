using System;
using App.Util;

namespace App.MvcPages.RequestLog
{
	/// <summary>
	/// All the filter, sort and paging options for the 
	/// </summary>
	public class RequestLogOptions : PageableOptions
	{
		public int? RequestLogId { get; set; }
		public RequestLogDateType DateType { get; set; }
		public DateTime? StartDate { get; set; }
		public DateTime? EndDate { get; set; }
		public bool InProgress { get; set; }
		public string URL { get; set; }
		public string IP { get; set; }
		public string RequestMethod { get; set; }
		public int? ResponseStatus { get; set; }
	}

	public enum RequestLogDateType
	{
		DateCreated,
		RequestBegin
	}
}
