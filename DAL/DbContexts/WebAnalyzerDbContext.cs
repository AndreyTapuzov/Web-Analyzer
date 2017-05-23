using System.Data;
using DAL.DomainModels;

namespace DAL.DbContexts
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class WebAnalyzerDbContext : DbContext
    {
       
        public WebAnalyzerDbContext(string connectionStringName)
            : base($"name={connectionStringName}")
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<WebAnalyzerDbContext>());
        }

        public virtual DbSet<WebSiteAnalyzedData> WebSitesAnalyzedData { get; set; }
        public virtual DbSet<ResponseData> ResponseData { get; set; }
        public virtual DbSet<AnalyzedWebSite> AnalyzedWebSites { get; set; }
    }
}