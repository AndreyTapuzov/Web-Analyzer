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
    public class AnalyzedWebSiteRepository : IRepository<AnalyzedWebSite>
    {
        private readonly WebAnalyzerDbContext _context;

        public AnalyzedWebSiteRepository(WebAnalyzerDbContext context)
        {
            this._context = context;
        }

        public IEnumerable<AnalyzedWebSite> GetAll()
        {
            return _context.AnalyzedWebSites;
        }

        public IEnumerable<AnalyzedWebSite> Find(Func<AnalyzedWebSite, bool> predicate)
        {
            return _context.AnalyzedWebSites.Where(predicate);
        }

        public AnalyzedWebSite GetById(int id)
        {
            return _context.AnalyzedWebSites.Find(id);
        }

        public AnalyzedWebSite Create(AnalyzedWebSite item)
        {
            return _context.AnalyzedWebSites.Add(item);
        }
        public void Update(AnalyzedWebSite item)
        {
            _context.Entry(item).State = EntityState.Modified;
        }

        public AnalyzedWebSite Delete(AnalyzedWebSite item)
        {
            return _context.AnalyzedWebSites.Remove(item);
        }
       
    }
}
