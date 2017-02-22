using System;

namespace CongressCollector
{
    /// <summary>
    /// Contains data related to a ZIP file residing in the GPO FDsys system. This is useful when you need to collect a list of ZIP files in bulk.
    /// </summary>
    public class BulkDataZipUrl
    {
        private string bulkDataZipBaseUrl = String.Empty;
        private string bulkDataCollectionName = String.Empty;

        /// <summary>
        /// Congress related to the data in the ZIP file
        /// </summary>
        /// <returns></returns>
        public int Congress { get; private set; }

        /// <summary>
        /// Legislative measure type related to the data in the ZIP file
        /// </summary>
        /// <returns></returns>
        public string Measure { get; private set; }

        /// <summary>
        /// Every bulk data ZIP URL is made up of 4 required values.
        /// </summary>
        /// <param name="congress">Congress related to the data in the ZIP file</param>
        /// <param name="measure">Legislative measure type related to the data in the ZIP file</param>
        /// <param name="bulkDataZipBaseUrl">The base URL of the GPO FDsys bulk data system</param>
        /// <param name="bulkDataCollectionName">The collection from which data is being obtained</param>
        public BulkDataZipUrl(int congress, string measure, string bulkDataZipBaseUrl, string bulkDataCollectionName)
        {
            Congress = congress;
            Measure = measure;
            this.bulkDataZipBaseUrl = bulkDataZipBaseUrl;
            this.bulkDataCollectionName = bulkDataCollectionName;
        }

        /// <summary>
        /// Returns the full ZIP URL to issue HTTP requests against
        /// </summary>
        public override string ToString()
        {
            return String.Format("{0}/{1}/{2}/{3}-{1}-{2}.zip", bulkDataZipBaseUrl, Congress, Measure, bulkDataCollectionName);
        }
    }
}