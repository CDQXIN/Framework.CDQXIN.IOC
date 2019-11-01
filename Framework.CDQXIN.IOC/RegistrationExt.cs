using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.CDQXIN.IOC
{
    public static class RegistrationExt
    {
        /// <summary>
        /// Create a new container with the component registrations that have been made.
        /// 并添加到服务发现
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IContainer BuildResolver(this ContainerBuilder builder)
        {
            var container = builder.Build();
            DependencyResolver.SetResolver(container);
            return container;
        }
    }
}
