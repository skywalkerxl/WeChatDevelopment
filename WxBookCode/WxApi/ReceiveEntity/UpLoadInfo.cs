﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WxApi.ReceiveEntity
{
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
