using System;
using System.Collections.Generic;

namespace App.Util
{
	public static class Extensions
	{
		/// <summary>
		/// Retrieves the value from the dictionary or returns the default value if key not present in dictionary.
		/// https://codereview.stackexchange.com/a/110640
		/// </summary>
		/// <param name="dictionary"></param>
		/// <param name="key"></param>
		/// <param name="defaultValue"></param>
		/// <typeparam name="TKey"></typeparam>
		/// <typeparam name="TValue"></typeparam>
		/// <returns></returns>
		/// <exception cref="ArgumentNullException"></exception>
		public static TValue GetValueOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key,
			TValue defaultValue = default(TValue))
		{
			if (dictionary == null)
			{
				throw new ArgumentNullException(nameof(dictionary));
			} // using C# 6

			if (key == null)
			{
				throw new ArgumentNullException(nameof(key));
			} //  using C# 6

			TValue value;
			return dictionary.TryGetValue(key, out value) ? value : defaultValue;
		}

		/// <summary>
		/// Returns first N characters of string.
		/// Returns input unmodified if input length is less than upToLength.
		/// </summary>
		/// <param name="str"></param>
		/// <param name="upToLength"></param>
		/// <returns></returns>
		public static string Left(this string str, int upToLength)
		{
			if (str.Length <= upToLength)
				return str;

			return str.Substring(0, upToLength);
		}

		/// <summary>
		/// Returns last N characters of string.
		/// Returns input unmodified if input length is less than upToLength.
		/// </summary>
		/// <param name="str"></param>
		/// <param name="upToLength"></param>
		/// <returns></returns>
		public static string Right(this string str, int upToLength)
		{
			if (str.Length <= upToLength)
				return str;

			return str.Substring(str.Length - upToLength);
		}

		/// <summary>
		/// Indicates whether this string is null, empty, or consists only of whitespace characters.
		/// </summary>
		/// <param name="str"></param>
		/// <returns></returns>
		public static bool IsNullOrWhitespace(this string str)
		{
			return string.IsNullOrWhiteSpace(str);
		}

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
		/// Date and time format with additional precision for display in the application
		/// yyyy-MM-dd hh:mm:ss.fff tt
		/// </summary>
		/// <param name="dt"></param>
		/// <returns></returns>
		public static string ToPreciseDateTime(this DateTime dt)
		{
			return dt.ToString("yyyy-MM-dd hh:mm:ss.fff tt");
		}
	}
}
