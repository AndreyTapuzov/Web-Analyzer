using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DTO;
using BLL.Enums;

namespace BLL.HelpModels
{
    public class AnalyzeResultData
    {
        public AnalyzeResult ResultState { get; set; }
        public string SerializedData { get; set; }
        public IEnumerable<ResponseDataDto> ResponseData { get; set; }
        public string DomainUrl { get; set; }
    }
}
