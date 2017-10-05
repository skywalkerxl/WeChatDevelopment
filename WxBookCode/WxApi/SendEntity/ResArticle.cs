﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WxApi.SendEntity
{
    public class ResArticle
    {
        /// <summary>
        /// 消息的标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 消息的描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 图片链接，支持JPG、PNG格式，较好的效果为360px * 200px、小图
        /// </summary>
        public string PicUrl { get; set; }
        /// <summary>
        /// 单机图文消息跳转链接
        /// </summary>
        public string Url { get; set; }
    }
}
