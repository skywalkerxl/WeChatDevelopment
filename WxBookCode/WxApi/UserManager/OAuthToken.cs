﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WxApi.ReceiveEntity;

namespace WxApi.UserManager
{
    public class OAuthToken : ErrorEntity
    {
        /// <summary>
        /// 网页授权接口调用凭证。注意：此access_token与基础支持的access_token不同
        /// </summary>
        public string access_token { get; set; }

        private int _expires_in;

        /// <summary>
        /// 接口调试凭证超时时间，单位(秒)
        /// </summary>
        public int expires_in 
        {
            get { return _expires_in; }
            set
            {
                expires_time = DateTime.Now.AddSeconds(value);
                _expires_in = value;
            }
        }
        /// <summary>
        /// 用户刷新access_token
        /// </summary>
        public string refresh_token { get; set; }
        /// <summary>
        /// 用户唯一标识，请注意，在未关注公众号时，用户访问公众号的网页，也会产生一个用户和公众号唯一的openid
        /// </summary>
        public string openid { get; set; }
        /// <summary>
        /// 用户授权的作用于，使用逗号(,)分隔
        /// </summary>
        public string scope { get; set; }
        
        public DateTime expires_time { get; set; }
        /// <summary>
        /// 只有在用户将公众号绑定到微信到微信开放平台账号之后，才会出现该字段
        /// </summary>
        public string unionid { get; set; }
    }
}
