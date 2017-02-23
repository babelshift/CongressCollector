using System;
using System.Collections.Generic;
using AutoMapper;
using System.Linq;
using CongressCollector.Models.Cleaned;
using CongressCollector.Models.Original;

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
            bill.UpdateDate = ParseNullableDateTime(source.UpdateDate);
            bill.IntroducedDate = ParseNullableDateTime(source.IntroducedDate);
            bill.CreateDate = ParseNullableDateTime(source.CreateDate);
            bill.Actions = GetBillActions(source.Actions);
            bill.Amendments = GetBillAmendments(source.Amendments);

            return bill;
        }

        private IReadOnlyCollection<Cleaned.BillAmendment> GetBillAmendments(Original.Amendments amendments)
        {
            List<Cleaned.BillAmendment> billAmendments = new List<Cleaned.BillAmendment>();

            if (amendments != null)
            {
                foreach (var amendment in amendments.InnerAmendments)
                {
                    Cleaned.BillAmendment billAmendment = new Cleaned.BillAmendment()
                    {
                        Chamber = amendment.Chamber,
                        Congress = GetFirstStringOrEmpty(amendment.Congress),
                        CreateDate = ParseNullableDateTime(amendment.CreateDate),
                        ProposedDate = ParseNullableDateTime(amendment.ProposedDate),
                        SubmittedDate = ParseNullableDateTime(amendment.SubmittedDate),
                        UpdateDate = ParseNullableDateTime(amendment.UpdateDate),
                        Number = GetFirstStringOrEmpty(amendment.Number),
                        Purpose = GetFirstStringOrEmpty(amendment.Purpose),
                        Description = GetFirstStringOrEmpty(amendment.Description)
                    };

                    billAmendment.Actions = GetBillAmentmentActions(amendment);
                    billAmendment.AmendedAmendment = GetAmendedAmendment(amendment.AmendedAmendment);

                    billAmendments.Add(billAmendment);
                }
            }

            return billAmendments.AsReadOnly();
        }

        private Cleaned.AmendedAmendment GetAmendedAmendment(Original.AmendedAmendment amendedAmendment)
        {
            return amendedAmendment != null ? new Cleaned.AmendedAmendment()
            {
                Congress = amendedAmendment.Congress,
                Description = amendedAmendment.Description,
                Number = amendedAmendment.Number,
                Purpose = amendedAmendment.Purpose,
                Type = amendedAmendment.Type
            } : null;
        }

        private List<Cleaned.BillAmendmentAction> GetBillAmentmentActions(Original.Amendment amendment)
        {
            List<Cleaned.BillAmendmentAction> billAmendmentActions = new List<Cleaned.BillAmendmentAction>();
            foreach (var action in amendment.Actions.Items)
            {
                Cleaned.BillAmendmentAction billAmendmentAction = new Cleaned.BillAmendmentAction()
                {
                    ActionCode = action.ActionCode,
                    ActionType = action.Type,
                    Text = action.Text,
                    ActionDate = ParseNullableDateTime(action.ActionDate, action.ActionTime),
                    SourceSystem = GetItemSourceSystem(action.SourceSystem),
                    Links = GetItemLinks(action.Links)
                };
                billAmendmentActions.Add(billAmendmentAction);
            }

            return billAmendmentActions;
        }

        private static Cleaned.SourceSystem GetItemSourceSystem(Original.SourceSystem sourceSystem)
        {
            return sourceSystem != null ? new Cleaned.SourceSystem()
            {
                Code = sourceSystem.Code,
                Name = sourceSystem.Name
            } : null;
        }

        private static System.Collections.ObjectModel.ReadOnlyCollection<Cleaned.Link> GetItemLinks(Original.Links links)
        {
            return (links != null && links.Link != null)
            ? links.Link.Select(x => new Cleaned.Link()
            {
                Name = x.Name,
                URL = new Uri(x.Url) // might need to input validation/trycreate this
            }).ToList().AsReadOnly() : null;
        }

        private string GetFirstStringOrEmpty(IList<string> items)
        {
            return items != null && items.Count > 0 ? items[0] : String.Empty;
        }

        private IReadOnlyCollection<Cleaned.BillAction> GetBillActions(Original.Actions actions)
        {
            List<Cleaned.BillAction> billActions = new List<Cleaned.BillAction>();
            if (actions != null)
            {
                foreach (var action in actions.Items)
                {
                    Cleaned.BillAction billAction = new Cleaned.BillAction()
                    {
                        Text = action.Text,
                        Type = action.Type,
                        Code = action.ActionCode,
                        DateTime = ParseNullableDateTime(action.ActionDate, action.ActionTime),
                        Committee = GetBillActionCommittee(action.Committee),
                        SourceSystem = GetItemSourceSystem(action.SourceSystem),
                        Links = GetItemLinks(action.Links)
                    };

                    billAction.Text = action.Text;
                    billAction.Type = action.Type;
                    billAction.Code = action.ActionCode;
                    billAction.DateTime = ParseNullableDateTime(action.ActionDate, action.ActionTime);
                    billAction.Committee = GetBillActionCommittee(action.Committee);
                    billAction.SourceSystem = GetItemSourceSystem(action.SourceSystem);
                    billAction.Links = GetItemLinks(action.Links);

                    billActions.Add(billAction);
                }
            }
            return billActions.AsReadOnly();
        }

        private BillActionCommittee GetBillActionCommittee(Original.Committee committee)
        {
            return committee != null ? new BillActionCommittee()
            {
                Name = committee.Name,
                SystemCode = committee.SystemCode
            } : null;
        }

        private DateTime? ParseNullableDateTime(string date, string time)
        {
            if (String.IsNullOrWhiteSpace(date)) { return null; }

            if (time == null) { time = String.Empty; }

            string rawDateTime = String.Format("{0} {1}", date.Trim(), time.Trim());

            DateTime dateTime;
            if (DateTime.TryParse(rawDateTime, out dateTime))
            {
                return dateTime;
            }

            return null;
        }

        private DateTime? ParseNullableDateTime(string dateTime)
        {
            if (String.IsNullOrWhiteSpace(dateTime))
            {
                return null;
            }

            DateTime updateDate;

            if (DateTime.TryParse(dateTime, out updateDate))
            {
                return (DateTime?)updateDate;
            }

            return null;
        }
    }
}