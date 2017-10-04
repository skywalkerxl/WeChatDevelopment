using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WxApi.ReceiveEntity
{
    public class IpEntity:ErrorEntity
    {
        /// <summary>
        /// IP列表
        /// </summary>
        public string[] ip_list { get; set; }
    }
}
