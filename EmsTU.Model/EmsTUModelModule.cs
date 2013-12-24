using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmsTU.Common.Data;
using EmsTU.Model.Data;
using EmsTU.Model.Infrastructure;
using EmsTU.Model.Models;
using Ninject.Modules;
using Ninject.Web.Common;

namespace EmsTU.Model
{
    public class EmsTUModelModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IUserContextProvider>().To<UserContextProviderImpl>();
            Bind<IUnitOfWork>().To<EmsTUUnitOfWork>().InRequestScope();
        }
    }
}
