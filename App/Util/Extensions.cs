using System;
using System.Collections.Generic;

namespace App.Util
{
    public static class Extensions
    {
        //https://codereview.stackexchange.com/a/110640
        public static TValue GetValueOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue defaultValue = default(TValue))
        {
            if (dictionary == null) { throw new ArgumentNullException(nameof(dictionary)); } // using C# 6
            if (key == null) { throw new ArgumentNullException(nameof(key)); } //  using C# 6

            TValue value;
            return dictionary.TryGetValue(key, out value) ? value : defaultValue;
        }  
    }
}