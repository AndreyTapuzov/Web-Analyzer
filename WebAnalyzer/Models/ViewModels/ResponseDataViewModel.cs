using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebAnalyzer.Models.ViewModels
{
    public class ResponseDataViewModel
    {
        public int Place { get; set; }

        [Display(Name = "URL")]
        public string RequestUrl { get; set; }

        [Display(Name = "Elapsed time (sec)")]
        public TimeSpan ElapsedTime { get; set; }
    }
}