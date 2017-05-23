using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DbContexts;
using DAL.DomainModels;
using DAL.Repositories.Abstract;

namespace DAL.Repositories.Concrete
{
    public class WebAnalyzerDbUnitOfWork : IUnitOfWork
    {
        private readonly WebAnalyzerDbContext _context;

        private IRepository<AnalyzedWebSite> _analyzedWebSite;
        private IRepository<ResponseData> _responseData;
        private IRepository<WebSiteAnalyzedData> _webSiteAnalyzedData;

        public WebAnalyzerDbUnitOfWork(string connectionStringName)
        {
            this._context = new WebAnalyzerDbContext(connectionStringName);    
        }

        public IRepository<AnalyzedWebSite> AnalyzedWebSite
        {
            get
            {
                return _analyzedWebSite ?? new AnalyzedWebSiteRepository(_context);
            }
        }

        public IRepository<ResponseData> ResponseData
        {
            get
            {
                return _responseData ?? new ResponseDataRepository(_context);
            }
        }

        public IRepository<WebSiteAnalyzedData> WebSiteAnalyzedData
        {
            get
            {
                return _webSiteAnalyzedData ?? new WebSiteAnalyzedDataRepository(_context);
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

    }
}
