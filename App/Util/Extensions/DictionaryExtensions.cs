using System;
using System.Collections.Generic;

namespace App.Util
{
	public static class DictionaryExtensions
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
	}
}
