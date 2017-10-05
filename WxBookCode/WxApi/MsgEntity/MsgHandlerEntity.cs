using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WxApi.MsgEntity
{
    public class MsgHandlerEntity
    {
        public MsgType MsgType { get; set; }

        public EventType EventType { get; set; }

        public Action<BaseMsg> Action { get; set; }

        public static List<MsgHandlerEntity> MsgHandlerEntities;
    }
}
