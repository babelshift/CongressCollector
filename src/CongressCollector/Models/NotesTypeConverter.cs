using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CongressCollector.Models
{
    public class NotesTypeConverter : ITypeConverter<Original.Notes, IReadOnlyCollection<Cleaned.BillNote>>
    {
        public IReadOnlyCollection<Cleaned.BillNote> Convert(Original.Notes source, IReadOnlyCollection<Cleaned.BillNote> destination, ResolutionContext context)
        {
            return (source != null && source.Items != null)
            ? source.Items.Select(x => new Cleaned.BillNote()
            {
                Links = context.Mapper.Map<Original.Links, IReadOnlyCollection<Cleaned.Link>>(x.Links),
                Text = x.Text
            }).ToList().AsReadOnly() : null;
        }
    }
}
