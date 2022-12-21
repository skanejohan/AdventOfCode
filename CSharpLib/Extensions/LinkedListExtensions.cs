using System.Collections.Generic;

namespace CSharpLib.Extensions
{
    public static class LinkedListExtensions
    {
        public static string AsString<T>(this LinkedList<T> list)
        {
            var items = new List<string>();
            var node = list.First;
            if (node == null)
            {
                return "";
            }
            do
            {
                items.Add($"{node.Value}");
                node = node.Next;
            } while (node != null);
            return string.Join(",", items);
        }
    }
}
