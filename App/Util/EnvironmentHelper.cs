using System;
using System.Text;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace App.Util
{
	public static class MetricsHelper
	{
		/*public static string EnvironmentInfo(this HtmlHelper htmlHelper)
		{
			StringBuilder sb = new StringBuilder();

			sb.Append(htmlHelper.ViewContext.HttpContext.Request.IsLocal() ? "Local" : "Remote");
			
			sb.Append(hostingEnv.EnvironmentName Config |)
			
			return sb.ToString();
		}*/

		public static bool IsDebug()
		{
#if DEBUG
			return true;
#else
			return false;
#endif
		}
	}
}
