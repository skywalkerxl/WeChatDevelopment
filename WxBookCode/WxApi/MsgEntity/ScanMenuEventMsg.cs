using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WxApi.MsgEntity
{
    public class ScanMenuEventMsg:BaseMenuEventMsg
    {
        /// <summary>
        /// 扫描类型，一般是qrcode
        /// </summary>
        public string ScanType { get; set; }
        /// <summary>
        /// 扫描结果，即二维码对应的字符串信息
        /// </summary>
        public string ScanResult { get; set; }
    }
}
