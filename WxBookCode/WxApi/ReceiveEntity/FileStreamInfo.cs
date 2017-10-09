using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace WxApi.ReceiveEntity
{
    public class FileStreamInfo : MemoryStream
    {
        /// <summary>
        /// 文件名
        /// </summary>
        public string FileName { get; set; }
    }
}
