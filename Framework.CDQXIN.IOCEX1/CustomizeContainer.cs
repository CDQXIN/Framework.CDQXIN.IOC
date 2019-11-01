using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Framework.CDQXIN.IOCEX1
{
    public class CustomizeContainer
    {
        /// <summary>
        /// 控制台程序容器
        /// </summary>
        public static class CustomizeContainer
        {
            /// <summary>
            /// 容器
            /// </summary>
            public static IContainer Instance;

            /// <summary>
            /// 初始化容器
            /// </summary>
            /// <param name="func">委托</param>
            /// <returns></returns>
            public static void Init(Func<ContainerBuilder, ContainerBuilder> func = null)
            {
                //新建容器构建器，用于注册组件和服务
                var builder = new ContainerBuilder();
                //注册组件
                BuildContainer(builder);
                func?.Invoke(builder);
                //利用构建器创建容器
                Instance = builder.Build();
            }

            /// <summary>
            /// 自定义注册
            /// </summary>
            /// <param name="builder"></param>
            public static void BuildContainer(ContainerBuilder builder)
            {
                BuildContainerFunc(builder);
            }

            /// <summary>
            /// 方法8：通过反射程序集
            /// </summary>
            /// <param name="builder"></param>
            public static void BuildContainerFunc(ContainerBuilder builder)
            {
                Assembly[] assemblies = ReflectionHelper.GetAllAssemblies();

                builder.RegisterAssemblyTypes(assemblies);//程序集内所有具象类（concrete classes）
                                                          //.Where(cc => cc.Name.EndsWith(".IOC") |//筛选
                                                          //            cc.Name.EndsWith(".IOC"))
                                                          //.PublicOnly()//只要public访问权限的
                                                          //.Where(cc => cc.IsClass)//只要class型（主要为了排除值和interface类型）
                                                          //                        //.Except<TeacherRepository>()//排除某类型
                                                          //                        //.As(x=>x.GetInterfaces()[0])//反射出其实现的接口，默认以第一个接口类型暴露
                                                          //.AsImplementedInterfaces();//自动以其实现的所有接口类型暴露（包括IDisposable接口）


            }
        }

        public class ReflectionHelper
        {
            /// <summary>
            ///  获取Asp.Net FrameWork项目所有程序集
            /// </summary>
            /// <returns></returns>
            public static Assembly[] GetAllAssemblies()
            {
                //1.获取当前程序集(Ray.EssayNotes.AutoFac.Infrastructure.Ioc)所有引用程序集
                Assembly executingAssembly = Assembly.GetExecutingAssembly();//当前程序集
                List<Assembly> assemblies = executingAssembly.GetReferencedAssemblies()
                    .Select(Assembly.Load)
                    .Where(m => m.FullName.Contains("Framework.CDQXIN"))
                    .ToList();
                //2.获取程序启动入口程序集（比如Ray.EssayNotes.AutoFac.ConsoleApp）
                Assembly assembly = Assembly.GetEntryAssembly();
                assemblies.Add(assembly);
                return assemblies.ToArray();
            }

            /// <summary>
            ///  获取Asp.Net FrameWork Web项目所有程序集
            /// </summary>
            /// <returns></returns>
            public static Assembly[] GetAllAssembliesWeb(object BuildManager)
            {
                Assembly[] assemblies = BuildManager
                    .GetReferencedAssemblies()
                    .Cast<Assembly>()
                    .Where(x => x.FullName.Contains("Framework.CDQXIN"))
                    .ToArray();
                return assemblies;
            }
        }
    }
}
