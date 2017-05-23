using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DbContexts;
using DAL.DomainModels;
using DAL.Repositories.Abstract;

namespace DAL.Repositories.Concrete
{
    public class WebSiteAnalyzedDataRepository : IRepository<WebSiteAnalyzedData>
    {
        private readonly WebAnalyzerDbContext _context;

        public WebSiteAnalyzedDataRepository(WebAnalyzerDbContext context)
        {
            this._context = context;
        }

        public IEnumerable<WebSiteAnalyzedData> GetAll()
        {
            return _context.WebSitesAnalyzedData;
        }

        public IEnumerable<WebSiteAnalyzedData> Find(Func<WebSiteAnalyzedData, bool> predicate)
        {
            return _context.WebSitesAnalyzedData.Where(predicate);
        }

        public WebSiteAnalyzedData Create(WebSiteAnalyzedData item)
        {
            return _context.WebSitesAnalyzedData.Add(item);
        }

        public void Update(WebSiteAnalyzedData item)
        {
            _context.Entry(item).State = EntityState.Modified;
        }

        public WebSiteAnalyzedData Delete(WebSiteAnalyzedData item)
        {
            return _context.WebSitesAnalyzedData.Remove(item);
        }
    }
}
