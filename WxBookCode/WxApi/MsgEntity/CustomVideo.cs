using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WxApi.MsgEntity
{
    public class CustomVideo
    {
        /// <summary>
        /// 媒体ID
        /// </summary>
        public string media_id { get; set; }
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
