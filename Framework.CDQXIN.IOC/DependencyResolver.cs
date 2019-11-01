using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.CDQXIN.IOC
{
    /// <summary>
    /// 依赖性解析器
    /// </summary>
    public class DependencyResolver
    {
        /// <summary>
        /// 容器
        /// </summary>
        private static IContainer _current;

        /// <summary>
        /// Set Resolver
        /// </summary>
        /// <param name="container"></param>
        public static void SetResolver(IContainer container)
        {
            _current = container;
        }

        /// <summary>
        /// 发现服务
        /// </summary>
        /// <typeparam name="T">interface</typeparam>
        /// <returns></returns>
        public static T Resolve<T>()
        {
            return _current.Resolve<T>();
        }
    }
}
