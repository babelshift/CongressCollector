using AutoMapper;
using CongressCollector.Models.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

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
                    x.CreateMap<Original.AmendedBill, Cleaned.AmendedBill>();

                    x.CreateMap<Original.AmendedAmendment, Cleaned.AmendedAmendment>();

                    x.CreateMap<Original.Committee, Cleaned.BillActionCommittee>();

                    x.CreateMap<Original.SourceSystem, Cleaned.SourceSystem>();

                    x.CreateMap<Original.CommitteeReport, Cleaned.CommitteeReport>();

                    x.CreateMap<Original.Item, Cleaned.CalendarNumber>();

                    x.CreateMap<Original.Item, Cleaned.BillAmendmentSponsor>();

                    x.CreateMap<Original.Item, Cleaned.BillTitle>();
                    
                    x.CreateMap<Original.Identifiers, Cleaned.Identifiers>();

                    x.CreateMap<Original.PolicyArea, Cleaned.PolicyArea>();

                    x.CreateMap<Original.Item, Cleaned.BillCosponsor>()
                        .ForMember(dest => dest.IsOriginalCosponsor,
                        opts => opts.MapFrom(src => ParseHelpers.ParseNullableBoolean(src.IsOriginalCosponsor)))
                        .ForMember(dest => dest.SponsorshipDate,
                        opts => opts.MapFrom(src => ParseHelpers.ParseNullableDateTime(src.SponsorshipDate)))
                        .ForMember(dest => dest.SponsorshipWithdrawnDate,
                        opts => opts.MapFrom(src => ParseHelpers.ParseNullableDateTime(src.SponsorshipWithdrawnDate)));

                    x.CreateMap<Original.Cosponsors, Cleaned.BillAmendmentCosponsor>();

                    x.CreateMap<Original.RecordedVotes, IReadOnlyCollection<Cleaned.RecordedVote>>().ConvertUsing<RecordedVotesTypeConverter>();

                    x.CreateMap<Original.Links, IReadOnlyCollection<Cleaned.Link>>().ConvertUsing<LinksTypeConverter>();

                    x.CreateMap<Original.Notes, IReadOnlyCollection<Cleaned.BillNote>>().ConvertUsing<NotesTypeConverter>();

                    x.CreateMap<Original.Actions, IReadOnlyCollection<Cleaned.BillAmendmentAction>>().ConvertUsing<BillAmendmentActionsTypeConverter>();

                    x.CreateMap<Original.Sponsors, IReadOnlyCollection<Cleaned.BillAmendmentSponsor>>().ConvertUsing<BillAmentmentSponsorsTypeConverter>();
                    
                    x.CreateMap<Original.Titles, IReadOnlyCollection<Cleaned.BillTitle>>().ConvertUsing<BillTitlesTypeConverter>();

                    x.CreateMap<Original.LatestAction, Cleaned.BillAmendmentAction>()
                        .ForMember(dest => dest.ActionDate,
                        opts => opts.MapFrom(src => ParseHelpers.ParseNullableDateTime(src.ActionDate, src.ActionTime)));

                    x.CreateMap<Original.LatestAction, Cleaned.BillAction>()
                        .ForMember(dest => dest.ActionDate,
                        opts => opts.MapFrom(src => ParseHelpers.ParseNullableDateTime(src.ActionDate, src.ActionTime)));

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

                    x.CreateMap<Original.Bill, Cleaned.Bill>().ConvertUsing<BillTypeConverter>();

                    x.CreateMap<Original.RelationshipDetails, IReadOnlyCollection<Cleaned.RelationshipDetail>>().ConvertUsing<RelationshipDetailsTypeConverter>();

                    x.CreateMap<Original.Item, Cleaned.RelatedBill>()
                        .ForMember(dest => dest.RelationshipDetails,
                        opts => opts.MapFrom(src => src.RelationshipDetails));

                    x.CreateMap<Original.Item, Cleaned.RelationshipDetail>();

                    x.CreateMap<Original.Item, Cleaned.Law>();

                    x.CreateMap<Original.Item, Cleaned.BillSponsor>();

                    x.CreateMap<Original.Subjects, Cleaned.BillSubject>().ConvertUsing<SubjectTypeConverter>();

                    x.CreateMap<Original.Item, Cleaned.BillTitle>();
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