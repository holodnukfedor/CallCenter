using System;
using Ninject;
using CallCenterDAL.Repositories;
using CallCenterDAL.Repositories.Interfaces;

namespace CallCenterBLL.Infrastructure
{
    public class BLLNinjectBinder
    {
        public static void Init(IKernel kernel, string connectionString)
        {
            kernel.Bind<IUnitOfWork>().To<EFUnitOfWork>().WithConstructorArgument(connectionString);
        }
    }
}
