using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DTO;
using DAL.DomainModels;

namespace BLL.Managers.Abstract
{
    public interface IHistoryManager : IDisposable
    {
        IEnumerable<WebSiteHistoryDto> GetFullHistory();
        AnalysisDataDto GetRequestHistory(int requestId);
    }
}
