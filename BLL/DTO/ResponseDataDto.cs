using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTO
{
    public class ResponseDataDto
    {
        public int Id { get; set; }
        public string RequestUrl { get; set; }
        public TimeSpan ElapsedTime { get; set; }
        public int WebSiteAnalyzedDataId { get; set; }
    }
}
