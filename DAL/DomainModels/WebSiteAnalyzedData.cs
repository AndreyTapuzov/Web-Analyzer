using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DomainModels
{
    public class WebSiteAnalyzedData
    {
        public WebSiteAnalyzedData()
        {
            ResponseData = new List<ResponseData>();
        }
        public int Id { get; set; }
        public DateTime RequestTime { get; set; }

        public int AnalyzedWebSiteId { get; set; }
        public virtual AnalyzedWebSite AnalyzedWebSite { get; set; }

        public virtual List<ResponseData> ResponseData { get; set; }
    }
}
