using AutoMapper;
using System.Collections.Generic;

namespace CongressCollector.Models
{
    public class BillTitlesTypeConverter : ITypeConverter<Original.Titles, IReadOnlyCollection<Cleaned.BillTitle>>
    {
        public IReadOnlyCollection<Cleaned.BillTitle> Convert(Original.Titles source, IReadOnlyCollection<Cleaned.BillTitle> destination, ResolutionContext context)
        {
            List<Cleaned.BillTitle> billTitles = new List<Cleaned.BillTitle>();

            if (source != null && source.Items != null)
            {
                billTitles = context.Mapper.Map<List<Original.Item>, List<Cleaned.BillTitle>>(source.Items);
            }

            return billTitles.AsReadOnly();
        }
    }
}