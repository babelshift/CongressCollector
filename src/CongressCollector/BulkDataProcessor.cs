using CongressCollector.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CongressCollector
{
    /// <summary>
    /// Provides methods to perform bulk data collection and processing from the GPO FDsys system.
    /// </summary>
    public class BulkDataProcessor
    {
        private const string bulkDataBaseUrl = "https://www.gpo.gov/fdsys/bulkdata/";
        private readonly string outputBasePathDefault = System.AppContext.BaseDirectory;
        private readonly string bulkDataZipBaseUrl = String.Empty;
        private string outputPathActual = String.Empty;
        private string bulkDataCollectionName = String.Empty;

        ILogger Logger { get; } = ApplicationLogging.CreateLogger<BulkDataProcessor>();

        /// <summary>
        /// Default constructor creates a bulk data processor for a specific collection.
        /// </summary>
        /// <param name="collectionName">Collection to bulk process such as bill statuses or bill summaries</param>
        public BulkDataProcessor(string collectionName)
        {
            bulkDataCollectionName = collectionName.ToUpper();
            bulkDataZipBaseUrl = bulkDataBaseUrl + bulkDataCollectionName;
            outputPathActual = Path.Combine(outputBasePathDefault, bulkDataCollectionName);
        }

        /// <summary>
        /// Begins collecting and processing bulk data based on this instance's collection and the optional congress and measure parameters.
        /// </summary>
        /// <param name="congress">Filters the process down to a specific congress</param>
        /// <param name="measure">Filters the process down to a specific legislative measure</param>
        public async Task StartAsync(int? congress = null, string measure = "", string outputPath = "", bool isForced = false)
        {
            bool isCongressProvided = congress.HasValue;
            bool isMeasureProvided = !String.IsNullOrWhiteSpace(measure);
            bool isOutputPathProvided = !String.IsNullOrWhiteSpace(outputPath);

            if (isMeasureProvided && !SupportedArgumentChecker.Instance.IsValidMeasure(measure))
            {
                throw new ArgumentOutOfRangeException("measure", String.Format("Measure '{0}' is not supported.", measure));
            }

            if (isCongressProvided && !SupportedArgumentChecker.Instance.IsValidCongress(congress.Value))
            {
                throw new ArgumentOutOfRangeException("congress", String.Format("Congress '{0}' is not supported.", congress));
            }

            // If an output path was provided, check if it exists and if we are forced to create it
            if (isOutputPathProvided)
            {
                // If the user input output location does not exist and did not force, then exception indicating it doesn't exist
                if (!Directory.Exists(outputPath) && !isForced)
                {
                    throw new DirectoryNotFoundException(String.Format("Output directory '{0}' not found.", outputPath));
                }

                // Override the output path with that value if all is good
                this.outputPathActual = Path.Combine(outputPath, bulkDataCollectionName);
            }

            List<BulkDataZipUrl> bulkDataZipUrls = new List<BulkDataZipUrl>();

            // No options were provided, so we need to loop over all congresses and measures
            if (!isCongressProvided && !isMeasureProvided)
            {
                foreach (var congressNumber in SupportedArgumentChecker.Instance.Congresses)
                {
                    foreach (var measureType in SupportedArgumentChecker.Instance.Measures)
                    {
                        var bulkDataZipUrl = GetBulkDataZipUrl(congressNumber.Value, measureType.Value);
                        bulkDataZipUrls.Add(bulkDataZipUrl);
                    }
                }
            }
            // Both congress and measure options were provided, so we only want to get specific congress and measure files
            else if (isCongressProvided && isMeasureProvided)
            {
                var bulkDataZipUrl = GetBulkDataZipUrl(congress.Value, measure);
                bulkDataZipUrls.Add(bulkDataZipUrl);
            }
            // Only the congress option was provided, so loop over all measures related to a specific congress
            else if (isCongressProvided && !isMeasureProvided)
            {
                foreach (var measureType in SupportedArgumentChecker.Instance.Measures)
                {
                    var bulkDataZipUrl = GetBulkDataZipUrl(congress.Value, measureType.Value);
                    bulkDataZipUrls.Add(bulkDataZipUrl);
                }
            }
            // Only the measure option was provided, so loop over all congresses related to a specific legislative measure
            else if (!isCongressProvided && isMeasureProvided)
            {
                foreach (var congressNumber in SupportedArgumentChecker.Instance.Congresses)
                {
                    var bulkDataZipUrl = GetBulkDataZipUrl(congressNumber.Value, measure);
                    bulkDataZipUrls.Add(bulkDataZipUrl);
                }
            }

            HttpClient httpClient = new HttpClient();

            // Collect and process the ZIP files found at the URLs determined from our parameter evaluation above
            foreach (var bulkDataZipUrl in bulkDataZipUrls)
            {
                string outputPathWithBillType = Path.Combine(outputPathActual, bulkDataZipUrl.Congress.ToString(), bulkDataZipUrl.Measure);

                if (!Directory.Exists(outputPathWithBillType))
                {
                    Directory.CreateDirectory(outputPathWithBillType);
                    Logger.LogInformation("Created directory: {0}", outputPathWithBillType);
                }

                Logger.LogInformation("Downloading file at: {0}", bulkDataZipUrl);
                using (var zipStream = await httpClient.GetStreamAsync(bulkDataZipUrl.ToString()))
                {
                    using (ZipArchive archive = new ZipArchive(zipStream, ZipArchiveMode.Read))
                    {
                        foreach (var entry in archive.Entries)
                        {
                            Logger.LogInformation("Processing file: {0}", entry.Name);
                            // Open the currently processing file, read into memory, write to XML and JSON
                            //using (Stream entryStream = File.OpenRead("F:\\Programming\\src-current\\GitHub\\CongressCollector\\src\\CongressCollector\\bin\\Debug\\netcoreapp1.1\\BILLSTATUS\\114\\hr\\BILLSTATUS-114hr10.xml"))
                            using (Stream entryStream = entry.Open())
                            {
                                using (MemoryStream entryMemoryStream = new MemoryStream())
                                {
                                    entryStream.CopyTo(entryMemoryStream);
                                    byte[] entryMemoryStreamBytes = entryMemoryStream.ToArray();

                                    WriteXmlFile(outputPathWithBillType, entry.Name, entryMemoryStreamBytes);

                                    WriteJsonFile(outputPathWithBillType, entry.Name, entryMemoryStreamBytes);
                                }
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Writes a JSON file out after cleaning up the data structure received from the XML contents.
        /// </summary>
        /// <param name="outputPathWithBillType"></param>
        /// <param name="zipArchiveEntryName"></param>
        /// <param name="entryMemoryStreamBytes"></param>
        private void WriteJsonFile(string outputPathWithBillType, string zipArchiveEntryName, byte[] entryMemoryStreamBytes)
        {
            if (String.IsNullOrWhiteSpace(zipArchiveEntryName) || entryMemoryStreamBytes == null || entryMemoryStreamBytes.Length == 0)
            {
                return;
            }

            string cleanedJson = String.Empty;

            // If the request was for BILLSTATUS, we need to deserialize to the correct objects
            if (bulkDataCollectionName == "BILLSTATUS")
            {
                // Deserialize the XML to disgusting objects
                var originalObjects = XmlHelper.DeserializeXML<Models.Original.BillStatus>(entryMemoryStreamBytes);

                // Clean the objects by mapping to a sane structure with proper data types and serialize to JSON
                var cleanedObjects = AutoMapperConfiguration.Mapper.Map<Models.Original.Bill, Models.Cleaned.BillStatus>(originalObjects.Bill);
                cleanedJson = JsonConvert.SerializeObject(cleanedObjects);
            }
            // BILLS collection is just raw XML text to save
            else if (bulkDataCollectionName == "BILLS")
            {
                Models.Cleaned.BillText billText = new Models.Cleaned.BillText()
                {
                    Text = System.Text.Encoding.UTF8.GetString(entryMemoryStreamBytes)
                };
                cleanedJson = JsonConvert.SerializeObject(billText);
            }

            // If we have something to write out, do so to a file
            if (!String.IsNullOrWhiteSpace(cleanedJson))
            {
                string jsonFileName = Path.GetFileNameWithoutExtension(zipArchiveEntryName) + ".json";
                string outputJsonPath = Path.Combine(outputPathWithBillType, jsonFileName);
                File.WriteAllText(outputJsonPath, cleanedJson);
            }
        }

        /// <summary>
        /// Writes the XML file out exactly as it appears from the
        /// </summary>
        /// <param name="outputPathWithBillType"></param>
        /// <param name="zipArchiveEntryName"></param>
        /// <param name="entryMemoryStreamBytes"></param>
        private static void WriteXmlFile(string outputPathWithBillType, string zipArchiveEntryName, byte[] entryMemoryStreamBytes)
        {
            if (String.IsNullOrWhiteSpace(zipArchiveEntryName) || entryMemoryStreamBytes == null || entryMemoryStreamBytes.Length == 0)
            {
                return;
            }

            string originalXml = Encoding.UTF8.GetString(entryMemoryStreamBytes);
            string outputXmlPath = Path.Combine(outputPathWithBillType, zipArchiveEntryName);
            File.WriteAllText(outputXmlPath, originalXml);
        }

        /// <summary>
        ///  Returns a URL to a ZIP file contained in the GPO FDsys system based on this instance's collection and the passed congress and measure parameters
        /// </summary>
        /// <param name="congress">Indicates the congress related to the ZIP file</param>
        /// <param name="measure">Indicates the legislative measure type related to the ZIP file</param>
        /// <returns>URL to ZIP file containing XML files in GPO FDsys</returns>
        private BulkDataZipUrl GetBulkDataZipUrl(int congress, string measure)
        {
            return new BulkDataZipUrl(congress, measure, bulkDataZipBaseUrl, bulkDataCollectionName);
        }
    }
}