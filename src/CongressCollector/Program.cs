using CongressCollector.Models;
using Microsoft.Extensions.CommandLineUtils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CongressCollector
{
    public class Program
    {
        private const string helpOptionText = "-? | -h | --help";

        public static void Main(string[] args)
        {
            CommandLineApplication commandLineApplication = new CommandLineApplication(throwOnUnexpectedArg: false);
            commandLineApplication.Name = "CongressCollector";
            commandLineApplication.FullName = "CongressCollector - For all your congressional data needs.";
            commandLineApplication.HelpOption(helpOptionText);

            // Setup the 'collect' command to initiate bulk processing based on user options
            commandLineApplication.Command("collect", collectCommandConfig =>
            {
                collectCommandConfig.Description = "Initiate the process of collecting bulk data from the GPO FDsys database.";
                collectCommandConfig.HelpOption(helpOptionText);

                // The collect command requires a collection argument and has two optional parameters to specify congress and measures to retrieve
                var collectionArgument = collectCommandConfig.Argument("collection", "Bulk data collection to retrieve. See 'list collections' for valid inputs.");
                var congressOption = collectCommandConfig.Option("-c | --congress <congress>", "Congress for which to receive data. See 'list congresses' for valid inputs.", CommandOptionType.SingleValue);
                var measureOption = collectCommandConfig.Option("-m | --measure <measure>", "Legislative measures to retrieve. See 'list measures' for valid inputs.", CommandOptionType.SingleValue);
                var outputOption = collectCommandConfig.Option("-o | --out <out>", "Path to write application output files.", CommandOptionType.SingleValue);
                var forceOption = collectCommandConfig.Option("-f | --force", "Forces the application to output to the folder identified by the '-o' option even if it doesn't exist.", CommandOptionType.NoValue);

                // Setup the logic to execute when the collect command is initiated
                collectCommandConfig.OnExecute(() =>
                {
                    // The collection argument is a required input for the collect command
                    if (String.IsNullOrWhiteSpace(collectionArgument.Value))
                    {
                        Console.WriteLine("Required parameter '{0}' for command '{1}' was not provided.", collectionArgument.Name, collectCommandConfig.Name);
                        return 0;
                    }

                    string collectionName = collectionArgument.Value.ToLower();

                    // Don't do anything if the user provided an invalid collection name
                    if (!SupportedArgumentChecker.Instance.IsValidCollection(collectionName))
                    {
                        Console.WriteLine("An invalid value was supplied for parameter '{0}' and command '{1}'. See 'list collections' for valid inputs.", collectionArgument.Name, collectCommandConfig.Name);
                        return 0;
                    }

                    // If the user provided a congress option, try to parse and use it
                    int? congress = null;
                    int temp = 0;
                    if (congressOption.HasValue())
                    {
                        bool success = int.TryParse(congressOption.Value(), out temp);
                        congress = success ? (int?)temp : null;

                        // Don't do anything if the user provided an invalid congress option
                        if (!success || !SupportedArgumentChecker.Instance.IsValidCongress(congress.Value))
                        {
                            Console.WriteLine("An invalid value was supplied for option '{0}'. See 'list congresses' for valid inputs.", congressOption.LongName);
                            return 0;
                        }
                    }

                    // If the user has provided a measure option, try to parse and use it
                    string measure = String.Empty;
                    if (measureOption.HasValue())
                    {
                        // Don't do anything if the user provided an invalid measure option
                        if (!SupportedArgumentChecker.Instance.IsValidMeasure(measureOption.Value()))
                        {
                            Console.WriteLine("An invalid value was supplied for option '{0}'. See 'list measures' for valid inputs.", measureOption.LongName);
                            return 0;
                        }
                        measure = measureOption.Value().ToString();
                    }

                    string output = String.Empty;
                    if (outputOption.HasValue())
                    {
                        output = outputOption.Value().ToString();
                    }

                    bool isForced = forceOption.HasValue();

                    // Start bulk processing based on the user inputs
                    BulkDataProcessor bulkDataProcessor = new BulkDataProcessor(collectionName);
                    Task.Run(async () =>
                    {
                        AutoMapperConfiguration.Initialize();

                        try
                        {
                            await bulkDataProcessor.StartAsync(congress, measure, output, isForced);
                        }
                        catch (DirectoryNotFoundException)
                        {
                            Console.WriteLine("That output directory doesn't exist. Use the '-f' flag to force use it or change to a different directory.");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }).Wait();
                    return 0;
                });
            });

            commandLineApplication.Command("list", config =>
            {
                config.Description = "List some valid inputs for commands and options.";
                config.HelpOption(helpOptionText);

                var listCongressesArgument = config.Command("congresses", argumentConfig =>
                {
                    argumentConfig.Description = "List all congresses that can be used in the '-c' option of the 'collect' command.";

                    argumentConfig.OnExecute(() =>
                    {
                        PrintCommandLineArguments(SupportedArgumentChecker.Instance.Congresses);
                        return 0;
                    });
                });
                var listMeasuresArgument = config.Command("measures", argumentConfig =>
                {
                    argumentConfig.Description = "List all measures that can be used in the '-m' option of the 'collect' command.";

                    argumentConfig.OnExecute(() =>
                    {
                        PrintCommandLineArguments(SupportedArgumentChecker.Instance.Measures);
                        return 0;
                    });
                });
                var listCollectionsArgument = config.Command("collections", argumentConfig =>
                {
                    argumentConfig.Description = "List all collections that can be used as an argument of the 'collect' command.";

                    argumentConfig.OnExecute(() =>
                    {
                        PrintCommandLineArguments(SupportedArgumentChecker.Instance.Collections);
                        return 0;
                    });
                });
            });

            commandLineApplication.Execute(args);
        }

        /// <summary>
        /// Prints each supported argument in the passed argument list to the console
        /// </summary>
        /// <param name="arguments">Supported arguments with values and descriptions to print</param>
        private static void PrintCommandLineArguments<T>(IEnumerable<SupportedArgument<T>> arguments)
        {
            if (arguments == null || !arguments.Any())
            {
                return;
            }

            foreach (var argument in arguments)
            {
                Console.WriteLine(String.Format("'{0}' - {1}", argument.Value, argument.Description));
            }
        }
    }
}