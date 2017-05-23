using System;

namespace DAL.DomainModels
{
    public class ResponseData
    {
        public int Id { get; set; }

        public string RequestUrl { get; set; }

        public TimeSpan ElapsedTime { get; set; }

        public int WebSiteAnalyzedDataId { get; set; }
        //[JsonIgnore]
        public virtual WebSiteAnalyzedData WebSiteAnalyzedData { get; set; }

    }
}
