using AutoMapper;
using CongressCollector.Models.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CongressCollector.Models
{
    public class RelationshipDetailsTypeConverter : ITypeConverter<Original.RelationshipDetails, IReadOnlyCollection<Cleaned.RelationshipDetail>>
    {
        public IReadOnlyCollection<Cleaned.RelationshipDetail> Convert(Original.RelationshipDetails source, 
            IReadOnlyCollection<Cleaned.RelationshipDetail> destination, ResolutionContext context)
        {
            return (source != null && source.Items != null)
            ? source.Items.Select(x => context.Mapper.Map<Original.Item, Cleaned.RelationshipDetail>(x))
            .ToList().AsReadOnly() : null;
        }
    }
}
