using System;
using System.Collections.Generic;
using System.Linq;

namespace CongressCollector
{
    public sealed class SupportedArgumentChecker
    {
        private List<SupportedArgument<string>> collections = new List<SupportedArgument<string>>();
        private List<SupportedArgument<string>> measures = new List<SupportedArgument<string>>();
        private List<SupportedArgument<int>> congresses = new List<SupportedArgument<int>>();

        public IReadOnlyCollection<SupportedArgument<string>> Collections { get { return collections.AsReadOnly(); } }
        public IReadOnlyCollection<SupportedArgument<string>> Measures { get { return measures.AsReadOnly(); } }
        public IReadOnlyCollection<SupportedArgument<int>> Congresses { get { return congresses.AsReadOnly(); } }

        private static volatile SupportedArgumentChecker instance;
        private static object syncRoot = new Object();

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

        public bool IsValidCollection(string collection)
        {
            if(String.IsNullOrWhiteSpace(collection)) { return false; }
            return collections.Any(x => x.Value == collection);
        }
        
        public bool IsValidMeasure(string measure)
        {
            if(String.IsNullOrWhiteSpace(measure)) { return false; }
            return measures.Any(x => x.Value == measure);
        }
        
        public bool IsValidCongress(int congress)
        {
            return congresses.Any(x => x.Value == congress);
        }
    }
}