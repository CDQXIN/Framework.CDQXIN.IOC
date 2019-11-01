using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.CDQXIN.IOC
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Serializable]
    public class ResultInfo<T>
    {
        /// <summary>
        /// 
        /// </summary>
        public bool HasError
        {
            get;
            private set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string ErrMessage
        {
            get;
            private set;
        }

        /// <summary>
        /// 
        /// </summary>
        public T ObjectValue
        {
            get;
            private set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string Hresult
        {
            get;
            private set;
        }

        /// <summary>
        /// 
        /// </summary>
        public ResultInfo()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hasError"></param>
        /// <param name="errMessage"></param>
        /// <param name="thresult"></param>
        /// <param name="objectValue"></param>
        public ResultInfo(bool hasError, string errMessage, string thresult, T objectValue)
        {
            this.HasError = hasError;
            this.ErrMessage = errMessage;
            this.Hresult = thresult;
            this.ObjectValue = objectValue;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string[] strs = new string[9];
            strs[0] = "ResultInfo:[HasError=";
            strs[1] = this.HasError.ToString();
            strs[2] = "] [ErrMessage=";
            strs[3] = this.ErrMessage;
            strs[4] = "] [ObjectValue=";
            int num = 5;
            string str;
            if (this.ObjectValue != null)
            {
                T objectValue = this.ObjectValue;
                str = objectValue.ToString();
            }
            else
            {
                str = "NULL";
            }
            strs[num] = str;
            strs[6] = "] [HRESULT=";
            strs[7] = this.Hresult;
            strs[8] = "]";
            return string.Concat(strs);
        }
    }
}
