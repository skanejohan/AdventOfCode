using System.Collections.Generic;

namespace CSharpLib.Extensions
{
    public static class DictionaryExtensions
    {
        public static void AddToList<TKey, TValue>(this Dictionary<TKey, List<TValue>> dictionary, TKey key, TValue value) where TKey : notnull
        {
            if (dictionary.TryGetValue(key, out var list))
            {
                list.Add(value);
            }
            else
            {
                dictionary[key] = new List<TValue> { value };
            }
        }
    }
}
