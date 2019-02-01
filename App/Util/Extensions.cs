using System;
using System.Collections.Generic;

namespace App.Util
{
	public static class Extensions
	{
		//https://codereview.stackexchange.com/a/110640
		public static TValue GetValueOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key,
			TValue defaultValue = default(TValue))
		{
			if(dictionary == null)
			{
				throw new ArgumentNullException(nameof(dictionary));
			} // using C# 6

			if(key == null)
			{
				throw new ArgumentNullException(nameof(key));
			} //  using C# 6

			TValue value;
			return dictionary.TryGetValue(key, out value) ? value : defaultValue;
		}

		/// <summary>
		/// Returns first N characters of string.
		/// Returns input unmodified if input length is less than upToLength
		/// </summary>
		/// <param name="str"></param>
		/// <param name="upToLength"></param>
		/// <returns></returns>
		public static string Left(this string str, int upToLength)
		{
			if(str.Length <= upToLength)
				return str;

			return str.Substring(0, upToLength);
		}

		/// <summary>
		/// Returns last N characters of string.
		/// Returns input unmodified if input length is less than upToLength
		/// </summary>
		/// <param name="str"></param>
		/// <param name="upToLength"></param>
		/// <returns></returns>
		public static string Right(this string str, int upToLength)
		{
			if(str.Length <= upToLength)
				return str;

			return str.Substring(str.Length - upToLength);
		}

		public static bool IsNullOrWhitespace(this string str)
		{
			return string.IsNullOrWhiteSpace(str);
		}
	}
}
