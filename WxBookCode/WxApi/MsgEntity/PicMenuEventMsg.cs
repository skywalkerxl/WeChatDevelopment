using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WxApi.MsgEntity
{
    public class PicMenuEventMsg : BaseMenuEventMsg
    {
        /// <summary>
        /// 发送的图片数量
        /// </summary>
        public int Count { get; set; }
        /// <summary>
        /// 图片的MD5集合
        /// </summary>
        public List<string> PicMd5SumList { get; set; }

    }
}
