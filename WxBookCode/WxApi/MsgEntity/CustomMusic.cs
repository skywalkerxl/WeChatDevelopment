using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WxApi.MsgEntity
{
    public class CustomMusic
    {
        /// <summary>
        /// 音乐链接
        /// </summary>
        public string musicurl { get; set; }
        /// <summary>
        /// 高品质音乐链接
        /// </summary>
        public string hqmusicurl { get; set; }
        /// <summary>
        /// 缩略图ID
        /// </summary>
        public string thumb_media_id { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string description { get; set; }
    }
}
