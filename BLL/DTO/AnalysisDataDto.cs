using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTO
{
    public class AnalysisDataDto
    {
        public string Url { get; set; }

        public string SerializedData { get; set; }

        public List<ResponseDataDto> ResponseData { get; set; }
    }
}
