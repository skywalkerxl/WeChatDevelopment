﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WxApi
{
    /// <summary>
    /// 微信接入参数
    /// </summary>
    public class EnterParam
    {
        /// <summary>
        /// 是否加密
        /// </summary>
        public bool IsAes { get; set; }
        /// <summary>
        /// 接入token
        /// </summary>
        public string token { get; set; }
        /// <summary>
        /// 微信AppID
        /// </summary>
        public string appid { get; set; }
        /// <summary>
        /// 加密密钥
        /// </summary>
        public string EncodingAESKey { get; set; }
    }
}
