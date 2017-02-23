using AutoMapper;

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
                    x.CreateMap<Models.Original.Bill, Models.Cleaned.Bill>().ConvertUsing<BillTypeConverter>();
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