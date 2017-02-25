using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CongressCollector.Models
{
    public class BillAmentmentSponsorsTypeConverter : ITypeConverter<Original.Sponsors, IReadOnlyCollection<Cleaned.BillAmendmentSponsor>>
    {
        public IReadOnlyCollection<Cleaned.BillAmendmentSponsor> Convert(Original.Sponsors source, 
            IReadOnlyCollection<Cleaned.BillAmendmentSponsor> destination, ResolutionContext context)
        {
            List<Cleaned.BillAmendmentSponsor> billAmendmentSponsors = new List<Cleaned.BillAmendmentSponsor>();

            if (source != null && source.Items != null)
            {
                billAmendmentSponsors = context.Mapper.Map<List<Original.Item>, List<Cleaned.BillAmendmentSponsor>>(source.Items);
            }

            return billAmendmentSponsors.AsReadOnly();
        }
    }
}
