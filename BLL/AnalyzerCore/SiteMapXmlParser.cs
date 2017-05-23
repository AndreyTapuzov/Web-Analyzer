using System.Collections.Generic;
using System.Net.Http;
using System.Xml;
using System.Xml.Linq;

namespace BLL.AnalyzerCore
{
    public class SiteMapXmlParser
    {
        private readonly XDocument xDocument;

        private readonly int? maxUrlCount;

        private int currentUrlCount;

        public SiteMapXmlParser(string sitemapXmlContentString, int? maxUrlCount)
        {
            try
            {
                xDocument = XDocument.Parse(sitemapXmlContentString);
            }
            catch (XmlException)
            {
                xDocument = null;
            }
            this.maxUrlCount = maxUrlCount;
        }

        public IList<string> GetUrls()
        {
            if (xDocument == null)
                return null;

            var urlsList = new List<string>();

            XNamespace xn = xDocument.Root?.GetDefaultNamespace();

            if (xDocument.Element(xn + "urlset") != null)
                urlsList.AddRange(ParseSitemap(xDocument));
            else
                urlsList.AddRange(ParseSitemapIndex(xDocument));
            
            return urlsList;
        }

        private IList<string> ParseSitemapIndex(XDocument sitemapIndexXDocument)
        {
            var urlsList = new List<string>();
            XNamespace xn = sitemapIndexXDocument.Root?.GetDefaultNamespace();
            var container = sitemapIndexXDocument.Element(xn + "sitemapindex");
            var sitemapElements = container?.Elements(xn + "sitemap");

            if (sitemapElements == null)
                return urlsList;

            using (HttpClient client = new HttpClient())
            {
                foreach (XElement urlElement in sitemapElements)
                {
                    if (maxUrlCount != null && currentUrlCount >= maxUrlCount) break;

                    var locElement = urlElement.Element(xn + "loc");
                    if (locElement?.Value == null) continue;
                    
                    var responseMsg = client.GetAsync(locElement.Value).Result;
                    if(responseMsg.IsSuccessStatusCode)
                        urlsList.AddRange(ParseSitemap(XDocument.Parse(responseMsg.Content.ReadAsStringAsync().Result)));
                }
            }

            return urlsList;
        }

        private IList<string> ParseSitemap(XDocument sitemapXDocument)
        {
            var urlsList = new List<string>();
            XNamespace xn = sitemapXDocument.Root.GetDefaultNamespace();
            var container = sitemapXDocument.Element(xn + "urlset");
            var urlElements = container.Elements(xn + "url");

            foreach (XElement urlElement in urlElements)
            {
                if (maxUrlCount != null)
                    if (currentUrlCount >= maxUrlCount) break;

                var locElement = urlElement.Element(xn + "loc");
                if (locElement?.Value != null)
                {
                    urlsList.Add(locElement.Value);
                    currentUrlCount++;
                }
            }

            return urlsList;
        }
    }
}