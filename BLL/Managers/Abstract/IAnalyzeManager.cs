using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BLL.HelpModels;

namespace BLL.Managers.Abstract
{
    public interface IAnalyzeManager : IDisposable
    {
        Task<AnalyzeResultData> StartAnalyzeAsync(string url, int? pageLimitation, CancellationToken cancellationToken);
    }
}
