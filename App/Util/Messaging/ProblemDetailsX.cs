using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace App.Util
{
	public class ProblemDetailsX : ProblemDetails
	{
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "details", Order = 4)]
		public List<MessageItem> Details { get; set; }
		
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "stackTraces", Order = 5)]
		public List<string> StackTraces { get; set; }

		public ProblemDetailsX()
		{
		}

		public ProblemDetailsX(Exception ex)
		{
			List<string> messages = ExtractExceptionMessages(ex);
			
			Title = "An unhandled error occurred.";
			Status = 500;
			Detail = String.Join("\n", messages);
			Details = messages.Select(m => new MessageItem(m, MessageType.Error)).ToList();
			StackTraces = ExtractExceptionMessages(ex, true);
		}

		public static List<string> ExtractExceptionMessages(Exception ex, bool getStackTraces = false)
		{
			List<string> result = new List<string>();

			Exception current = ex;

			while (current != null)
			{
				result.Add(getStackTraces ? current.StackTrace : current.Message);
				current = current.InnerException;
			}
			
			return result;
		}
	}
}
