namespace App.Util
{
	public static class StringExtensions
	{
		/// <summary>
		/// Returns first N characters of string.
		/// Returns input unmodified if input length is less than <paramref name="upToLength"/>.
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
		/// Returns input unmodified if input length is less than <paramref name="upToLength"/>.
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
	}
}
