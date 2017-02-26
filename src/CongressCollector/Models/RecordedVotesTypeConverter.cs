using AutoMapper;
using CongressCollector.Models.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CongressCollector.Models
{
    public class RecordedVotesTypeConverter : ITypeConverter<Original.RecordedVotes, IReadOnlyCollection<Cleaned.RecordedVote>>
    {
        public IReadOnlyCollection<Cleaned.RecordedVote> Convert(Original.RecordedVotes source, IReadOnlyCollection<Cleaned.RecordedVote> destination, ResolutionContext context)
        {
            return (source != null && source.InnerRecordedVotes != null)
            ? source.InnerRecordedVotes.Select(x => new Cleaned.RecordedVote()
            {
                Chamber = x.Chamber,
                Date = ParseHelpers.ParseNullableDateTime(x.Date),
                Congress = x.Congress,
                FullActionName = x.FullActionName,
                RollNumber = x.RollNumber,
                SessionNumber = x.SessionNumber,
                URL = new Uri(x.Url) // validate this?
            }).ToList().AsReadOnly() : null;
        }
    }
}
