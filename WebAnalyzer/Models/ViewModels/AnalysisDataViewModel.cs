using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BLL.DTO;

namespace WebAnalyzer.Models.ViewModels
{
    public class AnalysisDataViewModel
    {
        [Required(ErrorMessage = "Please, enter URL")]
        [Display(Name = "Enter URL to analyze")]
        [RegularExpression("^(https?|ftp)://[^\\s/$.?#].[^\\s]*$", ErrorMessage = "Please, enter valid URL")]
        [DataType(DataType.Url, ErrorMessage = "Please, enter correct URL")]
        public string Url { get; set; }

        [RegularExpression("\\s*(([1-9]\\d*)|(0+[1-9]\\d*))\\s*", ErrorMessage = "Please, enter valid number")]
        public int? PagesLimitation { get; set; }

        public string SerializedData { get; set; }

        public List<ResponseDataViewModel> ResponseData { get; set; }
    }
}