using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CongressCollector.Models
{
    public class LinksTypeConverter : ITypeConverter<Original.Links, IReadOnlyCollection<Cleaned.Link>>
    {
        public IReadOnlyCollection<Cleaned.Link> Convert(Original.Links source, IReadOnlyCollection<Cleaned.Link> destination, ResolutionContext context)
        {
            return (source != null && source.Link != null)
            ? source.Link.Select(x => new Cleaned.Link()
            {
                Name = x.Name,
                URL = new Uri(x.Url) // might need to input validation/trycreate this
            }).ToList().AsReadOnly() : null;
        }
    }
}
