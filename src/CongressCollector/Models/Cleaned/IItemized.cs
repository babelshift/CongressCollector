using System.Collections.Generic;

namespace CongressCollector.Models.Cleaned
{
    /// <summary>
    /// Any class that implements this is claiming that it is itemized and contains a collection of items
    /// that originated from the XML structure. This interface is used during the automapping type conversion
    /// between lists of XML items to lists of cleaned JSON objects.
    /// </summary>
    /// <typeparam name="T">Type of item</typeparam>
    public interface IItemized<T>
    {
        List<T> Items { get; set; }
    }
}