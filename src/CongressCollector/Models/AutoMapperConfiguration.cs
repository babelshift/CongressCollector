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

                    x.CreateMap<Original.Cosponsors, Cleaned.BillAmendmentCosponsor>();

                    x.CreateMap<Original.Links, IReadOnlyCollection<Cleaned.Link>>().ConvertUsing<LinksTypeConverter>();

                    x.CreateMap<Original.Notes, IReadOnlyCollection<Cleaned.BillNote>>().ConvertUsing<NotesTypeConverter>();

                    x.CreateMap<Original.Actions, IReadOnlyCollection<Cleaned.BillAmendmentAction>>().ConvertUsing<BillAmendmentActionsTypeConverter>();

                    x.CreateMap<Original.Sponsors, IReadOnlyCollection<Cleaned.BillAmendmentSponsor>>().ConvertUsing<BillAmentmentSponsorsTypeConverter>();
                    
                    x.CreateMap<Original.Titles, IReadOnlyCollection<Cleaned.BillTitle>>().ConvertUsing<BillTitlesTypeConverter>();

                    x.CreateMap<Original.LatestAction, Cleaned.BillAmendmentAction>()
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