using AutoMapper;
using CongressCollector.Models.Cleaned;
using System.Collections.Generic;

namespace CongressCollector.Models.Converters
{
    /// <summary>
    /// The XML structure of the Bill Status documents from the GPO FDsys database needs to have a special converter applied to many
    /// of the objects which are made up of collections of items. This converter should be applied to any automapping between
    /// objects that go from a container of a List of XML items to a List of cleaned JSON items.
    /// </summary>
    /// <typeparam name="TIn">Source type being converted from</typeparam>
    /// <typeparam name="TOut">Destination type being converted to (always returned as a list)</typeparam>
    /// <typeparam name="TItem">Item type contained within the source type's itemized list</typeparam>
    /// <remarks>
    /// For example, the XML structure may be (quotes replaced for less/greater than signs)
    /// "amendments"
    ///     "item"
    ///         "name" sample "/name"
    ///     "/item"
    ///     "item"
    ///         "name" sample2 "/name"
    ///     "/item"
    /// "/amendments"
    ///
    /// We apply this converter to this object to transform the list of items to a list of cleaned Amendment objects.
    /// </remarks>
    public class ItemizedTypeConverter<TIn, TOut, TItem> : ITypeConverter<TIn, IReadOnlyCollection<TOut>>
        where TIn : IItemized<TItem>
    {
        public IReadOnlyCollection<TOut> Convert(TIn source, IReadOnlyCollection<TOut> destination, ResolutionContext context)
        {
            if (source != null && source.Items != null)
            {
                List<TOut> cleanedItems = new List<TOut>();
                cleanedItems = context.Mapper.Map<List<TItem>, List<TOut>>(source.Items);
                return cleanedItems.AsReadOnly();
            }

            return null;
        }
    }
}