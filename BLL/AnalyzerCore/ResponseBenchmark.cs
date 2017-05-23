using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using DAL.DomainModels;

namespace BLL.AnalyzerCore
{
    public class ResponseBenchmark
    {
        public List<ResponseData> Result { get; protected set; }

        public virtual async Task StartAnalyzingAsync(IList<string> urls, CancellationToken cancellationToken)
        {
            if (urls.Any())
                Result = new List<ResponseData>();

            using (HttpClient client = new HttpClient())
            {
                foreach (var url in urls)
                {
                    Stopwatch sw = new Stopwatch();
                    sw.Start();
                    await client.GetAsync(url, cancellationToken);
                    sw.Stop();
                    Result.Add(new ResponseData() { ElapsedTime = sw.Elapsed, RequestUrl = url });
                }
            }
        }

    }
}