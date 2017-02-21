namespace CongressCollector
{
    public class BulkDataZipUrl
    {
        public int Congress { get; private set; }
        public string Measure { get; private set; }
        public string ZipUrl { get; private set; }

        public BulkDataZipUrl(int congress, string measure, string zipUrl)
        {
            Congress = congress;
            Measure = measure;
            ZipUrl = zipUrl;
        }
    }
}