using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Repositories.Abstract;
using DAL.Repositories.Concrete;
using Ninject.Modules;

namespace BLL.Infrastructure
{
    public class ServiceModule : NinjectModule
    {
        private readonly string _connectionStringName;

        public ServiceModule(string connectionStringName)
        {
            this._connectionStringName = connectionStringName;
        }

        public override void Load()
        {
            Bind<IUnitOfWork>().To<WebAnalyzerDbUnitOfWork>().WithConstructorArgument(_connectionStringName);
        }
    }
}
