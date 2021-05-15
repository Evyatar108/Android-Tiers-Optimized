namespace MOARANDROIDS
{
    using System.Collections.Generic;

    static class CollectionExtensions
    {
        public static List<T> FastToList<T>(this ICollection<T> collection)
        {
            var newList = new List<T>(collection.Count);

            newList.AddRange(collection);

            return newList;
        }
    }
}
