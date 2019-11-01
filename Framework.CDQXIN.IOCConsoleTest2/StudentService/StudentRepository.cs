using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.CDQXIN.IOC.StudentService
{
    public class StudentRepository: IStudentRepository
    {
        public string SayHello(string name)
        {
            return "hello," + name;
        }
    }
}
