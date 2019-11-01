using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Compilation;

namespace Framework.CDQXIN.IOC
{
    /// <summary>
    /// 注入类型
    /// </summary>
    public class TypeFinder
    {

        //FinderType
        private const string IocAssembliesConfigName = "ioc:assemblies";

        /// <summary>
        /// 待注入集合
        /// </summary>
        public readonly List<Assembly> Assemblies = new List<Assembly>();

        /// <summary>
        /// 
        /// </summary>
        public TypeFinder()
        {
            this.GetAssembliesConfiguration();
        }

        /// <summary>
        /// 获得程序集
        /// <remarks>
        /// 根据配置的部分程序集名
        /// </remarks>
        /// </summary>
        private void GetAssembliesConfiguration()
        {
            string iocassembliesname = ConfigurationManager.AppSettings[IocAssembliesConfigName] ?? "";
            string noiocassembliesname = ConfigurationManager.AppSettings["!" + IocAssembliesConfigName] ?? "";

            string[] noiocassembliesnames = noiocassembliesname.Split(new[] { ";", "；", ",", "，" }, StringSplitOptions.RemoveEmptyEntries);

            if (!string.IsNullOrEmpty(iocassembliesname))
            {
                string[] source = iocassembliesname.Split(new[] { ";", "；", ",", "，" }, StringSplitOptions.RemoveEmptyEntries);

                var current = HttpContext.Current;

                if (current != null)
                {
                    //web
                    foreach (var enumerator in BuildManager.GetReferencedAssemblies())
                    {
                        Assembly assembly = (Assembly)enumerator;


                        if (assembly.FullName.Contains(".MvcExtensions"))
                        {
                            continue;
                        }

                        if (noiocassembliesnames.Any(e => assembly.FullName.Contains(e)))
                        {
                            continue;
                        }

                        if (source.Any(p => assembly.FullName.Contains(p)))
                        {
                            this.Assemblies.Add(assembly);
                        }
                    }
                }
                else
                {
                    //win
                    var assemblies = GetAssemblies();

                    foreach (var assembly in assemblies)
                    {
                        if (assembly.FullName.Contains("Framework.MvcExtensions"))
                        {
                            continue;
                        }

                        if (noiocassembliesnames.Any(e => assembly.FullName.Contains(e)))
                        {
                            continue;
                        }

                        if (source.Any(p => assembly.FullName.Contains(p)))
                        {
                            this.Assemblies.Add(assembly);
                        }
                    }
                }
            }
            else
            {

                if (HttpContext.Current != null)
                {
                    //web
                    foreach (var enumerator in BuildManager.GetReferencedAssemblies())
                    {
                        Assembly assembly = (Assembly)enumerator;

                        //过滤.MVC扩展组件
                        if (assembly.FullName.Contains("Framework.MvcExtensions"))
                        {
                            continue;
                        }

                        if (noiocassembliesnames.Any(e => assembly.FullName.Contains(e)))
                        {
                            continue;
                        }

                        this.Assemblies.Add(assembly);
                    }
                }
                else
                {
                    var assemblies = GetAssemblies();

                    foreach (var assembly in assemblies)
                    {
                        //过滤.MVC扩展组件
                        if (assembly.FullName.Contains("Framewor.MvcExtensions"))
                        {
                            continue;
                        }

                        if (noiocassembliesnames.Any(e => assembly.FullName.Contains(e)))
                        {
                            continue;
                        }

                        this.Assemblies.Add(assembly);
                    }
                }
            }
        }

        /// <summary>
        /// Get Base Directory Assemblies
        /// </summary>
        /// <returns></returns>
        private IEnumerable<Assembly> GetAssemblies()
        {
            var assemblies = from file in Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory)
                             where Path.GetExtension(file).ToLower() == ".dll" || Path.GetExtension(file).ToLower() == ".exe"
                             select Assembly.LoadFrom(file);

            return assemblies;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public virtual IEnumerable<Type> FindClassesOfType<T>()
        {
            List<Type> list = new List<Type>();
            Type typeFromHandle = typeof(T);
            if (this.Assemblies != null && this.Assemblies.Count > 0)
            {
                foreach (Assembly item in this.Assemblies)
                {
                    Type[] types = item.GetTypes();
                    if (types.Length > 0)
                    {
                        Type[] array = types;
                        for (int i = 0; i < array.Length; i++)
                        {
                            Type type = array[i];
                            if (typeFromHandle.IsAssignableFrom(type) || (typeFromHandle.IsGenericTypeDefinition && this.DoesTypeImplementOpenGeneric(type, typeFromHandle)))
                            {
                                if (!type.IsInterface)
                                {
                                    if (type.IsClass && !type.IsAbstract)
                                    {
                                        list.Add(type);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return list;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="openGeneric"></param>
        /// <returns></returns>
        protected virtual bool DoesTypeImplementOpenGeneric(Type type, Type openGeneric)
        {
            try
            {
                Type genericTypeDefinition = openGeneric.GetGenericTypeDefinition();
                Type[] array = type.FindInterfaces((Type objType, object objCriteria) => true, null);
                for (int i = 0; i < array.Length; i++)
                {
                    Type type2 = array[i];
                    if (type2.IsGenericType)
                    {
                        bool flag = genericTypeDefinition.IsAssignableFrom(type2.GetGenericTypeDefinition());
                        var result = flag;
                        return result;
                    }
                }
            }
            catch
            {
                // ignored
            }
            return false;
        }
    }
}
