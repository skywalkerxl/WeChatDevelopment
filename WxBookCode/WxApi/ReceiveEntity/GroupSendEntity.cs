using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WxApi.ReceiveEntity
{
    public class GroupSendEntity:ErrorEntity
    {
        /// <summary>
        /// 消息ID
        /// </summary>
        public string media_id { get; set; }
    }
}
