using System;
using DAL.DomainModels;

namespace DAL.Repositories.Abstract
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<AnalyzedWebSite> AnalyzedWebSite { get; }
        IRepository<ResponseData> ResponseData { get; }
        IRepository<WebSiteAnalyzedData> WebSiteAnalyzedData { get; }

        void Save();
    }
}
