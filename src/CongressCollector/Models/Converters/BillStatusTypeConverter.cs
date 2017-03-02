using AutoMapper;
using CongressCollector.Models.Utilities;
using System.Collections.Generic;

namespace CongressCollector.Models.Converters
{
    public class BillStatusTypeConverter : ITypeConverter<Original.Bill, Cleaned.BillStatus>
    {
        public Cleaned.BillStatus Convert(Original.Bill source, Cleaned.BillStatus destination, ResolutionContext context)
        {
            Cleaned.BillStatus bill = new Cleaned.BillStatus();

            bill.Number = source.BillNumber;
            bill.Congress = source.Congress;
            bill.Title = source.Title;
            bill.ConstitutionalAuthorityStatement = ParseHelpers.ParseAndStripHTML(source.ConstitutionalAuthorityStatementText);
            bill.Type = source.BillType;
            bill.OriginChamber = source.OriginChamber;
            bill.UpdateDate = ParseHelpers.ParseNullableDateTime(source.UpdateDate);
            bill.IntroducedDate = ParseHelpers.ParseNullableDateTime(source.IntroducedDate);
            bill.CreateDate = ParseHelpers.ParseNullableDateTime(source.CreateDate);
            bill.Actions = context.Mapper.Map<Original.Actions, IReadOnlyCollection<Cleaned.BillStatusAction>>(source.Actions);
            bill.Amendments = context.Mapper.Map<Original.Amendments, IReadOnlyCollection<Cleaned.BillStatusAmendment>>(source.Amendments);
            bill.Summaries = context.Mapper.Map<Original.Summaries, IReadOnlyCollection<Cleaned.BillStatusSummary>>(source.Summaries);
            bill.CalendarNumbers = context.Mapper.Map<Original.CalendarNumbers, IReadOnlyCollection<Cleaned.BillStatusCalendarNumber>>(source.CalendarNumbers);
            bill.CBOCostEstimates = context.Mapper.Map<Original.CboCostEstimates, IReadOnlyCollection<Cleaned.BillStatusCBOCostEstimate>>(source.CboCostEstimates);
            bill.CommitteeReports = context.Mapper.Map<Original.CommitteeReports, IReadOnlyCollection<Cleaned.BillStatusCommitteeReport>>(source.CommitteeReports);
            bill.Committees = context.Mapper.Map<Original.Committees, IReadOnlyCollection<Cleaned.BillStatusCommittee>>(source.Committees);
            bill.Cosponsors = context.Mapper.Map<Original.Cosponsors, IReadOnlyCollection<Cleaned.BillStatusCosponsor>>(source.Cosponsors);
            bill.LatestAction = context.Mapper.Map<Original.LatestAction, Cleaned.LatestAction>(source.LatestAction);
            bill.Laws = context.Mapper.Map<Original.Laws, IReadOnlyCollection<Cleaned.BillStatusLaw>>(source.Laws);
            bill.Notes = context.Mapper.Map<Original.Notes, IReadOnlyCollection<Cleaned.BillStatusNote>>(source.Notes);
            bill.PolicyArea = context.Mapper.Map<Original.PolicyArea, Cleaned.PolicyArea>(source.PolicyArea);
            bill.RecordedVotes = context.Mapper.Map<Original.RecordedVotes, IReadOnlyCollection<Cleaned.BillStatusRecordedVote>>(source.RecordedVotes);
            bill.RelatedBills = context.Mapper.Map<Original.RelatedBills, IReadOnlyCollection<Cleaned.BillStatusRelatedBill>>(source.RelatedBills);
            bill.Sponsors = context.Mapper.Map<Original.Sponsors, IReadOnlyCollection<Cleaned.BillStatusSponsor>>(source.Sponsors);
            bill.Subject = context.Mapper.Map<Original.Subjects, Cleaned.BillStatusSubject>(source.Subjects);
            bill.Titles = context.Mapper.Map<Original.Titles, IReadOnlyCollection<Cleaned.BillStatusTitle>>(source.Titles);

            return bill;
        }
    }
}