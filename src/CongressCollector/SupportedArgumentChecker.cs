using System;
using System.Collections.Generic;
using System.Linq;

namespace CongressCollector
{
    /// <summary>
    /// Provides for lists and validations of supported arguments to be used by the command line interface and the bulk processor. This is useful to share "business requirements" across multiple
    // tiers. For example, if we want to build a graphical interface on top of the bulk data processor, we can share this checker to make sure all interfaces share the same logic.
    /// </summary>
    public sealed class SupportedArgumentChecker
    {
        private static volatile SupportedArgumentChecker instance;
        private static object syncRoot = new Object();

        private List<SupportedArgument<string>> collections = new List<SupportedArgument<string>>();
        private List<SupportedArgument<string>> measures = new List<SupportedArgument<string>>();
        private List<SupportedArgument<int>> congresses = new List<SupportedArgument<int>>();

        /// <summary>
        /// Supported bulk data collections from the GPO FDsys system.
        /// </summary>
        /// <returns></returns>
        public IReadOnlyCollection<SupportedArgument<string>> Collections { get { return collections.AsReadOnly(); } }

        /// <summary>
        /// Supported legislative measure types from the GPO FDsys system.
        /// </summary>
        /// <returns></returns>
        public IReadOnlyCollection<SupportedArgument<string>> Measures { get { return measures.AsReadOnly(); } }

        /// <summary>
        /// Supported congresses from the GPO FDsys system.
        /// </summary>
        /// <returns></returns>
        public IReadOnlyCollection<SupportedArgument<int>> Congresses { get { return congresses.AsReadOnly(); } }

        /// <summary>
        /// Static singleton instance
        /// </summary>
        public static SupportedArgumentChecker Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new SupportedArgumentChecker();
                    }
                }

                return instance;
            }
        }

        private SupportedArgumentChecker()
        {
            collections.Add(new SupportedArgument<string>("billstatus", "Detailed bill or resolution metadata"));
            collections.Add(new SupportedArgument<string>("billsum", "Text summaries of bills, resolutions, or other documents associated with measures such as amendments, reports, or public laws"));

            measures.Add(new SupportedArgument<string>("hconres", "House Concurrent Resolution"));
            measures.Add(new SupportedArgument<string>("hjres", "House Joint Resolution"));
            measures.Add(new SupportedArgument<string>("hres", "House Simple Resolution"));
            measures.Add(new SupportedArgument<string>("hr", "House Bill"));
            measures.Add(new SupportedArgument<string>("sconres", "Senate Concurrent Resolution"));
            measures.Add(new SupportedArgument<string>("sjres", "Senate Joint Resolution"));
            measures.Add(new SupportedArgument<string>("sres", "Senate Simple Resolution"));
            measures.Add(new SupportedArgument<string>("s", "Senate Bill"));

            congresses.Add(new SupportedArgument<int>(113, "113th Congress (2013 - 2014)"));
            congresses.Add(new SupportedArgument<int>(114, "114th Congress (2015 - 2016)"));
            congresses.Add(new SupportedArgument<int>(115, "115th Congress (2017 - 2018)"));
        }

        /// <summary>
        /// Determines if a collection is valid by checking against the checker's known list of collections.
        /// </summary>
        /// <param name="collection">Collection name to validate. Usually comes from user or system input.</param>
        /// <returns>True or false indicating of the passed value is valid</returns>
        public bool IsValidCollection(string collection)
        {
            if (String.IsNullOrWhiteSpace(collection)) { return false; }
            return collections.Any(x => x.Value == collection);
        }

        /// <summary>
        /// Determines if a measure is valid by checking against the checker's known list of measures.
        /// </summary>
        /// <param name="measure">Measure name to validate. Usually comes from user or system input.</param>
        /// <returns>True or false indicating of the passed value is valid</returns>
        public bool IsValidMeasure(string measure)
        {
            if (String.IsNullOrWhiteSpace(measure)) { return false; }
            return measures.Any(x => x.Value == measure);
        }

        /// <summary>
        /// Determines if a congress is valid by checking against the checker's known list of congresses.
        /// </summary>
        /// <param name="congress">Congress name to validate. Usually comes from user or system input.</param>
        /// <returns>True or false indicating of the passed value is valid</returns>
        public bool IsValidCongress(int congress)
        {
            if (congress <= 0) { return false; }
            return congresses.Any(x => x.Value == congress);
        }
    }
}