using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WxApi.ReceiveEntity
{
    /// <summary>
    /// 错误信息实体
    /// </summary>
    public class ErrorEntity
    {
        public int _errCode { get; set; }
        /// <summary>
        /// 错误编码
        /// </summary>
        public int ErrCode
        {
            get { return _errCode; }
            set
            {
                _errCode = value;
                // 根据错误码，从错误列表中找到错误信息，并给Errr=Description赋值
                ErrDescription = ErrList.FirstOrDefault(e=>e.Key==value).Value;
            }
        } 
        /// <summary>
        /// 错误描述
        /// </summary>
        public string ErrDescription { get; set; }

        private static Dictionary<int, string> _errorDic;

        public static Dictionary<int, string> ErrList
        {
            get 
            {
                if (_errorDic != null && _errorDic.Count > 0)
                    return _errorDic;
                _errorDic = new Dictionary<int, string>();
                var temp = Code.CodeInfo.Split(new char[]{'\r','\n'},
                    StringSplitOptions.RemoveEmptyEntries);

                foreach(var strArr in temp.Select(str => str.Split('\t')))
                {
                    _errorDic.Add(int.Parse(strArr[0]), strArr[1]);
                }
                return _errorDic;
            }
        }
    }

    /// <summary>
    /// 错误描述
    /// </summary>
   
}
