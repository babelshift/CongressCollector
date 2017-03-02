using AutoMapper;
using CongressCollector.Models.Converters;
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

                    x.CreateMap<Original.Titles, IReadOnlyCollection<Cleaned.BillStatusTitle>>()
                        .ConvertUsing<ItemizedTypeConverter<Original.Titles, Cleaned.BillStatusTitle, Original.Item>>();

                    x.CreateMap<Original.Sponsors, IReadOnlyCollection<Cleaned.BillStatusSponsor>>()
                        .ConvertUsing<ItemizedTypeConverter<Original.Sponsors, Cleaned.BillStatusSponsor, Original.Item>>();

                    x.CreateMap<Original.Sponsors, IReadOnlyCollection<Cleaned.BillAmendmentSponsor>>()
                        .ConvertUsing<ItemizedTypeConverter<Original.Sponsors, Cleaned.BillAmendmentSponsor, Original.Item>>();

                    x.CreateMap<Original.RelatedBills, IReadOnlyCollection<Cleaned.BillStatusRelatedBill>>()
                        .ConvertUsing<ItemizedTypeConverter<Original.RelatedBills, Cleaned.BillStatusRelatedBill, Original.Item>>();

                    x.CreateMap<Original.Notes, IReadOnlyCollection<Cleaned.BillStatusNote>>()
                        .ConvertUsing<ItemizedTypeConverter<Original.Notes, Cleaned.BillStatusNote, Original.Item>>();

                    x.CreateMap<Original.Laws, IReadOnlyCollection<Cleaned.BillStatusLaw>>()
                        .ConvertUsing<ItemizedTypeConverter<Original.Laws, Cleaned.BillStatusLaw, Original.Item>>();

                    x.CreateMap<Original.Cosponsors, IReadOnlyCollection<Cleaned.BillStatusCosponsor>>()
                        .ConvertUsing<ItemizedTypeConverter<Original.Cosponsors, Cleaned.BillStatusCosponsor, Original.Item>>();

                    x.CreateMap<Original.BillCommittees, IReadOnlyCollection<Cleaned.BillStatusCommittee>>()
                        .ConvertUsing<ItemizedTypeConverter<Original.BillCommittees, Cleaned.BillStatusCommittee, Original.Item>>();

                    x.CreateMap<Original.CboCostEstimates, IReadOnlyCollection<Cleaned.BillStatusCBOCostEstimate>>()
                        .ConvertUsing<ItemizedTypeConverter<Original.CboCostEstimates, Cleaned.BillStatusCBOCostEstimate, Original.Item>>();

                    x.CreateMap<Original.CalendarNumbers, IReadOnlyCollection<Cleaned.BillStatusCalendarNumber>>()
                        .ConvertUsing<ItemizedTypeConverter<Original.CalendarNumbers, Cleaned.BillStatusCalendarNumber, Original.Item>>();

                    x.CreateMap<Original.Actions, IReadOnlyCollection<Cleaned.BillStatusAction>>()
                        .ConvertUsing<ItemizedTypeConverter<Original.Actions, Cleaned.BillStatusAction, Original.Item>>();

                    x.CreateMap<Original.Actions, IReadOnlyCollection<Cleaned.BillAmendmentAction>>()
                        .ConvertUsing<ItemizedTypeConverter<Original.Actions, Cleaned.BillAmendmentAction, Original.Item>>();

                    x.CreateMap<Original.Links, IReadOnlyCollection<Cleaned.Link>>()
                        .ConvertUsing<ItemizedTypeConverter<Original.Links, Cleaned.Link, Original.Link>>();

                    x.CreateMap<Original.Amendments, IReadOnlyCollection<Cleaned.BillStatusAmendment>>()
                        .ConvertUsing<ItemizedTypeConverter<Original.Amendments, Cleaned.BillStatusAmendment, Original.Amendment>>();

                    x.CreateMap<Original.LegislativeSubjects, IReadOnlyCollection<Cleaned.LegislativeSubject>>()
                        .ConvertUsing<ItemizedTypeConverter<Original.LegislativeSubjects, Cleaned.LegislativeSubject, Original.Item>>();

                    x.CreateMap<Original.Activities, IReadOnlyCollection<Cleaned.CommitteeActivity>>()
                        .ConvertUsing<ItemizedTypeConverter<Original.Activities, Cleaned.CommitteeActivity, Original.Item>>();

                    x.CreateMap<Original.Activities, IReadOnlyCollection<Cleaned.SubcommitteeActivity>>()
                        .ConvertUsing<ItemizedTypeConverter<Original.Activities, Cleaned.SubcommitteeActivity, Original.Item>>();

                    x.CreateMap<Original.Subcommittees, IReadOnlyCollection<Cleaned.Subcommittee>>()
                        .ConvertUsing<ItemizedTypeConverter<Original.Subcommittees, Cleaned.Subcommittee, Original.Item>>();

                    x.CreateMap<Original.BillCommittees, IReadOnlyCollection<Cleaned.BillStatusCommittee>>()
                        .ConvertUsing<ItemizedTypeConverter<Original.BillCommittees, Cleaned.BillStatusCommittee, Original.Item>>();

                    // Bill summaries have HTML tags in the Text. We should get rid of this so that it doesn't conflict with downstream displays.
                    x.CreateMap<Original.BillSummaries, IReadOnlyCollection<Cleaned.BillStatusSummary>>()
                        .ConvertUsing<ItemizedTypeConverter<Original.BillSummaries, Cleaned.BillStatusSummary, Original.Item>>();

                    x.CreateMap<Original.Notes, IReadOnlyCollection<Cleaned.BillStatusNote>>()
                        .ConvertUsing<ItemizedTypeConverter<Original.Notes, Cleaned.BillStatusNote, Original.Item>>();

                    x.CreateMap<Original.CommitteeReports, IReadOnlyCollection<Cleaned.BillStatusCommitteeReport>>()
                        .ConvertUsing<ItemizedTypeConverter<Original.CommitteeReports, Cleaned.BillStatusCommitteeReport, Original.CommitteeReport>>();

                    x.CreateMap<Original.Committees, IReadOnlyCollection<Cleaned.BillStatusCommittee>>()
                        .ConvertUsing(src =>
                        {
                            return mapper.Map<Original.BillCommittees, IReadOnlyCollection<Cleaned.BillStatusCommittee>>(src.BillCommittees);
                        });

                    x.CreateMap<Original.Summaries, IReadOnlyCollection<Cleaned.BillStatusSummary>>()
                        .ConvertUsing(src =>
                        {
                            return mapper.Map<Original.BillSummaries, IReadOnlyCollection<Cleaned.BillStatusSummary>>(src.BillSummaries);
                        });

                    x.CreateMap<Original.RecordedVotes, IReadOnlyCollection<Cleaned.BillStatusRecordedVote>>()
                        .ConvertUsing<ItemizedTypeConverter<Original.RecordedVotes, Cleaned.BillStatusRecordedVote, Original.RecordedVote>>();

                    x.CreateMap<Original.RelationshipDetails, IReadOnlyCollection<Cleaned.RelationshipDetail>>()
                        .ConvertUsing<ItemizedTypeConverter<Original.RelationshipDetails, Cleaned.RelationshipDetail, Original.Item>>();

                    #endregion Itemized Collection Mappings

                    #region Item Mappings

                    x.CreateMap<Original.Item, Cleaned.Subcommittee>();

                    x.CreateMap<Original.Item, Cleaned.LegislativeSubject>();

                    x.CreateMap<Original.Item, Cleaned.BillStatusCalendarNumber>();

                    x.CreateMap<Original.Item, Cleaned.BillAmendmentSponsor>();

                    x.CreateMap<Original.Item, Cleaned.BillStatusNote>();

                    x.CreateMap<Original.Item, Cleaned.BillStatusSummary>()
                        .ForMember(dest => dest.ActionDate,
                        opts => opts.MapFrom(src => ParseHelpers.ParseNullableDateTime(src.ActionDate)))
                        .ForMember(dest => dest.UpdateDate,
                        opts => opts.MapFrom(src => ParseHelpers.ParseNullableDateTime(src.UpdateDate)))
                        .ForMember(dest => dest.LastSummaryUpdateDate,
                        opts => opts.MapFrom(src => ParseHelpers.ParseNullableDateTime(src.LastSummaryUpdateDate)))
                        .ForMember(dest => dest.ActionDescription,
                        opts => opts.MapFrom(src => src.ActionDesc))
                        .ForMember(dest => dest.Text,
                        opts => opts.MapFrom(src => ParseHelpers.ParseAndStripHTML(src.Text)));

                    x.CreateMap<Original.Item, Cleaned.BillStatusCosponsor>()
                        .ForMember(dest => dest.IsOriginalCosponsor,
                        opts => opts.MapFrom(src => ParseHelpers.ParseNullableBoolean(src.IsOriginalCosponsor)))
                        .ForMember(dest => dest.SponsorshipDate,
                        opts => opts.MapFrom(src => ParseHelpers.ParseNullableDateTime(src.SponsorshipDate)))
                        .ForMember(dest => dest.SponsorshipWithdrawnDate,
                        opts => opts.MapFrom(src => ParseHelpers.ParseNullableDateTime(src.SponsorshipWithdrawnDate)))
                        .ForMember(dest => dest.District,
                        opts => opts.MapFrom(src => ParseHelpers.ParseNullableInt(src.District)));

                    x.CreateMap<Original.Item, Cleaned.BillAmendmentAction>()
                        .ForMember(dest => dest.ActionDate,
                        opts => opts.MapFrom(src => ParseHelpers.ParseNullableDateTime(src.ActionDate, src.ActionTime)));

                    x.CreateMap<Original.Item, Cleaned.BillStatusAction>()
                        .ForMember(dest => dest.ActionDate,
                        opts => opts.MapFrom(src => ParseHelpers.ParseNullableDateTime(src.ActionDate, src.ActionTime)));

                    x.CreateMap<Original.Item, Cleaned.SubcommitteeActivity>()
                        .ForMember(dest => dest.Date,
                        opts => opts.MapFrom(src => ParseHelpers.ParseNullableDateTime(src.Date)));

                    x.CreateMap<Original.Item, Cleaned.CommitteeActivity>()
                        .ForMember(dest => dest.Date,
                        opts => opts.MapFrom(src => ParseHelpers.ParseNullableDateTime(src.Date)));

                    x.CreateMap<Original.Item, Cleaned.BillStatusRelatedBill>()
                        .ForMember(dest => dest.RelationshipDetails,
                        opts => opts.MapFrom(src => src.RelationshipDetails));

                    x.CreateMap<Original.Item, Cleaned.RelationshipDetail>();

                    x.CreateMap<Original.Item, Cleaned.BillStatusLaw>();

                    x.CreateMap<Original.Item, Cleaned.BillStatusSponsor>()
                        .ForMember(dest => dest.District,
                        opts => opts.MapFrom(src => ParseHelpers.ParseNullableInt(src.District)));

                    x.CreateMap<Original.Item, Cleaned.BillStatusTitle>();

                    x.CreateMap<Original.Item, Cleaned.BillStatusCommittee>();

                    x.CreateMap<Original.Item, Cleaned.BillStatusCBOCostEstimate>()
                        .ForMember(dest => dest.PublishedDate,
                        opts => opts.MapFrom(src => ParseHelpers.ParseNullableDateTime(src.PubDate)))
                        .ForMember(dest => dest.URL,
                        opts => opts.MapFrom(src => new Uri(src.Url)));

                    x.CreateMap<Original.RecordedVote, Cleaned.BillStatusRecordedVote>()
                        .ForMember(dest => dest.Date,
                        opts => opts.MapFrom(src => ParseHelpers.ParseNullableDateTime(src.Date)))
                        .ForMember(dest => dest.URL,
                        opts => opts.MapFrom(src => new Uri(src.Url)));

                    #endregion Item Mappings

                    x.CreateMap<Original.Link, Cleaned.Link>()
                        .ForMember(dest => dest.URL,
                        opts => opts.MapFrom(src => new Uri(src.Url)));

                    x.CreateMap<Original.Amendment, Cleaned.BillStatusAmendment>()
                        .ForMember(dest => dest.Actions,
                        opts => opts.MapFrom(src => src.Actions != null ? src.Actions.InnerActions : null))
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
                        .ForMember(dest => dest.Type,
                        opts => opts.MapFrom(src => ParseHelpers.GetFirstStringOrEmpty(src.Type)));

                    x.CreateMap<Original.AmendedBill, Cleaned.AmendedBill>();

                    x.CreateMap<Original.AmendedAmendment, Cleaned.AmendedAmendment>();

                    x.CreateMap<Original.Committee, Cleaned.BillActionCommittee>();

                    x.CreateMap<Original.SourceSystem, Cleaned.SourceSystem>();

                    x.CreateMap<Original.CommitteeReport, Cleaned.BillStatusCommitteeReport>();

                    x.CreateMap<Original.Identifiers, Cleaned.Identifiers>();

                    x.CreateMap<Original.PolicyArea, Cleaned.PolicyArea>();

                    x.CreateMap<Original.Cosponsors, Cleaned.BillAmendmentCosponsor>();

                    x.CreateMap<Original.LatestAction, Cleaned.LatestAction>()
                        .ForMember(dest => dest.ActionDate,
                        opts => opts.MapFrom(src => ParseHelpers.ParseNullableDateTime(src.ActionDate, src.ActionTime)));

                    x.CreateMap<Original.Bill, Cleaned.BillStatus>().ConvertUsing<BillStatusTypeConverter>();

                    x.CreateMap<Original.Subjects, Cleaned.BillStatusSubject>()
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