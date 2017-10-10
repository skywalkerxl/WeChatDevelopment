using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WxApi.ReceiveEntity;

namespace WxApi.MsgEntity
{
    public class TemplateMsg:ErrorEntity
    {
        public string msgid { get; set; }
    }
}
