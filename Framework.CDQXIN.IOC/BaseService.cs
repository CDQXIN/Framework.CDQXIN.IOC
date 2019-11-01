using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.CDQXIN.IOC
{
    /// <summary>
    /// 基础服务
    /// </summary>
    public abstract class BaseService : MarshalByRefObject, IDisposable
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override object InitializeLifetimeService()
        {
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="retObj"></param>
        /// <returns></returns>
        public ResultInfo<int> GetIntResult(object retObj)
        {
            int num = -9999;
            if (retObj != null)
            {
                if (!int.TryParse(retObj.ToString(), out num))
                {
                    num = -9999;
                }
            }
            return new ResultInfo<int>(num <= 0, "", num.ToString(), num);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="retObj"></param>
        /// <returns></returns>
        public ResultInfo<long> GetLongResult(object retObj)
        {
            long num = -9999L;
            if (retObj != null)
            {
                if (!long.TryParse(retObj.ToString(), out num))
                {
                    num = -9999L;
                }
            }
            return new ResultInfo<long>(num <= 0L, "", num.ToString(), num);
        }

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(true);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
        }
    }
}
