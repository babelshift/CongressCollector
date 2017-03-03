using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CongressCollector.Models.Converters
{
    public class BillAmendmentActionsTypeConverter : ITypeConverter<Original.Actions, IReadOnlyCollection<Cleaned.BillAmendmentAction>>
    {
        public IReadOnlyCollection<Cleaned.BillAmendmentAction> Convert(Original.Actions source, IReadOnlyCollection<Cleaned.BillAmendmentAction> destination, ResolutionContext context)
        {
            if (source != null && source.Items != null)
            {
                List<Cleaned.BillAmendmentAction> cleanedItems = new List<Cleaned.BillAmendmentAction>();
                cleanedItems = context.Mapper.Map<IEnumerable<Original.Item>, List<Cleaned.BillAmendmentAction>>(source.Items.Where(x => !String.IsNullOrWhiteSpace(x.Text)));
                return cleanedItems.AsReadOnly();
            }

            return null;
        }
    }
}