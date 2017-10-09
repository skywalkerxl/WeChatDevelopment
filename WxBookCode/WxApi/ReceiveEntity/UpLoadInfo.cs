using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WxApi.ReceiveEntity
{
    public enum MaterialType
    {
        /// <summary>
        /// 图片(image): 2MB,支持bmp/png/jpeg/jpg/gif 
        /// </summary>
        image,
        /// <summary>
        /// 语音(voice): 5MB,播放长度不超过60s,支持mp3/wma/wav/amr格式
        /// </summary>
        voice,
        /// <summary>
        /// 视频(video): 20MB,支持rm/rmvb/wmv/avi/mpg/mpeg/mp4格式
        /// </summary>
        video,
        /// <summary>
        /// 缩略图(thumb)， 64KB,支持jpg格式
        /// </summary>
        thumb,
        /// <summary>
        /// 图文
        /// </summary>
        news
    }

    public class UpLoadInfo:ErrorEntity
    {
        /// <summary>
        /// 媒体文件类型，分别有图片(image)、语音(voice)、视频(video)和缩略图(thumb 主要用于视频和音乐格式的缩略图)
        /// </summary>
        public string type { get; set; }

        /// <summary>
        /// 媒体文件上传之后，获取时的唯一标识
        /// </summary>
        public string media_id { get; set; }

        /// <summary>
        /// 媒体文件上传时间戳
        /// </summary>
        public string created_at { get; set; }
    }
}
