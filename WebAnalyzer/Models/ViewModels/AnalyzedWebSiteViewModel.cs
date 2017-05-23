using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebAnalyzer.Models.ViewModels
{
    public class AnalyzedWebSiteViewModel
    {
        public int Id { get; set; }

        [Display(Name = "URL of website")]
        public string URL { get; set; }

        [Display(Name = "Number of pages")]
        public int CountPages { get; set; }

        [Display(Name = "Request time")]
        public DateTime RequestTime { get; set; }
    }
}