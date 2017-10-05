using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WxApi.SendEntity
{
    public class ResMusic
    {
        /// <summary>
        /// 音乐标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 音乐描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 音乐链接
        /// </summary>
        public string MusicUrl { get; set; }
        /// <summary>
        /// 高质量音乐(Wifi环境优先使用该链接播放音乐)
        /// </summary>
        public string HQMusicUrl { get; set; }
        /// <summary>
        /// 缩略图的媒体ID，通过上传多媒体文件得到的ID
        /// </summary>
        public string ThumbMediaId { get; set; }
    }
}
