using System;
using System.Collections.Generic;

namespace CongressCollector.Models.Cleaned
{
    public class BillStatus
    {
        public string BillId { get { return String.Format("{0}{1}-{2}", Type, Number, Congress); } }
        public string Number { get; set; }
        public string Congress { get; set; }
        public string Title { get; set; }
        public string ConstitutionalAuthorityStatement { get; set; }
        public string Type { get; set; }
        public string OriginChamber { get; set; }
        public DateTime? IntroducedDate { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public PolicyArea PolicyArea { get; set; }
        public BillStatusSubject Subject { get; set; }
        public LatestAction LatestAction { get; set; }
        public IReadOnlyCollection<BillStatusAction> Actions { get; set; }
        public IReadOnlyCollection<BillStatusAmendment> Amendments { get; set; }
        public IReadOnlyCollection<BillStatusCBOCostEstimate> CBOCostEstimates { get; set; }
        public IReadOnlyCollection<BillStatusCommitteeReport> CommitteeReports { get; set; }
        public IReadOnlyCollection<BillStatusLaw> Laws { get; set; }
        public IReadOnlyCollection<BillStatusSponsor> Sponsors { get; set; }
        public IReadOnlyCollection<BillStatusCommittee> Committees { get; set; }
        public IReadOnlyCollection<BillStatusNote> Notes { get; set; }
        public IReadOnlyCollection<BillStatusRecordedVote> RecordedVotes { get; set; }
        public IReadOnlyCollection<BillStatusCosponsor> Cosponsors { get; set; }
        public IReadOnlyCollection<BillStatusRelatedBill> RelatedBills { get; set; }
        public IReadOnlyCollection<BillStatusSummary> Summaries { get; set; }
        public IReadOnlyCollection<BillStatusTitle> Titles { get; set; }
        public IReadOnlyCollection<BillStatusCalendarNumber> CalendarNumbers { get; set; }
    }

    public class LatestAction
    {
        public DateTime? ActionDate { get; set; }
        public string Text { get; set; }
    }

    public class BillStatusNote
    {
        public string Text { get; set; }
        public IReadOnlyCollection<Link> Links { get; set; }
    }

    public class BillStatusCalendarNumber
    {
        public string Calendar { get; set; }
        public string Number { get; set; }
    }

    public class BillStatusTitle
    {
        public string ChamberCode { get; set; }
        public string ChamberName { get; set; }
        public string ParentTitleType { get; set; }
        public string Title { get; set; }
        public string TitleType { get; set; }
    }

    public class BillStatusSummary
    {
        public DateTime? ActionDate { get; set; }
        public string ActionDescription { get; set; }
        public string Text { get; set; }
        public DateTime? LastSummaryUpdateDate { get; set; }
        public string Name { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string VersionCode { get; set; }
    }

    public class BillStatusRelatedBill
    {
        public string LatestTitle { get; set; }
        public string Number { get; set; }
        public string Type { get; set; }
        public string Congress { get; set; }
        public LatestAction LatestAction { get; set; }
        public IReadOnlyCollection<RelationshipDetail> RelationshipDetails { get; set; }
    }

    public class RelatedBillAction
    {
        public DateTime? Date { get; set; } // combine time+date
        public string Text { get; set; }
    }

    public class RelationshipDetail
    {
        public string Type { get; set; }
        public string IdentifiedBy { get; set; }
    }

    public class BillStatusCosponsor
    {
        public string Party { get; set; }
        public string District { get; set; }
        public string MiddleName { get; set; }
        public string BioGuideId { get; set; }
        public DateTime? SponsorshipDate { get; set; }
        public bool? IsOriginalCosponsor { get; set; }
        public string FirstName { get; set; }
        public string FullName { get; set; }
        public DateTime? SponsorshipWithdrawnDate { get; set; }
        public Identifiers Identifiers { get; set; }
        public string LastName { get; set; }
        public string State { get; set; }
    }

    public class BillStatusRecordedVote
    {
        public string FullActionName { get; set; }
        public DateTime? Date { get; set; }
        public string RollNumber { get; set; }
        public string Congress { get; set; }
        public string Chamber { get; set; }
        public Uri URL { get; set; }
        public string SessionNumber { get; set; }
    }

    public class BillStatusCommittee
    {
        public string Chamber { get; set; }
        public string Name { get; set; }
        public string SystemCode { get; set; }
        public string Type { get; set; }
        public IReadOnlyCollection<CommitteeActivity> Activities { get; set; }
        public IReadOnlyCollection<Subcommittee> Subcommittees { get; set; }
    }

    public class Subcommittee
    {
        public string Name { get; set; }
        public string SystemCode { get; set; }
        public IReadOnlyCollection<SubcommitteeActivity> Activities { get; set; }
    }

    public class SubcommitteeActivity
    {
        public DateTime? Date { get; set; }
        public string Name { get; set; }
    }

    public class CommitteeActivity
    {
        public DateTime? Date { get; set; }
        public string Name { get; set; }

        // what is this?
        //public IReadOnlyCollection<CommitteeActivityReport> Reports { get; set; }
    }

    public class CommitteeActivityReport
    {
        public string Citation { get; set; }
    }

    public class BillStatusSponsor
    {
        public string FullName { get; set; }
        public string ByRequestType { get; set; }
        public string LastName { get; set; }
        public string Party { get; set; }
        public string BioguideId { get; set; }
        public Identifiers Identifiers { get; set; }
        public string MiddleName { get; set; }
        public string District { get; set; }
        public string State { get; set; }
        public string FirstName { get; set; }
    }

    public class Identifiers
    {
        public string LISID { get; set; }
        public string GPOID { get; set; }
        public string BioguideId { get; set; }
    }

    public class BillStatusSubject
    {
        public IReadOnlyCollection<LegislativeSubject> LegislativeSubjects { get; set; }
        public PolicyArea PolicyArea { get; set; }
    }

    public class PolicyArea
    {
        public string Name { get; set; }
    }

    public class LegislativeSubject
    {
        public string Name { get; set; }
    }

    public class BillStatusAction
    {
        public SourceSystem SourceSystem { get; set; }
        public BillActionCommittee Committee { get; set; }
        public string Text { get; set; }
        public DateTime? ActionDate { get; set; } // TODO: combine Time+Date
        public string Type { get; set; }
        public IReadOnlyCollection<Link> Links { get; set; }
        public string ActionCode { get; set; }
    }

    public class BillActionCommittee
    {
        public string Name { get; set; }
        public string SystemCode { get; set; }
    }

    public class Link
    {
        public string Name { get; set; }
        public Uri URL { get; set; }
    }

    public class SourceSystem
    {
        public string Name { get; set; }
        public string Code { get; set; }
    }

    public class BillStatusAmendment
    {
        public string AmendmentId { get { return String.Format("{0}{1}-{2}", Type, Number, Congress); } }
        public string Number { get; set; }
        public string Congress { get; set; }
        public string Description { get; set; }
        public string Purpose { get; set; }
        public string Chamber { get; set; }
        public string Type { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? SubmittedDate { get; set; }
        public DateTime? ProposedDate { get; set; }
        public LatestAction LatestAction { get; set; }
        public AmendedAmendment AmendedAmendment { get; set; }
        public AmendedBill AmendedBill { get; set; }
        public BillAmendmentCosponsor Cosponsors { get; set; }
        public IReadOnlyCollection<BillStatusTitle> Titles { get; set; }
        public IReadOnlyCollection<BillAmendmentSponsor> Sponsors { get; set; }
        public IReadOnlyCollection<BillStatusNote> Notes { get; set; }
        public IReadOnlyCollection<BillAmendmentAction> Actions { get; set; }
        public IReadOnlyCollection<Link> Links { get; set; }
    }

    public class AmendedAmendment
    {
        public string Purpose { get; set; }
        public string Number { get; set; }
        public string Congress { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
    }

    public class BillStatusLaw
    {
        public string Number { get; set; }
        public string Type { get; set; }
    }

    public class BillStatusCommitteeReport
    {
        public string Citation { get; set; }
    }

    public class BillStatusCBOCostEstimate
    {
        public DateTime? PublishedDate { get; set; }
        public Uri URL { get; set; }
        public string Title { get; set; }
    }

    public class BillAmendmentAction
    {
        public DateTime? ActionDate { get; set; }
        public string Text { get; set; }
        public string ActionCode { get; set; }
        public SourceSystem SourceSystem { get; set; }
        public string Type { get; set; }
        public IReadOnlyCollection<Link> Links { get; set; }
    }

    public class BillAmendmentCosponsor
    {
        public int TotalCount { get; set; }
        public int CurrentCount { get; set; }
    }

    public class BillAmendmentSponsor
    {
        public string Name { get; set; }
        public string State { get; set; }
        public string BioguideId { get; set; }
        public string Party { get; set; }
        public string MiddleName { get; set; }
        public string FullName { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
    }

    public class AmendedBill
    {
        public string AmendmentId { get { return String.Format("{0}{1}-{2}", Type, Number, Congress); } }
        public string Congress { get; set; }
        public string Number { get; set; }
        public string OriginChamber { get; set; }
        public string OriginChamberCode { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
    }
}