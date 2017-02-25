using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CongressCollector.Models
{
    public class BillAmendmentActionsTypeConverter : ITypeConverter<Original.Actions, IReadOnlyCollection<Cleaned.BillAmendmentAction>>
    {
        public IReadOnlyCollection<Cleaned.BillAmendmentAction> Convert(Original.Actions source, IReadOnlyCollection<Cleaned.BillAmendmentAction> destination, ResolutionContext context)
        {
            List<Cleaned.BillAmendmentAction> billAmendmentActions = new List<Cleaned.BillAmendmentAction>();

            if (source != null && source.InnerActions != null && source.InnerActions.Items != null)
            {
                billAmendmentActions = context.Mapper.Map<List<Original.Item>, List<Cleaned.BillAmendmentAction>>(source.InnerActions.Items);
            }

            return billAmendmentActions.AsReadOnly();
        }
    }
}
