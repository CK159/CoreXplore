using System;
using App.Util;
using Db;
using GenericServices;
using X.PagedList;

namespace App.MvcPages.RequestLog
{
	#region Index

	/// <summary>
	/// All the filter, sort and paging options for the Request Log index page
	/// </summary>
	public class RequestLogIndexOptions : PageableOptions
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
		public RequestLogDetailType DetailType { get; set; }
	}

	public enum RequestLogDateType
	{
		DateCreated,
		RequestBegin
	}

	public enum RequestLogDetailType
	{
		Modal,
		Panel,
		SamePage,
		NewPage,
		All
	}

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

	#endregion

	#region Detail

	public class RequestLogDetailModel : ILinkToEntity<Db.RequestLog>
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

	#endregion
}
