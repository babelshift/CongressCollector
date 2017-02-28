using System.Collections.Generic;
using System.Xml.Serialization;

namespace CongressCollector.Models.Original
{
    [XmlRoot(ElementName = "sourceSystem")]
    public class SourceSystem
    {
        [XmlElement(ElementName = "name")]
        public string Name { get; set; }

        [XmlElement(ElementName = "code")]
        public string Code { get; set; }
    }

    [XmlRoot(ElementName = "link")]
    public class Link
    {
        [XmlElement(ElementName = "name")]
        public string Name { get; set; }

        [XmlElement(ElementName = "url")]
        public string Url { get; set; }
    }

    [XmlRoot(ElementName = "links")]
    public class Links : IItemized<Link>
    {
        [XmlElement(ElementName = "link")]
        public List<Link> Items { get; set; }
    }

    [XmlRoot(ElementName = "item")]
    public class Item
    {
        [XmlElement(ElementName = "calendar")]
        public string Calendar { get; set; }

        [XmlElement(ElementName = "sourceSystem")]
        public SourceSystem SourceSystem { get; set; }

        [XmlElement(ElementName = "text")]
        public string Text { get; set; }

        [XmlElement(ElementName = "actionDate")]
        public string ActionDate { get; set; }

        [XmlElement(ElementName = "type")]
        public string Type { get; set; }

        [XmlElement(ElementName = "links")]
        public Links Links { get; set; }

        [XmlElement(ElementName = "actionCode")]
        public string ActionCode { get; set; }

        [XmlElement(ElementName = "actionTime")]
        public string ActionTime { get; set; }

        [XmlElement(ElementName = "committee")]
        public Committee Committee { get; set; }

        [XmlElement(ElementName = "name")]
        public string Name { get; set; }

        [XmlElement(ElementName = "state")]
        public string State { get; set; }

        [XmlElement(ElementName = "bioguideId")]
        public string BioguideId { get; set; }

        [XmlElement(ElementName = "party")]
        public string Party { get; set; }

        [XmlElement(ElementName = "middleName")]
        public string MiddleName { get; set; }

        [XmlElement(ElementName = "fullName")]
        public string FullName { get; set; }

        [XmlElement(ElementName = "lastName")]
        public string LastName { get; set; }

        [XmlElement(ElementName = "firstName")]
        public string FirstName { get; set; }

        [XmlElement(ElementName = "url")]
        public string Url { get; set; }

        [XmlElement(ElementName = "pubDate")]
        public string PubDate { get; set; }

        [XmlElement(ElementName = "title")]
        public string Title { get; set; }

        [XmlElement(ElementName = "number")]
        public string Number { get; set; }

        [XmlElement(ElementName = "byRequestType")]
        public string ByRequestType { get; set; }

        [XmlElement(ElementName = "identifiers")]
        public Identifiers Identifiers { get; set; }

        [XmlElement(ElementName = "district")]
        public string District { get; set; }

        [XmlElement(ElementName = "chamber")]
        public string Chamber { get; set; }

        [XmlElement(ElementName = "activities")]
        public Activities Activities { get; set; }

        [XmlElement(ElementName = "systemCode")]
        public string SystemCode { get; set; }

        [XmlElement(ElementName = "subcommittees")]
        public Subcommittees Subcommittees { get; set; }

        [XmlElement(ElementName = "sponsorshipDate")]
        public string SponsorshipDate { get; set; }

        [XmlElement(ElementName = "isOriginalCosponsor")]
        public string IsOriginalCosponsor { get; set; }

        [XmlElement(ElementName = "sponsorshipWithdrawnDate")]
        public string SponsorshipWithdrawnDate { get; set; }

        [XmlElement(ElementName = "latestTitle")]
        public string LatestTitle { get; set; }

        [XmlElement(ElementName = "relationshipDetails")]
        public RelationshipDetails RelationshipDetails { get; set; }

        [XmlElement(ElementName = "congress")]
        public string Congress { get; set; }

        [XmlElement(ElementName = "latestAction")]
        public LatestAction LatestAction { get; set; }

        [XmlElement(ElementName = "updateDate")]
        public string UpdateDate { get; set; }

        [XmlElement(ElementName = "actionDesc")]
        public string ActionDesc { get; set; }

        [XmlElement(ElementName = "versionCode")]
        public string VersionCode { get; set; }

        [XmlElement(ElementName = "lastSummaryUpdateDate")]
        public string LastSummaryUpdateDate { get; set; }

        [XmlElement(ElementName = "titleType")]
        public string TitleType { get; set; }

        [XmlElement(ElementName = "parentTitleType")]
        public string ParentTitleType { get; set; }

        [XmlElement(ElementName = "chamberCode")]
        public string ChamberCode { get; set; }

        [XmlElement(ElementName = "chamberName")]
        public string ChamberName { get; set; }

        [XmlElement(ElementName = "date")]
        public string Date { get; set; }

        [XmlElement(ElementName = "identifiedBy")]
        public string IdentifiedBy { get; set; }
    }

    [XmlRoot(ElementName = "committee")]
    public class Committee
    {
        [XmlElement(ElementName = "name")]
        public string Name { get; set; }

        [XmlElement(ElementName = "systemCode")]
        public string SystemCode { get; set; }
    }

    [XmlRoot(ElementName = "actionTypeCounts")]
    public class ActionTypeCounts
    {
        [XmlElement(ElementName = "measureLaidBeforeSenate")]
        public string MeasureLaidBeforeSenate { get; set; }

        [XmlElement(ElementName = "passageOfAMeasure")]
        public string PassageOfAMeasure { get; set; }

        [XmlElement(ElementName = "ruleForConsiderationOfBillReportedToHouse")]
        public string RuleForConsiderationOfBillReportedToHouse { get; set; }

        [XmlElement(ElementName = "motionToWaiveMeasure")]
        public string MotionToWaiveMeasure { get; set; }

        [XmlElement(ElementName = "introducedInTheHouse")]
        public string IntroducedInTheHouse { get; set; }

        [XmlElement(ElementName = "motionToReconsiderResults")]
        public string MotionToReconsiderResults { get; set; }

        [XmlElement(ElementName = "passedAgreedToInHouse")]
        public string PassedAgreedToInHouse { get; set; }

        [XmlElement(ElementName = "pointOfOrderMeasure")]
        public string PointOfOrderMeasure { get; set; }

        [XmlElement(ElementName = "placeholderTextForE")]
        public string PlaceholderTextForE { get; set; }

        [XmlElement(ElementName = "presentedToPresident")]
        public string PresentedToPresident { get; set; }

        [XmlElement(ElementName = "introducedInSenate")]
        public string IntroducedInSenate { get; set; }

        [XmlElement(ElementName = "motionForPreviousQuestion")]
        public string MotionForPreviousQuestion { get; set; }

        [XmlElement(ElementName = "becamePublicLaw")]
        public string BecamePublicLaw { get; set; }

        [XmlElement(ElementName = "considerationByHouse")]
        public string ConsiderationByHouse { get; set; }

        [XmlElement(ElementName = "passedAgreedToInSenate")]
        public string PassedAgreedToInSenate { get; set; }

        [XmlElement(ElementName = "introducedInHouse")]
        public string IntroducedInHouse { get; set; }

        [XmlElement(ElementName = "placeholderTextForH")]
        public string PlaceholderTextForH { get; set; }

        [XmlElement(ElementName = "generalDebate")]
        public string GeneralDebate { get; set; }

        [XmlElement(ElementName = "placeholderTextForHL")]
        public string PlaceholderTextForHL { get; set; }

        [XmlElement(ElementName = "passedSenate")]
        public string PassedSenate { get; set; }

        [XmlElement(ElementName = "billReferralsAggregate")]
        public string BillReferralsAggregate { get; set; }

        [XmlElement(ElementName = "sentToHouse")]
        public string SentToHouse { get; set; }

        [XmlElement(ElementName = "billReferrals")]
        public string BillReferrals { get; set; }

        [XmlElement(ElementName = "houseAmendmentOffered")]
        public string HouseAmendmentOffered { get; set; }

        [XmlElement(ElementName = "pointOfOrderAmendment")]
        public string PointOfOrderAmendment { get; set; }

        [XmlElement(ElementName = "motionToWaiveAmendment")]
        public string MotionToWaiveAmendment { get; set; }

        [XmlElement(ElementName = "senateAmendmentProposedOnTheFloor")]
        public string SenateAmendmentProposedOnTheFloor { get; set; }

        [XmlElement(ElementName = "senateAmendmentSubmitted")]
        public string SenateAmendmentSubmitted { get; set; }

        [XmlElement(ElementName = "amendmentProposed")]
        public string AmendmentProposed { get; set; }

        [XmlElement(ElementName = "rollCallVotesOnAmendmentsInSenate")]
        public string RollCallVotesOnAmendmentsInSenate { get; set; }

        [XmlElement(ElementName = "rulingInSenate")]
        public string RulingInSenate { get; set; }

        [XmlElement(ElementName = "senateAmendmentNotAgreedTo")]
        public string SenateAmendmentNotAgreedTo { get; set; }

        [XmlElement(ElementName = "amendmentNotAgreedTo")]
        public string AmendmentNotAgreedTo { get; set; }
    }

    [XmlRoot(ElementName = "actionByCounts")]
    public class ActionByCounts
    {
        [XmlElement(ElementName = "senate")]
        public string Senate { get; set; }

        [XmlElement(ElementName = "houseOfRepresentatives")]
        public string HouseOfRepresentatives { get; set; }
    }

    [XmlRoot(ElementName = "actions")]
    public class Actions : IItemized<Item>
    {
        [XmlElement(ElementName = "item")]
        public List<Item> Items { get; set; }

        [XmlElement(ElementName = "actionTypeCounts")]
        public ActionTypeCounts ActionTypeCounts { get; set; }

        [XmlElement(ElementName = "actionByCounts")]
        public ActionByCounts ActionByCounts { get; set; }

        [XmlElement(ElementName = "count")]
        public string Count { get; set; }

        [XmlElement(ElementName = "actions")]
        public Actions InnerActions { get; set; }
    }

    [XmlRoot(ElementName = "cosponsors")]
    public class Cosponsors : IItemized<Item>
    {
        [XmlElement(ElementName = "totalCount")]
        public string TotalCount { get; set; }

        [XmlElement(ElementName = "currentCount")]
        public string CurrentCount { get; set; }

        [XmlElement(ElementName = "item")]
        public List<Item> Items { get; set; }
    }

    [XmlRoot(ElementName = "sponsors")]
    public class Sponsors : IItemized<Item>
    {
        [XmlElement(ElementName = "item")]
        public List<Item> Items { get; set; }
    }

    [XmlRoot(ElementName = "amendments")]
    public class Amendments : IItemized<Amendment>
    {
        [XmlElement(ElementName = "amendment")]
        public List<Amendment> Items { get; set; }
    }

    [XmlRoot(ElementName = "amendedBill")]
    public class AmendedBill
    {
        [XmlElement(ElementName = "originChamberCode")]
        public string OriginChamberCode { get; set; }

        [XmlElement(ElementName = "congress")]
        public string Congress { get; set; }

        [XmlElement(ElementName = "number")]
        public string Number { get; set; }

        [XmlElement(ElementName = "originChamber")]
        public string OriginChamber { get; set; }

        [XmlElement(ElementName = "title")]
        public string Title { get; set; }

        [XmlElement(ElementName = "type")]
        public string Type { get; set; }
    }

    [XmlRoot(ElementName = "amendment")]
    public class Amendment
    {
        [XmlElement(ElementName = "description")]
        public List<string> Description { get; set; }

        [XmlElement(ElementName = "congress")]
        public List<string> Congress { get; set; }

        [XmlElement(ElementName = "number")]
        public List<string> Number { get; set; }

        [XmlElement(ElementName = "titles")]
        public Titles Titles { get; set; }

        [XmlElement(ElementName = "cosponsors")]
        public Cosponsors Cosponsors { get; set; }

        [XmlElement(ElementName = "sponsors")]
        public Sponsors Sponsors { get; set; }

        [XmlElement(ElementName = "purpose")]
        public List<string> Purpose { get; set; }

        [XmlElement(ElementName = "updateDate")]
        public string UpdateDate { get; set; }

        [XmlElement(ElementName = "type")]
        public List<string> Type { get; set; }

        [XmlElement(ElementName = "notes")]
        public Notes Notes { get; set; }

        [XmlElement(ElementName = "proposedDate")]
        public string ProposedDate { get; set; }

        [XmlElement(ElementName = "amendments")]
        public Amendments Amendments { get; set; }

        [XmlElement(ElementName = "submittedDate")]
        public string SubmittedDate { get; set; }

        [XmlElement(ElementName = "createDate")]
        public string CreateDate { get; set; }

        [XmlElement(ElementName = "actions")]
        public Actions Actions { get; set; }

        [XmlElement(ElementName = "amendedAmendment")]
        public AmendedAmendment AmendedAmendment { get; set; }

        [XmlElement(ElementName = "links")]
        public Links Links { get; set; }

        [XmlElement(ElementName = "amendedBill")]
        public AmendedBill AmendedBill { get; set; }

        [XmlElement(ElementName = "chamber")]
        public string Chamber { get; set; }

        [XmlElement(ElementName = "latestAction")]
        public LatestAction LatestAction { get; set; }
    }

    [XmlRoot(ElementName = "amendedAmendment")]
    public class AmendedAmendment
    {
        [XmlElement(ElementName = "purpose")]
        public string Purpose { get; set; }

        [XmlElement(ElementName = "number")]
        public string Number { get; set; }

        [XmlElement(ElementName = "congress")]
        public string Congress { get; set; }

        [XmlElement(ElementName = "type")]
        public string Type { get; set; }

        [XmlElement(ElementName = "description")]
        public string Description { get; set; }
    }

    [XmlRoot(ElementName = "titles")]
    public class Titles : IItemized<Item>
    {
        [XmlElement(ElementName = "item")]
        public List<Item> Items { get; set; }
    }

    [XmlRoot(ElementName = "latestAction")]
    public class LatestAction
    {
        [XmlElement(ElementName = "actionDate")]
        public string ActionDate { get; set; }

        [XmlElement(ElementName = "text")]
        public string Text { get; set; }

        [XmlElement(ElementName = "links")]
        public Links Links { get; set; }

        [XmlElement(ElementName = "actionTime")]
        public string ActionTime { get; set; }
    }

    [XmlRoot(ElementName = "cboCostEstimates")]
    public class CboCostEstimates : IItemized<Item>
    {
        [XmlElement(ElementName = "item")]
        public List<Item> Items { get; set; }
    }

    [XmlRoot(ElementName = "laws")]
    public class Laws : IItemized<Item>
    {
        [XmlElement(ElementName = "item")]
        public List<Item> Items { get; set; }
    }

    [XmlRoot(ElementName = "legislativeSubjects")]
    public class LegislativeSubjects : IItemized<Item>
    {
        [XmlElement(ElementName = "item")]
        public List<Item> Items { get; set; }
    }

    [XmlRoot(ElementName = "policyArea")]
    public class PolicyArea
    {
        [XmlElement(ElementName = "name")]
        public string Name { get; set; }
    }

    [XmlRoot(ElementName = "billSubjects")]
    public class BillSubjects
    {
        [XmlElement(ElementName = "legislativeSubjects")]
        public LegislativeSubjects LegislativeSubjects { get; set; }

        [XmlElement(ElementName = "policyArea")]
        public PolicyArea PolicyArea { get; set; }
    }

    [XmlRoot(ElementName = "subjects")]
    public class Subjects
    {
        [XmlElement(ElementName = "billSubjects")]
        public BillSubjects BillSubjects { get; set; }
    }

    [XmlRoot(ElementName = "identifiers")]
    public class Identifiers
    {
        [XmlElement(ElementName = "lisID")]
        public string LISID { get; set; }

        [XmlElement(ElementName = "bioguideId")]
        public string BioguideId { get; set; }

        [XmlElement(ElementName = "gpoId")]
        public string GPOID { get; set; }
    }

    [XmlRoot(ElementName = "activities")]
    public class Activities : IItemized<Item>
    {
        [XmlElement(ElementName = "item")]
        public List<Item> Items { get; set; }
    }

    [XmlRoot(ElementName = "subcommittees")]
    public class Subcommittees : IItemized<Item>
    {
        [XmlElement(ElementName = "item")]
        public List<Item> Items { get; set; }
    }

    [XmlRoot(ElementName = "billCommittees")]
    public class BillCommittees : IItemized<Item>
    {
        [XmlElement(ElementName = "item")]
        public List<Item> Items { get; set; }
    }

    [XmlRoot(ElementName = "committees")]
    public class Committees
    {
        [XmlElement(ElementName = "billCommittees")]
        public BillCommittees BillCommittees { get; set; }
    }

    [XmlRoot(ElementName = "recordedVote")]
    public class RecordedVote
    {
        [XmlElement(ElementName = "fullActionName")]
        public string FullActionName { get; set; }

        [XmlElement(ElementName = "date")]
        public string Date { get; set; }

        [XmlElement(ElementName = "rollNumber")]
        public string RollNumber { get; set; }

        [XmlElement(ElementName = "congress")]
        public string Congress { get; set; }

        [XmlElement(ElementName = "chamber")]
        public string Chamber { get; set; }

        [XmlElement(ElementName = "url")]
        public string Url { get; set; }

        [XmlElement(ElementName = "sessionNumber")]
        public string SessionNumber { get; set; }
    }

    [XmlRoot(ElementName = "recordedVotes")]
    public class RecordedVotes : IItemized<RecordedVote>
    {
        [XmlElement(ElementName = "recordedVote")]
        public List<RecordedVote> Items { get; set; }
    }

    [XmlRoot(ElementName = "relationshipDetails")]
    public class RelationshipDetails : IItemized<Item>
    {
        [XmlElement(ElementName = "item")]
        public List<Item> Items { get; set; }
    }

    [XmlRoot(ElementName = "relatedBills")]
    public class RelatedBills : IItemized<Item>
    {
        [XmlElement(ElementName = "item")]
        public List<Item> Items { get; set; }
    }

    [XmlRoot(ElementName = "billSummaries")]
    public class BillSummaries : IItemized<Item>
    {
        [XmlElement(ElementName = "item")]
        public List<Item> Items { get; set; }
    }

    [XmlRoot(ElementName = "summaries")]
    public class Summaries
    {
        [XmlElement(ElementName = "billSummaries")]
        public BillSummaries BillSummaries { get; set; }
    }

    [XmlRoot(ElementName = "bill")]
    public class Bill
    {
        [XmlElement(ElementName = "billNumber")]
        public string BillNumber { get; set; }

        [XmlElement(ElementName = "congress")]
        public string Congress { get; set; }

        [XmlElement(ElementName = "originChamber")]
        public string OriginChamber { get; set; }

        [XmlElement(ElementName = "constitutionalAuthorityStatementText")]
        public string ConstitutionalAuthorityStatementText { get; set; }

        [XmlElement(ElementName = "actions")]
        public Actions Actions { get; set; }

        [XmlElement(ElementName = "amendments")]
        public Amendments Amendments { get; set; }

        [XmlElement(ElementName = "cboCostEstimates")]
        public CboCostEstimates CboCostEstimates { get; set; }

        [XmlElement(ElementName = "committeeReports")]
        public CommitteeReports CommitteeReports { get; set; }

        [XmlElement(ElementName = "laws")]
        public Laws Laws { get; set; }

        [XmlElement(ElementName = "createDate")]
        public string CreateDate { get; set; }

        [XmlElement(ElementName = "latestAction")]
        public LatestAction LatestAction { get; set; }

        [XmlElement(ElementName = "subjects")]
        public Subjects Subjects { get; set; }

        [XmlElement(ElementName = "title")]
        public string Title { get; set; }

        [XmlElement(ElementName = "sponsors")]
        public Sponsors Sponsors { get; set; }

        [XmlElement(ElementName = "committees")]
        public Committees Committees { get; set; }

        [XmlElement(ElementName = "updateDate")]
        public string UpdateDate { get; set; }

        [XmlElement(ElementName = "notes")]
        public Notes Notes { get; set; }

        [XmlElement(ElementName = "recordedVotes")]
        public RecordedVotes RecordedVotes { get; set; }

        [XmlElement(ElementName = "billType")]
        public string BillType { get; set; }

        [XmlElement(ElementName = "cosponsors")]
        public Cosponsors Cosponsors { get; set; }

        [XmlElement(ElementName = "relatedBills")]
        public RelatedBills RelatedBills { get; set; }

        [XmlElement(ElementName = "summaries")]
        public Summaries Summaries { get; set; }

        [XmlElement(ElementName = "introducedDate")]
        public string IntroducedDate { get; set; }

        [XmlElement(ElementName = "titles")]
        public Titles Titles { get; set; }

        [XmlElement(ElementName = "policyArea")]
        public PolicyArea PolicyArea { get; set; }

        [XmlElement(ElementName = "calendarNumbers")]
        public CalendarNumbers CalendarNumbers { get; set; }
    }

    [XmlRoot(ElementName = "committeeReports")]
    public class CommitteeReports : IItemized<CommitteeReport>
    {
        [XmlElement(ElementName = "committeeReport")]
        public List<CommitteeReport> Items { get; set; }
    }

    [XmlRoot(ElementName = "committeeReport")]
    public class CommitteeReport
    {
        [XmlElement(ElementName = "citation")]
        public string Citation { get; set; }
    }

    [XmlRoot(ElementName = "notes")]
    public class Notes : IItemized<Item>
    {
        [XmlElement(ElementName = "item")]
        public List<Item> Items { get; set; }
    }

    [XmlRoot(ElementName = "calendarNumbers")]
    public class CalendarNumbers : IItemized<Item>
    {
        [XmlElement(ElementName = "item")]
        public List<Item> Items { get; set; }
    }

    [XmlRoot(ElementName = "dublinCore")]
    public class DublinCore
    {
        [XmlElement(ElementName = "format", Namespace = "http://purl.org/dc/elements/1.1/")]
        public string Format { get; set; }

        [XmlElement(ElementName = "language", Namespace = "http://purl.org/dc/elements/1.1/")]
        public string Language { get; set; }

        [XmlElement(ElementName = "rights", Namespace = "http://purl.org/dc/elements/1.1/")]
        public string Rights { get; set; }

        [XmlElement(ElementName = "contributor", Namespace = "http://purl.org/dc/elements/1.1/")]
        public string Contributor { get; set; }

        [XmlElement(ElementName = "description", Namespace = "http://purl.org/dc/elements/1.1/")]
        public string Description { get; set; }

        [XmlAttribute(AttributeName = "dc", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string Dc { get; set; }
    }

    [XmlRoot(ElementName = "billStatus")]
    public class BillStatus
    {
        [XmlElement(ElementName = "bill")]
        public Bill Bill { get; set; }

        [XmlElement(ElementName = "dublinCore")]
        public DublinCore DublinCore { get; set; }
    }
}