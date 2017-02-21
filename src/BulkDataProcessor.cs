using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Newtonsoft.Json;

namespace CongressCollector
{
    public class BulkDataProcessor
    {
        private const string bulkDataBaseUrl = "https://www.gpo.gov/fdsys/bulkdata/";
        private readonly string outputBasePath = System.AppContext.BaseDirectory + "/";
        private readonly string bulkDataZipBaseUrl = String.Empty;
        private readonly string outputPath = String.Empty;
        private string bulkDataCollectionName = String.Empty;

        public BulkDataProcessor(string collectionName)
        {
            bulkDataCollectionName = collectionName.ToUpper();
            bulkDataZipBaseUrl = bulkDataBaseUrl + bulkDataCollectionName;
            outputPath = outputBasePath + bulkDataCollectionName;
        }

        public async Task StartAsync(int? congress = null, string measure = "")
        {
            bool isCongressProvided = congress.HasValue;
            bool isMeasureProvided = !String.IsNullOrWhiteSpace(measure);

            if (isMeasureProvided && !SupportedArgumentChecker.Instance.IsValidMeasure(measure))
            {
                throw new ArgumentOutOfRangeException("congress", String.Format("Measure '{0}' is not supported.", measure));
            }

            if (isCongressProvided && !SupportedArgumentChecker.Instance.IsValidCongress(congress.Value))
            {
                throw new ArgumentOutOfRangeException("congress", String.Format("Congress '{0}' is not supported.", congress));
            }

            List<BulkDataZipUrl> bulkDataZipUrls = new List<BulkDataZipUrl>();

            // nothing was provided
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
            // both congress and measures were provided
            else if (isCongressProvided && isMeasureProvided)
            {
                var bulkDataZipUrl = GetBulkDataZipUrl(congress.Value, measure);
                bulkDataZipUrls.Add(bulkDataZipUrl);
            }
            // only congress was provided, so loop over measures to get all of them
            else if (isCongressProvided && !isMeasureProvided)
            {
                foreach (var measureType in SupportedArgumentChecker.Instance.Measures)
                {
                    var bulkDataZipUrl = GetBulkDataZipUrl(congress.Value, measureType.Value);
                    bulkDataZipUrls.Add(bulkDataZipUrl);
                }
            }
            // only measure was provided so loop over congresses to get all of them
            else if (!isCongressProvided && isMeasureProvided)
            {
                foreach (var congressNumber in SupportedArgumentChecker.Instance.Congresses)
                {
                    var bulkDataZipUrl = GetBulkDataZipUrl(congressNumber.Value, measure);
                    bulkDataZipUrls.Add(bulkDataZipUrl);
                }
            }

            HttpClient httpClient = new HttpClient();

            foreach (var bulkDataZipUrl in bulkDataZipUrls)
            {
                string outputPathWithBillType = String.Format("{0}/{1}/{2}", outputPath, bulkDataZipUrl.Congress, bulkDataZipUrl.Measure);

                if (!Directory.Exists(outputPathWithBillType))
                {
                    Directory.CreateDirectory(outputPathWithBillType);
                }

                using (var zipStream = await httpClient.GetStreamAsync(bulkDataZipUrl.ZipUrl))
                {
                    using (ZipArchive archive = new ZipArchive(zipStream, ZipArchiveMode.Read))
                    {
                        foreach (var entry in archive.Entries)
                        {
                            using (Stream entryStream = entry.Open())
                            {
                                using (MemoryStream entryMemoryStream = new MemoryStream())
                                {
                                    entryStream.CopyTo(entryMemoryStream);
                                    string xml = Encoding.UTF8.GetString(entryMemoryStream.ToArray());
                                    string outputXmlPath = Path.Combine(outputPathWithBillType, entry.Name);
                                    File.WriteAllText(outputXmlPath, xml);
                                }
                            }
                            using (Stream entryStream = entry.Open())
                            {
                                var xDocument = XDocument.Load(entryStream);
                                string json = JsonConvert.SerializeXNode(xDocument);
                                string jsonFileName = Path.GetFileNameWithoutExtension(entry.Name) + ".json";
                                string output = Path.Combine(outputPathWithBillType, jsonFileName);
                                File.WriteAllText(output, json);
                            }
                        }
                    }
                }
            }
        }

        private BulkDataZipUrl GetBulkDataZipUrl(int congress, string measure)
        {
            string zipUrl = bulkDataZipBaseUrl + String.Format("/{0}/{1}/{2}-{0}-{1}.zip", congress, measure, bulkDataCollectionName);
            return new BulkDataZipUrl(congress, measure, zipUrl);
        }
    }
}