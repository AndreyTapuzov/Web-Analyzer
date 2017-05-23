using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTO
{
    public class WebSiteHistoryDto
    {
        public int Id { get; set; }

        public string URL { get; set; }

        public int CountPages { get; set; }

        public DateTime RequestTime { get; set; }
    }
}
