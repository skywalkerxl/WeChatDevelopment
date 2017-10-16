using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WxApi.ReceiveEntity
{
    public class JsApiTicket:ErrorEntity
    {
        /// <summary>
        /// ticket
        /// </summary>
        public string ticket { get; set; }

        public int _expires_in;
        /// <summary>
        /// 有效期时间，单位为秒
        /// </summary>
        public int expires_in
        {
            get { return _expires_in; }
            set
            {
                // 获取失效时间
                expires_time = DateTime.Now.AddSeconds(value);
                _expires_in = value;
            }
        }
        /// <summary>
        /// 失效时间
        /// </summary>
        public DateTime expires_time { get; set; }
    }
}
