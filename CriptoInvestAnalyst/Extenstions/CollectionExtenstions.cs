using System.Collections.Generic;

namespace Extenstions
{
    public static class CollectionExtenstions
    {
        public static bool OutOfBounds<T>(this T[] array, int index)
        {
            return index < 0 || index >= array.Length;
        }

        public static bool OutOfBounds<T>(this List<T> collection, int index)
        {
            return index < 0 || index >= collection.Count;
        }
    }
}
