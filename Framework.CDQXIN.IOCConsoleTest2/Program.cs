using Autofac;
using Framework.CDQXIN.IOC;
using Framework.CDQXIN.IOC.StudentService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.CDQXIN.IOCConsoleTest2
{
    class Program
    {
        static void Main(string[] args)
        {
            CustomizeContainer.Init();
            // 创建一个生命域, 解析对象，使用完后, 自动释放掉所有解析资源
            using (ILifetimeScope scope = CustomizeContainer.Instance.BeginLifetimeScope())
            {
                IStudentRepository stuService = CustomizeContainer.Instance.Resolve<StudentRepository>();
                string nameStr = stuService.SayHello("jack");
                Console.WriteLine(nameStr);
                Console.ReadLine();
            }
        }
    }
}
