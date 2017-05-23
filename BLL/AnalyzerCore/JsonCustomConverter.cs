using System.Collections.Generic;
using System.Linq;
using BLL.DTO;
using BLL.HelpModels;
using System.Web.Script.Serialization;
using Newtonsoft.Json;

namespace BLL.AnalyzerCore
{
    public class JsonCustomConverter
    {
        public static string ConvertCollection(IEnumerable<ResponseDataDto> data)
        {
            var jsonCollection = from responseData in data
                                 select
                                 new JsonData()
                                 {
                                     RequestUrl = responseData.RequestUrl,
                                     ElapsedSeconds = responseData.ElapsedTime.TotalSeconds
                                 };

            return JsonConvert.SerializeObject(jsonCollection);
        }
    }
}