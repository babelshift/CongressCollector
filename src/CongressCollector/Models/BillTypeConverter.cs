using System;
using System.Collections.Generic;
using AutoMapper;
using System.Linq;
using CongressCollector.Models.Cleaned;
using CongressCollector.Models.Original;
using CongressCollector.Models.Utilities;

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
            bill.Actions = GetBillActions(context, source.Actions);
            bill.Amendments = GetBillAmendments(context, source.Amendments);
            bill.BillSummaries = GetBillSummaries(source.Summaries);
            bill.CalendarNumbers = GetCalendarNumbers(context, source.CalendarNumbers);
            bill.CBOCostEstimates = GetCBOCostEstimates(source.CboCostEstimates);
            bill.CommitteeReports = GetCommitteeReports(context, source.CommitteeReports);
            bill.Committees = GetCommittees(context, source.Committees);

            return bill;
        }

        private IReadOnlyCollection<Cleaned.Committee> GetCommittees(ResolutionContext context, Committees committees)
        {
            List<Cleaned.Committee> billCommittees = new List<Cleaned.Committee>();

            if (committees != null && committees.BillCommittees != null && committees.BillCommittees.Items != null)
            {
                foreach (var committee in committees.BillCommittees.Items)
                {
                    Cleaned.Committee billCommittee = new Cleaned.Committee()
                    {
                        Name = committee.Name,
                        Chamber = committee.Chamber,
                        SystemCode = committee.SystemCode,
                        Type = committee.Type,
                        Activities = GetCommitteeActivities(context, committee.Activities),
                        Subcommittees = GetSubcommittees(context, committee.Subcommittees)
                    };
                }
            }

            return billCommittees.AsReadOnly();
        }

        private IReadOnlyCollection<Cleaned.Subcommittee> GetSubcommittees(ResolutionContext context, Original.Subcommittees subcommittees)
        {
            List<Cleaned.Subcommittee> billSubcommittees = new List<Cleaned.Subcommittee>();

            if (subcommittees != null && subcommittees.Items != null)
            {
                foreach (var subcommittee in subcommittees.Items)
                {
                    Cleaned.Subcommittee billSubcommittee = new Subcommittee()
                    {
                        Name = subcommittee.Name,
                        SystemCode = subcommittee.SystemCode,
                        Activities = GetSubcommitteeActivities(context, subcommittee.Activities)
                    };
                    billSubcommittees.Add(billSubcommittee);
                }
            }

            return billSubcommittees.AsReadOnly();
        }

        private IReadOnlyCollection<SubcommitteeActivity> GetSubcommitteeActivities(ResolutionContext context, Activities activities)
        {
            List<Cleaned.SubcommitteeActivity> subcommitteeActivities = new List<Cleaned.SubcommitteeActivity>();

            if (activities != null && activities.Items != null)
            {
                subcommitteeActivities = context.Mapper.Map<List<Original.Item>, List<Cleaned.SubcommitteeActivity>>(activities.Items);
            }

            return subcommitteeActivities.AsReadOnly();
        }

        private IReadOnlyCollection<Cleaned.CommitteeActivity> GetCommitteeActivities(ResolutionContext context, Original.Activities activities)
        {
            List<Cleaned.CommitteeActivity> billCommitteeActivities = new List<Cleaned.CommitteeActivity>();

            if (activities != null && activities.Items != null)
            {
                billCommitteeActivities = context.Mapper.Map<List<Original.Item>, List<Cleaned.CommitteeActivity>>(activities.Items);
            }

            return billCommitteeActivities.AsReadOnly();
        }

        private IReadOnlyCollection<Cleaned.CommitteeReport> GetCommitteeReports(ResolutionContext context, Original.CommitteeReports committeeReports)
        {
            List<Cleaned.CommitteeReport> billCommitteeReports = new List<Cleaned.CommitteeReport>();

            if (committeeReports != null && committeeReports.InnerCommitteeReports != null)
            {
                billCommitteeReports = context.Mapper.Map<List<Original.CommitteeReport>, List<Cleaned.CommitteeReport>>(committeeReports.InnerCommitteeReports);
            }

            return billCommitteeReports.AsReadOnly();
        }

        private IReadOnlyCollection<CBOCostEstimate> GetCBOCostEstimates(Original.CboCostEstimates cboCostEstimates)
        {
            List<Cleaned.CBOCostEstimate> billCboCostEstimates = new List<Cleaned.CBOCostEstimate>();

            if (cboCostEstimates != null && cboCostEstimates.Items != null)
            {
                foreach (var cboCostEstimate in cboCostEstimates.Items)
                {
                    Cleaned.CBOCostEstimate billCboCostEstimate = new Cleaned.CBOCostEstimate()
                    {
                        Title = cboCostEstimate.Title,
                        PublishedDate = ParseHelpers.ParseNullableDateTime(cboCostEstimate.PubDate),
                        URL = new Uri(cboCostEstimate.Url) // validate url first?
                    };

                    billCboCostEstimates.Add(billCboCostEstimate);
                }
            }

            return billCboCostEstimates.AsReadOnly();
        }

        private IReadOnlyCollection<Cleaned.CalendarNumber> GetCalendarNumbers(ResolutionContext context, Original.CalendarNumber calendarNumbers)
        {
            List<Cleaned.CalendarNumber> billCalendarNumbers = new List<Cleaned.CalendarNumber>();

            if (calendarNumbers != null && calendarNumbers.Items != null)
            {
                billCalendarNumbers = context.Mapper.Map<List<Original.Item>, List<Cleaned.CalendarNumber>>(calendarNumbers.Items);
            }

            return billCalendarNumbers.AsReadOnly();
        }

        private IReadOnlyCollection<BillSummary> GetBillSummaries(Original.Summaries summaries)
        {
            List<Cleaned.BillSummary> billSummaries = new List<Cleaned.BillSummary>();

            if (summaries != null && summaries.BillSummaries != null && summaries.BillSummaries.Items != null)
            {
                foreach (var summary in summaries.BillSummaries.Items)
                {
                    Cleaned.BillSummary billSummary = new BillSummary()
                    {
                        ActionDate = ParseHelpers.ParseNullableDateTime(summary.ActionDate),
                        UpdateDate = ParseHelpers.ParseNullableDateTime(summary.UpdateDate),
                        LastSummaryUpdateDate = ParseHelpers.ParseNullableDateTime(summary.LastSummaryUpdateDate),
                        ActionDescription = summary.ActionDesc,
                        Name = summary.Name,
                        Text = summary.Text,
                        VersionCode = summary.VersionCode
                    };

                    billSummaries.Add(billSummary);
                }
            }

            return billSummaries.AsReadOnly();
        }

        private IReadOnlyCollection<Cleaned.BillAmendment> GetBillAmendments(ResolutionContext context, Original.Amendments amendments)
        {
            List<Cleaned.BillAmendment> billAmendments = new List<Cleaned.BillAmendment>();

            if (amendments != null)
            {
                foreach (var amendment in amendments.InnerAmendments)
                {
                    Cleaned.BillAmendment billAmendment = new Cleaned.BillAmendment()
                    {
                        Chamber = amendment.Chamber,
                        Congress = ParseHelpers.GetFirstStringOrEmpty(amendment.Congress),
                        CreateDate = ParseHelpers.ParseNullableDateTime(amendment.CreateDate),
                        ProposedDate = ParseHelpers.ParseNullableDateTime(amendment.ProposedDate),
                        SubmittedDate = ParseHelpers.ParseNullableDateTime(amendment.SubmittedDate),
                        UpdateDate = ParseHelpers.ParseNullableDateTime(amendment.UpdateDate),
                        Number = ParseHelpers.GetFirstStringOrEmpty(amendment.Number),
                        Purpose = ParseHelpers.GetFirstStringOrEmpty(amendment.Purpose),
                        Description = ParseHelpers.GetFirstStringOrEmpty(amendment.Description),
                        AmendmentType = ParseHelpers.GetFirstStringOrEmpty(amendment.Type)
                    };

                    billAmendment.Actions = GetBillAmentmentActions(context, amendment.Actions);
                    billAmendment.AmendedAmendment = context.Mapper.Map<Original.AmendedAmendment, Cleaned.AmendedAmendment>(amendment.AmendedAmendment);
                    billAmendment.AmendedBill = context.Mapper.Map<Original.AmendedBill, Cleaned.AmendedBill>(amendment.AmendedBill);
                    billAmendment.Cosponsors = context.Mapper.Map<Original.Cosponsors, Cleaned.BillAmendmentCosponsor>(amendment.Cosponsors);
                    //billAmendment.LatestAction = 

                    billAmendments.Add(billAmendment);
                }
            }

            return billAmendments.AsReadOnly();
        }

        private IReadOnlyCollection<Cleaned.BillAmendmentAction> GetBillAmentmentActions(ResolutionContext context, Original.Actions actions)
        {
            List<Cleaned.BillAmendmentAction> billAmendmentActions = new List<Cleaned.BillAmendmentAction>();

            if (actions != null && actions.Items != null)
            {
                billAmendmentActions = context.Mapper.Map<List<Original.Item>, List<Cleaned.BillAmendmentAction>>(actions.Items);
            }

            return billAmendmentActions.AsReadOnly();
        }


        private IReadOnlyCollection<Cleaned.BillAction> GetBillActions(ResolutionContext context, Original.Actions actions)
        {
            List<Cleaned.BillAction> billActions = new List<Cleaned.BillAction>();

            if (actions != null && actions.Items != null)
            {
                billActions = context.Mapper.Map<List<Original.Item>, List<Cleaned.BillAction>>(actions.Items);
            }

            return billActions.AsReadOnly();
        }
    }
}