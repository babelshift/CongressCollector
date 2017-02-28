using AutoMapper;
using CongressCollector.Models.Utilities;
using System;
using System.Collections.Generic;

namespace CongressCollector.Models
{
    internal static class AutoMapperConfiguration
    {
        private static bool isInitialized = false;
        private static MapperConfiguration config;
        private static IMapper mapper;

        public static IMapper Mapper { get { return mapper; } }

        public static void Initialize()
        {
            if (isInitialized)
            {
                return;
            }

            if (config == null)
            {
                config = new MapperConfiguration(x =>
                {
                    #region Itemized Collection Mappings

                    x.CreateMap<Original.Titles, IReadOnlyCollection<Cleaned.BillTitle>>()
                        .ConvertUsing<ItemizedTypeConverter<Original.Titles, Cleaned.BillTitle, Original.Item>>();

                    x.CreateMap<Original.Sponsors, IReadOnlyCollection<Cleaned.BillSponsor>>()
                        .ConvertUsing<ItemizedTypeConverter<Original.Sponsors, Cleaned.BillSponsor, Original.Item>>();

                    x.CreateMap<Original.Sponsors, IReadOnlyCollection<Cleaned.BillAmendmentSponsor>>()
                        .ConvertUsing<ItemizedTypeConverter<Original.Sponsors, Cleaned.BillAmendmentSponsor, Original.Item>>();

                    x.CreateMap<Original.RelatedBills, IReadOnlyCollection<Cleaned.RelatedBill>>()
                        .ConvertUsing<ItemizedTypeConverter<Original.RelatedBills, Cleaned.RelatedBill, Original.Item>>();

                    x.CreateMap<Original.Notes, IReadOnlyCollection<Cleaned.BillNote>>()
                        .ConvertUsing<ItemizedTypeConverter<Original.Notes, Cleaned.BillNote, Original.Item>>();

                    x.CreateMap<Original.Laws, IReadOnlyCollection<Cleaned.BillLaw>>()
                        .ConvertUsing<ItemizedTypeConverter<Original.Laws, Cleaned.BillLaw, Original.Item>>();

                    x.CreateMap<Original.Cosponsors, IReadOnlyCollection<Cleaned.BillCosponsor>>()
                        .ConvertUsing<ItemizedTypeConverter<Original.Cosponsors, Cleaned.BillCosponsor, Original.Item>>();

                    x.CreateMap<Original.BillCommittees, IReadOnlyCollection<Cleaned.Committee>>()
                        .ConvertUsing<ItemizedTypeConverter<Original.BillCommittees, Cleaned.Committee, Original.Item>>();

                    x.CreateMap<Original.CboCostEstimates, IReadOnlyCollection<Cleaned.CBOCostEstimate>>()
                        .ConvertUsing<ItemizedTypeConverter<Original.CboCostEstimates, Cleaned.CBOCostEstimate, Original.Item>>();

                    x.CreateMap<Original.CalendarNumbers, IReadOnlyCollection<Cleaned.CalendarNumber>>()
                        .ConvertUsing<ItemizedTypeConverter<Original.CalendarNumbers, Cleaned.CalendarNumber, Original.Item>>();

                    x.CreateMap<Original.Actions, IReadOnlyCollection<Cleaned.BillAction>>()
                        .ConvertUsing<ItemizedTypeConverter<Original.Actions, Cleaned.BillAction, Original.Item>>();

                    x.CreateMap<Original.Actions, IReadOnlyCollection<Cleaned.BillAmendmentAction>>()
                        .ConvertUsing(src =>
                        {
                            if (src.InnerActions == null)
                            {
                                return null;
                            }

                            return mapper.Map<Original.Actions, IReadOnlyCollection<Cleaned.BillAmendmentAction>>(src.InnerActions);
                        });

                    x.CreateMap<Original.Links, IReadOnlyCollection<Cleaned.Link>>()
                        .ConvertUsing<ItemizedTypeConverter<Original.Links, Cleaned.Link, Original.Link>>();

                    x.CreateMap<Original.Amendments, IReadOnlyCollection<Cleaned.BillAmendment>>()
                        .ConvertUsing<ItemizedTypeConverter<Original.Amendments, Cleaned.BillAmendment, Original.Amendment>>();

                    x.CreateMap<Original.LegislativeSubjects, IReadOnlyCollection<Cleaned.LegislativeSubject>>()
                        .ConvertUsing<ItemizedTypeConverter<Original.LegislativeSubjects, Cleaned.LegislativeSubject, Original.Item>>();

                    x.CreateMap<Original.Activities, IReadOnlyCollection<Cleaned.CommitteeActivity>>()
                        .ConvertUsing<ItemizedTypeConverter<Original.Activities, Cleaned.CommitteeActivity, Original.Item>>();

                    x.CreateMap<Original.Activities, IReadOnlyCollection<Cleaned.SubcommitteeActivity>>()
                        .ConvertUsing<ItemizedTypeConverter<Original.Activities, Cleaned.SubcommitteeActivity, Original.Item>>();

                    x.CreateMap<Original.Subcommittees, IReadOnlyCollection<Cleaned.Subcommittee>>()
                        .ConvertUsing<ItemizedTypeConverter<Original.Subcommittees, Cleaned.Subcommittee, Original.Item>>();

                    x.CreateMap<Original.BillCommittees, IReadOnlyCollection<Cleaned.Committee>>()
                        .ConvertUsing<ItemizedTypeConverter<Original.BillCommittees, Cleaned.Committee, Original.Item>>();

                    x.CreateMap<Original.BillSummaries, IReadOnlyCollection<Cleaned.BillSummary>>()
                        .ConvertUsing<ItemizedTypeConverter<Original.BillSummaries, Cleaned.BillSummary, Original.Item>>();

                    x.CreateMap<Original.Notes, IReadOnlyCollection<Cleaned.BillNote>>()
                        .ConvertUsing<ItemizedTypeConverter<Original.Notes, Cleaned.BillNote, Original.Item>>();

                    x.CreateMap<Original.CommitteeReports, IReadOnlyCollection<Cleaned.CommitteeReport>>()
                        .ConvertUsing<ItemizedTypeConverter<Original.CommitteeReports, Cleaned.CommitteeReport, Original.CommitteeReport>>();

                    x.CreateMap<Original.Committees, IReadOnlyCollection<Cleaned.Committee>>()
                        .ConvertUsing(src =>
                        {
                            return mapper.Map<Original.BillCommittees, IReadOnlyCollection<Cleaned.Committee>>(src.BillCommittees);
                        });

                    x.CreateMap<Original.Summaries, IReadOnlyCollection<Cleaned.BillSummary>>()
                        .ConvertUsing(src =>
                        {
                            return mapper.Map<Original.BillSummaries, IReadOnlyCollection<Cleaned.BillSummary>>(src.BillSummaries);
                        });

                    x.CreateMap<Original.RecordedVotes, IReadOnlyCollection<Cleaned.RecordedVote>>()
                        .ConvertUsing<ItemizedTypeConverter<Original.RecordedVotes, Cleaned.RecordedVote, Original.RecordedVote>>();

                    x.CreateMap<Original.RelationshipDetails, IReadOnlyCollection<Cleaned.RelationshipDetail>>()
                        .ConvertUsing<ItemizedTypeConverter<Original.RelationshipDetails, Cleaned.RelationshipDetail, Original.Item>>();

                    #endregion Itemized Collection Mappings

                    #region Item Mappings

                    x.CreateMap<Original.Item, Cleaned.Subcommittee>();

                    x.CreateMap<Original.Item, Cleaned.LegislativeSubject>();

                    x.CreateMap<Original.Item, Cleaned.CalendarNumber>();

                    x.CreateMap<Original.Item, Cleaned.BillAmendmentSponsor>();

                    x.CreateMap<Original.Item, Cleaned.BillNote>();

                    x.CreateMap<Original.Item, Cleaned.BillSummary>()
                        .ForMember(dest => dest.ActionDate,
                        opts => opts.MapFrom(src => ParseHelpers.ParseNullableDateTime(src.ActionDate)))
                        .ForMember(dest => dest.UpdateDate,
                        opts => opts.MapFrom(src => ParseHelpers.ParseNullableDateTime(src.UpdateDate)))
                        .ForMember(dest => dest.LastSummaryUpdateDate,
                        opts => opts.MapFrom(src => ParseHelpers.ParseNullableDateTime(src.LastSummaryUpdateDate)))
                        .ForMember(dest => dest.ActionDescription,
                        opts => opts.MapFrom(src => src.ActionDesc));

                    x.CreateMap<Original.Item, Cleaned.BillCosponsor>()
                        .ForMember(dest => dest.IsOriginalCosponsor,
                        opts => opts.MapFrom(src => ParseHelpers.ParseNullableBoolean(src.IsOriginalCosponsor)))
                        .ForMember(dest => dest.SponsorshipDate,
                        opts => opts.MapFrom(src => ParseHelpers.ParseNullableDateTime(src.SponsorshipDate)))
                        .ForMember(dest => dest.SponsorshipWithdrawnDate,
                        opts => opts.MapFrom(src => ParseHelpers.ParseNullableDateTime(src.SponsorshipWithdrawnDate)));

                    x.CreateMap<Original.Item, Cleaned.BillAmendmentAction>()
                        .ForMember(dest => dest.ActionDate,
                        opts => opts.MapFrom(src => ParseHelpers.ParseNullableDateTime(src.ActionDate, src.ActionTime)));

                    x.CreateMap<Original.Item, Cleaned.BillAction>()
                        .ForMember(dest => dest.ActionDate,
                        opts => opts.MapFrom(src => ParseHelpers.ParseNullableDateTime(src.ActionDate, src.ActionTime)));

                    x.CreateMap<Original.Item, Cleaned.SubcommitteeActivity>()
                        .ForMember(dest => dest.Date,
                        opts => opts.MapFrom(src => ParseHelpers.ParseNullableDateTime(src.Date)));

                    x.CreateMap<Original.Item, Cleaned.CommitteeActivity>()
                        .ForMember(dest => dest.Date,
                        opts => opts.MapFrom(src => ParseHelpers.ParseNullableDateTime(src.Date)));

                    x.CreateMap<Original.Item, Cleaned.RelatedBill>()
                        .ForMember(dest => dest.RelationshipDetails,
                        opts => opts.MapFrom(src => src.RelationshipDetails));

                    x.CreateMap<Original.Item, Cleaned.RelationshipDetail>();

                    x.CreateMap<Original.Item, Cleaned.BillLaw>();

                    x.CreateMap<Original.Item, Cleaned.BillSponsor>();

                    x.CreateMap<Original.Item, Cleaned.BillTitle>();

                    x.CreateMap<Original.Item, Cleaned.Committee>();

                    x.CreateMap<Original.Item, Cleaned.CBOCostEstimate>()
                        .ForMember(dest => dest.PublishedDate,
                        opts => opts.MapFrom(src => ParseHelpers.ParseNullableDateTime(src.PubDate)))
                        .ForMember(dest => dest.URL,
                        opts => opts.MapFrom(src => new Uri(src.Url)));

                    x.CreateMap<Original.RecordedVote, Cleaned.RecordedVote>()
                        .ForMember(dest => dest.Date,
                        opts => opts.MapFrom(src => ParseHelpers.ParseNullableDateTime(src.Date)))
                        .ForMember(dest => dest.URL,
                        opts => opts.MapFrom(src => new Uri(src.Url)));

                    #endregion Item Mappings

                    x.CreateMap<Original.Link, Cleaned.Link>()
                        .ForMember(dest => dest.URL,
                        opts => opts.MapFrom(src => new Uri(src.Url)));

                    x.CreateMap<Original.Amendment, Cleaned.BillAmendment>()
                        .ForMember(dest => dest.Congress,
                        opts => opts.MapFrom(src => ParseHelpers.GetFirstStringOrEmpty(src.Congress)))
                        .ForMember(dest => dest.CreateDate,
                        opts => opts.MapFrom(src => ParseHelpers.ParseNullableDateTime(src.CreateDate)))
                        .ForMember(dest => dest.ProposedDate,
                        opts => opts.MapFrom(src => ParseHelpers.ParseNullableDateTime(src.ProposedDate)))
                        .ForMember(dest => dest.SubmittedDate,
                        opts => opts.MapFrom(src => ParseHelpers.ParseNullableDateTime(src.SubmittedDate)))
                        .ForMember(dest => dest.UpdateDate,
                        opts => opts.MapFrom(src => ParseHelpers.ParseNullableDateTime(src.UpdateDate)))
                        .ForMember(dest => dest.Number,
                        opts => opts.MapFrom(src => ParseHelpers.GetFirstStringOrEmpty(src.Number)))
                        .ForMember(dest => dest.Purpose,
                        opts => opts.MapFrom(src => ParseHelpers.GetFirstStringOrEmpty(src.Purpose)))
                        .ForMember(dest => dest.Description,
                        opts => opts.MapFrom(src => ParseHelpers.GetFirstStringOrEmpty(src.Description)))
                        .ForMember(dest => dest.AmendmentType,
                        opts => opts.MapFrom(src => ParseHelpers.GetFirstStringOrEmpty(src.Type)));

                    x.CreateMap<Original.AmendedBill, Cleaned.AmendedBill>();

                    x.CreateMap<Original.AmendedAmendment, Cleaned.AmendedAmendment>();

                    x.CreateMap<Original.Committee, Cleaned.BillActionCommittee>();

                    x.CreateMap<Original.SourceSystem, Cleaned.SourceSystem>();

                    x.CreateMap<Original.CommitteeReport, Cleaned.CommitteeReport>();

                    x.CreateMap<Original.Identifiers, Cleaned.Identifiers>();

                    x.CreateMap<Original.PolicyArea, Cleaned.PolicyArea>();

                    x.CreateMap<Original.Cosponsors, Cleaned.BillAmendmentCosponsor>();

                    x.CreateMap<Original.LatestAction, Cleaned.BillAmendmentAction>()
                        .ForMember(dest => dest.ActionDate,
                        opts => opts.MapFrom(src => ParseHelpers.ParseNullableDateTime(src.ActionDate, src.ActionTime)));

                    x.CreateMap<Original.LatestAction, Cleaned.BillAction>()
                        .ForMember(dest => dest.ActionDate,
                        opts => opts.MapFrom(src => ParseHelpers.ParseNullableDateTime(src.ActionDate, src.ActionTime)));

                    x.CreateMap<Original.Bill, Cleaned.Bill>().ConvertUsing<BillTypeConverter>();

                    x.CreateMap<Original.Subjects, Cleaned.BillSubject>()
                        .ForMember(dest => dest.LegislativeSubjects,
                        opts => opts.MapFrom(src =>
                            src.BillSubjects != null
                            && src.BillSubjects.LegislativeSubjects != null
                            && src.BillSubjects.LegislativeSubjects.Items != null
                            ? mapper.Map<Original.LegislativeSubjects, IReadOnlyCollection<Cleaned.LegislativeSubject>>
                                (src.BillSubjects.LegislativeSubjects)
                            : null))
                        .ForMember(dest => dest.PolicyArea,
                        opts => opts.MapFrom(src =>
                            src.BillSubjects != null
                            ? src.BillSubjects.PolicyArea
                            : null));
                });
            }

            if (mapper == null)
            {
                mapper = config.CreateMapper();
            }

            isInitialized = true;

#if DEBUG
            //config.AssertConfigurationIsValid();
#endif
        }

        public static void Reset()
        {
            config = null;
            mapper = null;
        }
    }
}