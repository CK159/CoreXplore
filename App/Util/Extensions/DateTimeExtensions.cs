using System;

namespace App.Util
{
	public static class DateTimeExtensions
	{
		/// <summary>
		/// Date-only format for general display in application.
		/// yyyy-MM-dd
		/// </summary>
		/// <param name="dt"></param>
		/// <returns></returns>
		public static string ToStandardDate(this DateTime dt)
		{
			return dt.ToString("yyyy-MM-dd");
		}

		/// <summary>
		/// Date-only format for general display in application.
		/// yyyy-MM-dd
		/// </summary>
		/// <param name="dt"></param>
		/// <returns></returns>
		public static string ToStandardDate(this DateTime? dt)
		{
			return dt?.ToStandardDate() ?? "";
		}

		/// <summary>
		/// Date and time format for general display in application.
		/// yyyy-MM-dd hh:mm tt
		/// </summary>
		/// <param name="dt"></param>
		/// <returns></returns>
		public static string ToStandardDateTime(this DateTime dt)
		{
			return dt.ToString("yyyy-MM-dd hh:mm tt");
		}

		/// <summary>
		/// Date and time format for general display in application.
		/// yyyy-MM-dd hh:mm tt
		/// </summary>
		/// <param name="dt"></param>
		/// <returns></returns>
		public static string ToStandardDateTime(this DateTime? dt)
		{
			return dt?.ToStandardDateTime() ?? "";
		}

		/// <summary>
		/// Date and time format with additional precision for display in the application
		/// yyyy-MM-dd hh:mm:ss.fff tt
		/// </summary>
		/// <param name="dt"></param>
		/// <returns></returns>
		public static string ToPreciseDateTime(this DateTime dt)
		{
			return dt.ToString("yyyy-MM-dd hh:mm:ss.fff tt");
		}

		/// <summary>
		/// Date and time format with additional precision for display in the application
		/// yyyy-MM-dd hh:mm:ss.fff tt
		/// </summary>
		/// <param name="dt"></param>
		/// <returns></returns>
		public static string ToPreciseDateTime(this DateTime? dt)
		{
			return dt?.ToPreciseDateTime() ?? "";
		}
	}
}
