using System;
using System.Collections.Generic;

namespace CongressCollector.Models.Cleaned
{
    public class Bill
    {
        public string BillNumber { get; set; }
        public string Congress { get; set; }
        public string Title { get; set; }
        public string ConstitutionalAuthorityStatement { get; set; }
        public DateTime? UpdateDate { get; set; }
        public BillAction LatestAction { get; set; }
        public string BillType { get; set; }
        public string OriginChamber { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? IntroducedDate { get; set; }
        public PolicyArea PolicyArea { get; set; }
        public IReadOnlyCollection<BillAction> Actions { get; set; }
        public IReadOnlyCollection<BillAmendment> Amendments { get; set; }
        public IReadOnlyCollection<CBOCostEstimate> CBOCostEstimates { get; set; }
        public IReadOnlyCollection<CommitteeReport> CommitteeReports { get; set; }
        public IReadOnlyCollection<Law> Laws { get; set; }
        public IReadOnlyCollection<BillSubject> Subjects { get; set; }
        public IReadOnlyCollection<BillSponsor> Sponsors { get; set; }
        public IReadOnlyCollection<Committee> Committees { get; set; }
        public IReadOnlyCollection<BillNote> Notes { get; set; }
        public IReadOnlyCollection<RecordedVote> RecordedVotes { get; set; }
        public IReadOnlyCollection<BillCosponsor> Cosponsors { get; set; }
        public IReadOnlyCollection<RelatedBill> RelatedBills { get; set; }
        public IReadOnlyCollection<BillSummary> BillSummaries { get; set; }
        public IReadOnlyCollection<BillTitle> Titles { get; set; }
        public IReadOnlyCollection<CalendarNumber> CalendarNumbers { get; set; }
    }

    public class BillNote
    {
        public string Text { get; set; }
        public IReadOnlyCollection<Link> Links { get; set; }
    }

    public class CalendarNumber
    {
        public string Calendar { get; set; }
        public string Number { get; set; }
    }

    public class BillTitle
    {
        public string ChamberCode { get; set; }
        public string ChamberName { get; set; }
        public string ParentTitleType { get; set; }
        public string Title { get; set; }
        public string TitleType { get; set; }
    }

    public class BillSummary
    {
        public DateTime ActionDate { get; set; }
        public string ActionDescription { get; set; }
        public string Text { get; set; }
        public DateTime LastSummaryUpdateDate { get; set; }
        public string Name { get; set; }
        public DateTime UpdateDate { get; set; }
        public string VersionCode { get; set; }
    }

    public class RelatedBill
    {
        public string LatestTitle { get; set; }
        public int Number { get; set; }
        public string Type { get; set; }
        public int Congress { get; set; }
        public IReadOnlyCollection<RelatedBillAction> Actions { get; set; }
        public IReadOnlyCollection<RelatedBillRelationshipDetail> RelationshipDetails { get; set; }
    }

    public class RelatedBillAction
    {
        public DateTime Date { get; set; } // combine time+date
        public string Text { get; set; }
    }

    public class RelatedBillRelationshipDetail
    {
        public string Type { get; set; }
        public string IdentifiedBy { get; set; }
    }

    public class BillCosponsor
    {
        public string Party { get; set; }
        public int District { get; set; }
        public string MiddleName { get; set; }
        public string BioGuideId { get; set; }
        public DateTime SponsorshipDate { get; set; }
        public bool IsOriginalCosponsor { get; set; }
        public string FirstName { get; set; }
        public string FullName { get; set; }
        public DateTime SponsorshipWithdrawnDate { get; set; }
        public IReadOnlyCollection<Identifier> Identifiers { get; set; }
        public string LastName { get; set; }
        public string State { get; set; }
    }

    public class RecordedVote
    {
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public int RollNumber { get; set; }
        public int Congress { get; set; }
        public string Chamber { get; set; }
        public Uri URL { get; set; }
        public int SessionNumber { get; set; }
    }

    public class Committee
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
        public DateTime Date { get; set; }
        public string Name { get; set; }
    }

    public class CommitteeActivity
    {
        public DateTime Date { get; set; }
        public string Name { get; set; }
        public IReadOnlyCollection<CommitteeActivityReport> Reports { get; set; }
    }

    public class CommitteeActivityReport
    {
        public string Citation { get; set; }
    }

    public class BillSponsor
    {
        public string FullName { get; set; }
        public string ByRequestType { get; set; }
        public string LastName { get; set; }
        public string Party { get; set; }
        public string BioGuideId { get; set; }
        public IReadOnlyCollection<Identifier> Identifiers { get; set; }
        public string MiddleName { get; set; }
        public int District { get; set; }
        public string State { get; set; }
        public string FirstName { get; set; }
    }

    public class Identifier
    {
        public int LISID { get; set; }
        public int GPOID { get; set; }
        public string BioGuideId { get; set; }
    }

    public class BillSubject
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

    public class BillAction
    {
        public SourceSystem SourceSystem { get; set; }
        public BillActionCommittee Committee { get; set; }
        public string Text { get; set; }
        public DateTime? DateTime { get; set; } // TODO: combine Time+Date
        public string Type { get; set; }
        public IReadOnlyCollection<Link> Links { get; set; }
        public string Code { get; set; }
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

    public class BillAmendment
    {
        public string Description { get; set; }
        public BillAmendmentAction LatestAction { get; set; }
        public string Congress { get; set; }
        public string Number { get; set; }
        public IReadOnlyCollection<BillTitle> Titles { get; set; }
        public IReadOnlyCollection<BillAmendmentCosponsor> Cosponsors { get; set; }
        public IReadOnlyCollection<BillAmendmentSponsor> Sponsors { get; set; }
        public string Purpose { get; set; }
        public string Chamber { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? SubmittedDate { get; set; }
        public string AmendmentType { get; set; } // TODO: enum?
        public IReadOnlyCollection<BillNote> Notes { get; set; }
        public DateTime? ProposedDate { get; set; }
        public IReadOnlyCollection<BillAmendmentAction> Actions { get; set; }
        public AmendedAmendment AmendedAmendment { get; set; }
        public IReadOnlyCollection<Link> Links { get; set; }
        public AmendedBill AmendedBill { get; set; }
    }

    public class AmendedAmendment
    {
        public string Purpose { get; set; }
        public string Number { get; set; }
        public string Congress { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
    }

    public class Law
    {
        public string Number { get; set; }
        public string Type { get; set; }
    }

    public class CommitteeReport
    {
        public string Citation { get; set; }
    }

    public class CBOCostEstimate
    {
        public DateTime PubDate { get; set; }
        public Uri URL { get; set; }
        public string Title { get; set; }
    }

    public class BillAmendmentAction
    {
        public DateTime? ActionDate { get; set; }
        public string Text { get; set; }
        public string ActionCode { get; set; }
        public SourceSystem SourceSystem { get; set; }
        public string ActionType { get; set; }
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
        public string BioGuideId { get; set; }
        public string Party { get; set; }
        public string MiddleName { get; set; }
        public string FullName { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
    }

    public class AmendedBill
    {
        public int Congress { get; set; }
        public int Number { get; set; }
        public string OriginChamber { get; set; }
        public string OriginChamberCode { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
    }
}