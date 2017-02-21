using System;
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

        public BulkDataProcessor(BulkDataType bulkDataType)
        {
            if (bulkDataType == BulkDataType.BillSummary)
            {
                bulkDataCollectionName = "BILLSUM";
            }
            else if (bulkDataType == BulkDataType.BillStatus)
            {
                bulkDataCollectionName = "BILLSTATUS";
            }
            else
            {
                throw new NotImplementedException();
            }

            bulkDataZipBaseUrl = bulkDataBaseUrl + bulkDataCollectionName;
            outputPath = outputBasePath + bulkDataCollectionName;
        }

        public async Task StartAsync(int congress, BillType billType)
        {
            if (congress < 113 || congress > 115)
            {
                throw new ArgumentOutOfRangeException("congress", "Bulk data processing only supported for Congress 113, 114, and 115.");
            }

            string outputPathWithBillType = outputPath + "/" + billType.ToString().ToLower();

            if (!Directory.Exists(outputPathWithBillType))
            {
                Directory.CreateDirectory(outputPathWithBillType);
            }

            string bulkDataZipUrl = bulkDataZipBaseUrl + String.Format("/{0}/{1}/{2}-{0}-{1}.zip", 
                congress,
                billType.ToString().ToLower(),
                bulkDataCollectionName);

            HttpClient httpClient = new HttpClient();
            using (var zipStream = await httpClient.GetStreamAsync(bulkDataZipUrl))
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
                                string outputXmlPath = Path.Combine(outputPathWithBillType , entry.Name);
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
}