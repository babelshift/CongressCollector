using AutoMapper;
using CongressCollector.Models.Utilities;
using System.Collections.Generic;

namespace CongressCollector.Models
{
    public class BillTypeConverter : ITypeConverter<Models.Original.Bill, Models.Cleaned.Bill>
    {
        public Models.Cleaned.Bill Convert(Models.Original.Bill source, Models.Cleaned.Bill destination, ResolutionContext context)
        {
            Models.Cleaned.Bill bill = new Models.Cleaned.Bill();

            bill.BillNumber = source.BillNumber;
            bill.Congress = source.Congress;
            bill.Title = source.Title;
            bill.ConstitutionalAuthorityStatement = source.ConstitutionalAuthorityStatementText;
            bill.BillType = source.BillType;
            bill.OriginChamber = source.OriginChamber;
            bill.UpdateDate = ParseHelpers.ParseNullableDateTime(source.UpdateDate);
            bill.IntroducedDate = ParseHelpers.ParseNullableDateTime(source.IntroducedDate);
            bill.CreateDate = ParseHelpers.ParseNullableDateTime(source.CreateDate);
            bill.BillActions = context.Mapper.Map<Original.Actions, IReadOnlyCollection<Cleaned.BillAction>>(source.Actions);
            bill.BillAmendments = context.Mapper.Map<Original.Amendments, IReadOnlyCollection<Cleaned.BillAmendment>>(source.Amendments);
            bill.BillSummaries = context.Mapper.Map<Original.Summaries, IReadOnlyCollection<Cleaned.BillSummary>>(source.Summaries);
            bill.CalendarNumbers = context.Mapper.Map<Original.CalendarNumbers, IReadOnlyCollection<Cleaned.CalendarNumber>>(source.CalendarNumbers);
            bill.BillCBOCostEstimates = context.Mapper.Map<Original.CboCostEstimates, IReadOnlyCollection<Cleaned.CBOCostEstimate>>(source.CboCostEstimates);
            bill.BillCommitteeReports = context.Mapper.Map<Original.CommitteeReports, IReadOnlyCollection<Cleaned.CommitteeReport>>(source.CommitteeReports);
            bill.BillCommittees = context.Mapper.Map<Original.Committees, IReadOnlyCollection<Cleaned.Committee>>(source.Committees);
            bill.BillCosponsors = context.Mapper.Map<Original.Cosponsors, IReadOnlyCollection<Cleaned.BillCosponsor>>(source.Cosponsors);
            bill.LatestAction = context.Mapper.Map<Original.LatestAction, Cleaned.BillAction>(source.LatestAction);
            bill.Laws = context.Mapper.Map<Original.Laws, IReadOnlyCollection<Cleaned.BillLaw>>(source.Laws);
            bill.BillNotes = context.Mapper.Map<Original.Notes, IReadOnlyCollection<Cleaned.BillNote>>(source.Notes);
            bill.PolicyArea = context.Mapper.Map<Original.PolicyArea, Cleaned.PolicyArea>(source.PolicyArea);
            bill.RecordedVotes = context.Mapper.Map<Original.RecordedVotes, IReadOnlyCollection<Cleaned.RecordedVote>>(source.RecordedVotes);
            bill.RelatedBills = context.Mapper.Map<Original.RelatedBills, IReadOnlyCollection<Cleaned.RelatedBill>>(source.RelatedBills);
            bill.BillSponsors = context.Mapper.Map<Original.Sponsors, IReadOnlyCollection<Cleaned.BillSponsor>>(source.Sponsors);
            bill.BillSubject = context.Mapper.Map<Original.Subjects, Cleaned.BillSubject>(source.Subjects);
            bill.BillTitles = context.Mapper.Map<Original.Titles, IReadOnlyCollection<Cleaned.BillTitle>>(source.Titles);

            return bill;
        }
    }
}