using Autofac;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Framework.CDQXIN.IOC
{
    /// <summary>
    /// 依赖配置
    /// </summary>
    public class DependencyConfig : IDependencyConfig
    {
        /// <summary>
        /// 注入依赖项
        /// </summary>
        /// <param name="builder">容器</param>
        public static void RegisterDependency(ContainerBuilder builder)
        {
            var enumerable = typeof(DependencyConfig).Assembly.GetTypes().Where(p => typeof(IDependency).IsAssignableFrom(p) && p.IsClass);

            var currents = enumerable as Type[] ?? enumerable.ToArray();

            if (currents.Any())
            {
                foreach (var current in currents)
                {
                    var type = current.GetInterfaces().FirstOrDefault(p =>
                            typeof(IDependency).IsAssignableFrom(p)
                            && !string.Equals(p.Name, "IDependency", StringComparison.CurrentCultureIgnoreCase)
                            && p.Name.IndexOf("IBase", StringComparison.CurrentCultureIgnoreCase) < 0);

                    if (type != null)
                    {
                        builder.RegisterType(current).As(type);
                    }
                }
            }
        }

        /// <summary>
        /// 注册依赖项
        /// </summary>
        /// <param name="assembly">程序集</param>
        /// <param name="builder">容器</param>
        public static void RegisterDependency(Assembly assembly, ContainerBuilder builder)
        {
            try
            {
                var enumerable = assembly.GetTypes().Where(p => typeof(IDependency).IsAssignableFrom(p) && p.IsClass);

                var currents = enumerable as Type[] ?? enumerable.ToArray();

                if (currents.Any())
                {
                    foreach (var current in currents)
                    {
                        var type = current.GetInterfaces().FirstOrDefault(p =>
                            typeof(IDependency).IsAssignableFrom(p)
                            && !string.Equals(p.Name, "IDependency", StringComparison.CurrentCultureIgnoreCase)
                            && p.Name.IndexOf("IBase", StringComparison.CurrentCultureIgnoreCase) < 0);

                        if (type != null)
                        {
                            builder.RegisterType(current).As(type);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine("RegisterDependency : " + ex.ToString());
            }
        }
    }
}
