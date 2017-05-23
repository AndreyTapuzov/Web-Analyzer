using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using DAL.DomainModels;

namespace BLL.AnalyzerCore
{
    public class MultithreadResponseBenchmark : ResponseBenchmark
    {

        private SynchronizedCollection<ResponseData> _syncCollection;

        public override async Task StartAnalyzingAsync(IList<string> urls, CancellationToken cancellationToken)
        {
            if (urls.Any())
            {
                Result = new List<ResponseData>();
                _syncCollection = new SynchronizedCollection<ResponseData>();
            }

            var tasks = new List<Task>();

            foreach (var url in urls)
            {
                //cancellationToken.ThrowIfCancellationRequested();

                var task = new Task(() =>
                {
                    using (HttpClient client = new HttpClient())
                    {
                        client.Timeout = TimeSpan.FromMinutes(1);
                        Stopwatch sw = new Stopwatch();
                        sw.Start();
                        var response = client.GetAsync(url, cancellationToken).Result;
                        sw.Stop();
                        _syncCollection.Add(new ResponseData()
                        {
                            ElapsedTime = sw.Elapsed,
                            RequestUrl = url
                        });
                    }

                });

                tasks.Add(task);
            }

            tasks.ForEach(t => t.Start());

            await Task.WhenAll(tasks).ContinueWith(task =>
            {
                Result = _syncCollection.ToList();
            }, cancellationToken);
        }
    }
}