using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using BLL.Enums;

namespace BLL.AnalyzerCore
{

    public class SiteMapSearchEngine
    {
        public AnalyzeResult SiteMapSearchResult { get; private set; }

        private readonly string _url;

        public SiteMapSearchEngine(string hostUrl)
        {
            this._url = hostUrl;
            SiteMapSearchResult = AnalyzeResult.Success;
        }

        public virtual async Task<string> SearchSiteMapContentAsync(CancellationToken cancellationToken)
        {
            string[] supposedPaths = { "/sitemap.xml" };

            string contentString = null;

            using (HttpClient client = new HttpClient())
            {
                foreach (var supposedPath in supposedPaths)
                {
                    var supposedSiteMapUrl = _url + supposedPath;

                    HttpResponseMessage response;
                    try
                    {
                        response = await client.GetAsync(supposedSiteMapUrl, cancellationToken);
                    }
                    catch (Exception)
                    {
                        SiteMapSearchResult = AnalyzeResult.NotCorrectUrl;
                        return contentString;
                    }

                    if (response.IsSuccessStatusCode)
                    {
                        contentString = await response.Content.ReadAsStringAsync();
                    }
                    else SiteMapSearchResult = AnalyzeResult.NoSitemap;
                }
            }

            return contentString;
        }
    }
}