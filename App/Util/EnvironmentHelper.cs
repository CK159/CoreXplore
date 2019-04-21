using System.Net;
using Microsoft.AspNetCore.Http;

namespace App.Util
{
	public static class EnvironmentHelper
	{
		public static bool IsDebug()
		{
#if DEBUG
			return true;
#else
			return false;
#endif
		}
	}
	
	/// <summary>
	/// From https://www.strathweb.com/2016/04/request-islocal-in-asp-net-core/
	/// </summary>
	public static class HttpRequestExtensions
	{
		public static bool IsLocal(this HttpRequest req)
		{
			var connection = req.HttpContext.Connection;
			if (connection.RemoteIpAddress != null)
			{
				if (connection.LocalIpAddress != null)
				{
					return connection.RemoteIpAddress.Equals(connection.LocalIpAddress);
				}
				else
				{
					return IPAddress.IsLoopback(connection.RemoteIpAddress);
				}
			}

			// for in memory TestServer or when dealing with default connection info
			if (connection.RemoteIpAddress == null && connection.LocalIpAddress == null)
			{
				return true;
			}

			return false;
		}
	}
}
