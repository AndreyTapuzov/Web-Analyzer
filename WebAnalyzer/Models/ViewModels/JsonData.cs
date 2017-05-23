using System.Runtime.Serialization;

namespace WebAnalyzer.Models.ViewModels
{
    [DataContract]
    public class JsonData
    {
        [DataMember(Name = "label")]
        public string RequestUrl { get; set; }
        [DataMember(Name = "y")]
        public double ElapsedSeconds { get; set; } 
    }
}
