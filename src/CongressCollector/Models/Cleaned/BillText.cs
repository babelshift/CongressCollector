using System;

namespace CongressCollector.Models.Cleaned
{
    public class BillText
    {
        public string BillId { get; set; }
        public string PackageId { get; set; }

        public string PackageUrl
        {
            get
            {
                return String.Format("https://www.gpo.gov/fdsys/search/pagedetails.action?packageId={0}", PackageId);
            }
        }

        public string TextUrl
        {
            get
            {
                return String.Format("https://www.gpo.gov/fdsys/pkg/{0}/html/{0}.htm", PackageId);
            }
        }

        public string PdfUrl
        {
            get
            {
                return String.Format("https://www.gpo.gov/fdsys/pkg/{0}/pdf/{0}.pdf", PackageId);
            }
        }

        public string XmlUrl
        {
            get
            {
                return String.Format("https://www.gpo.gov/fdsys/pkg/{0}/xml/{0}.xml", PackageId);
            }
        }

        public string ZipUrl
        {
            get
            {
                return String.Format("https://www.gpo.gov/fdsys/pkg/{0}.zip", PackageId);
            }
        }

        public string Text { get; set; }
    }
}