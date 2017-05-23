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
    public class ResponseDataRepository : IRepository<ResponseData>
    {
        private readonly WebAnalyzerDbContext _context;

        public ResponseDataRepository(WebAnalyzerDbContext context)
        {
            this._context = context;
        }

        public ResponseData Create(ResponseData item)
        {
            return _context.ResponseData.Add(item);
        }

        public ResponseData Delete(ResponseData item)
        {
            return _context.ResponseData.Remove(item);
        }

        public IEnumerable<ResponseData> Find(Func<ResponseData, bool> predicate)
        {
            return _context.ResponseData.Where(predicate);
        }

        public IEnumerable<ResponseData> GetAll()
        {
            return _context.ResponseData;
        }

        public void Update(ResponseData item)
        {
            _context.Entry(item).State = EntityState.Modified;
        }

    }
}
