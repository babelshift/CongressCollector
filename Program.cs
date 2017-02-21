using System.Threading.Tasks;

namespace ConsoleApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Task.Run(async () =>
            {
                BulkDataProcessor bulkDataProcessor = new BulkDataProcessor(BulkDataType.BillStatus);
                await bulkDataProcessor.StartAsync(113, BillType.HCONRES);
                await bulkDataProcessor.StartAsync(113, BillType.HJRES);
                await bulkDataProcessor.StartAsync(113, BillType.HR);
                await bulkDataProcessor.StartAsync(113, BillType.HRES);
            }).Wait();
        }
    }
}
