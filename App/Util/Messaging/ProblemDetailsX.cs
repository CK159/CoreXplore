using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace App.Util
{
	public class ProblemDetailsX : ProblemDetails
	{
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "details", Order = 4)]
		public List<MessageItem> Details { get; set; }

		[JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "trace", Order = 5)]
		public string Trace { get; set; }

		/// <summary>
		/// The method name and file path information from the top stack frame with that information available
		/// </summary>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "probableSource", Order = -1)]
		public string ProbableSource { get; set; }

		public ProblemDetailsX()
		{
		}

		public ProblemDetailsX(Exception ex, bool withStackTrace = false)
		{
			List<string> messages = ExtractExceptionMessages(ex);

			Title = "An unhandled error occurred.";
			Status = 500;
			Detail = String.Join("\n", messages);
			Details = messages.Select(m => new MessageItem(m, MessageType.Error)).ToList();
			if (withStackTrace)
			{
				Trace = ex.ToString();
				ProbableSource = GetProbableSource(ex);
			}
		}

		public static List<string> ExtractExceptionMessages(Exception ex)
		{
			List<string> result = new List<string>();

			Exception current = ex;

			while (current != null)
			{
				result.Add(current.Message);
				current = current.InnerException;
			}

			return result;
		}

		/// <summary>
		/// Finds the first frame in stack trace which has file path information
		/// as that is likely to be where the problem needs to be addressed 
		/// </summary>
		/// <param name="ex"></param>
		/// <returns></returns>
		public static string GetProbableSource(Exception ex)
		{
			StackTrace st = new StackTrace(ex, true);
			for (int i = 0; i < st.FrameCount; i++)
			{
				StackFrame sf = st.GetFrame(i);
				string fileName = sf.GetFileName();
				
				if (!fileName.IsNullOrWhitespace())
				{
					return $"{sf.GetMethod().Name} in {fileName}:{sf.GetFileLineNumber()}:{sf.GetFileColumnNumber()}";
				}
			}

			return null;
		}
	}
}
