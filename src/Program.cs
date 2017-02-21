using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.CommandLineUtils;

namespace CongressCollector
{
    public class Program
    {
        public static void Main(string[] args)
        {
                // Program.exe <-g|--greeting|-$ <greeting>> [name <fullname>]
                // [-?|-h|--help] [-u|--uppercase]
                CommandLineApplication commandLineApplication = new CommandLineApplication(throwOnUnexpectedArg: false);
                commandLineApplication.Name = "CongressCollector";
                commandLineApplication.FullName = "CongressCollector - For all your congressional data needs.";

                commandLineApplication.HelpOption("-? | -h | --help");
                var collectionOption = commandLineApplication.Option("-b | --bulkcollection <bulkcollection>", "Bulk data collection to get such as 'billstatus'.", CommandOptionType.SingleValue);
                var congressOption = commandLineApplication.Option("-c | --congress <congress>", "Congress to retrieve data for such as '113'.", CommandOptionType.SingleValue);
                var billTypeOption = commandLineApplication.Option("-t | --billtype <billtype>", "Bill types to retrieve such as 'hconres'.", CommandOptionType.SingleValue);

                commandLineApplication.OnExecute(async () =>
                {
                    if(collectionOption.HasValue())
                    {
                        if(collectionOption.Value() == "billstatus")
                        {
                            BulkDataProcessor bulkDataProcessor = new BulkDataProcessor(BulkDataType.BillStatus);

                            if(congressOption.HasValue())
                            {
                                int congress = 0;
                                bool success = int.TryParse(congressOption.Value(), out congress);

                                if(!success)
                                {
                                    return 0;
                                }

                                if(billTypeOption.HasValue())
                                {
                                    BillType billType = BillType.Unknown;
                                    if(billTypeOption.Value() == "hconres")
                                    {
                                        billType = BillType.HCONRES;
                                        await bulkDataProcessor.StartAsync(congress, billType);
                                    }
                                }
                                else
                                {
                                    return 0;
                                }
                            }
                            else
                            {
                                return 0;
                            }
                        }
                    }

                    return 0;
                });
                commandLineApplication.Execute(args);

                
                // await bulkDataProcessor.StartAsync(113, BillType.HCONRES);
                // await bulkDataProcessor.StartAsync(113, BillType.HJRES);
                // await bulkDataProcessor.StartAsync(113, BillType.HR);
                // await bulkDataProcessor.StartAsync(113, BillType.HRES);
        }
    }
}