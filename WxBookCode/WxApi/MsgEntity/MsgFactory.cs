using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace WxApi.MsgEntity
{
    public class MsgFactory
    {
        private static List<BMsg> _queue;

        /// <summary>
        /// 加载消息，并绑定各消息类型的处理程序
        /// </summary>
        /// <param name="mheList">消息处理程序列表</param>
        public static void LoadMsg(List<MsgHandlerEntity> mheList)
        {
            // 判断队列是否为空，为空则实例化
            if(_queue == null)
            {
                _queue = new List<BMsg>();
            }
            else
            {
                //保留20秒内未响应的消息
                _queue = _queue.Where(q => q.CreateTime.AddSeconds(20) > DateTime.Now).ToList();
            }
            // 获取数据包
            string postStr = Utils.GetRequestData();
            System.Diagnostics.Debug.Write("\n************************ This is postStr start *************************\n");
            System.Diagnostics.Debug.Write(postStr);
            System.Diagnostics.Debug.Write("\n************************  This is postStr end  *************************\n");
            XElement xdoc = XElement.Parse(postStr);
            var msgtype = xdoc.Element("MsgType").Value.ToUpper();
            var FromUserName = xdoc.Element("FromUserName").Value;
            var CreateTime = xdoc.Element("CreateTime").Value;
            // 获取消息类型
            MsgType type = (MsgType)Enum.Parse(typeof(MsgType), msgtype);
            // 如果不是事件类型
            if(type != MsgType.EVENT)
            {
                // 判断队列中是否已经存在该消息。如果不存在，则将此消息加入队列中；否则返回null
                var MsgId = xdoc.Element("MsgId").Value;
                if(_queue.FirstOrDefault(m => m.MsgFlag == MsgId) == null)
                {
                    _queue.Add(new BMsg
                        {
                            CreateTime = DateTime.Now,
                            FromUser = FromUserName,
                            MsgFlag = MsgId
                        });
                }
                else
                {
                    return;
                }
            }
            // 如果是事件类型
            else
            {
                // 判断队列中是否已经存在该消息。如果不存在，则将此消息加入队列中；否则返回null
                if(_queue.FirstOrDefault(m => m.MsgFlag == CreateTime && m.FromUser == FromUserName) == null)
                {
                    _queue.Add(new BMsg
                        {
                        CreateTime = DateTime.Now,
                        FromUser = FromUserName,
                        MsgFlag = CreateTime
                        });
                }
                else
                {
                    return;
                }
            }
            //消息实体对象
            BaseMsg msg = null;
            // 事件类型，默认为NOEVENT，即非事件类型
            EventType eventtype = EventType.NOEVENT;
            switch(type)
            {
                case MsgType.TEXT: msg = Utils.ConvertObj<TextMsg>(postStr); break;
                case MsgType.IMAGE: msg = Utils.ConvertObj<ImgMsg>(postStr); break;
                case MsgType.VIDEO: msg = Utils.ConvertObj<VideoMsg>(postStr); break;
                case MsgType.VOICE: msg = Utils.ConvertObj<VoiceMsg>(postStr); break;
                case MsgType.LINK: msg = Utils.ConvertObj<LinkMsg>(postStr); break;
                case MsgType.LOCATION: msg = Utils.ConvertObj<LocationMsg>(postStr); break;
                case MsgType.EVENT:
                    {
                        eventtype = (EventType)Enum.Parse(typeof(EventType), xdoc.Element("Event").Value.ToUpper());
                        switch(eventtype)
                        {
                            // 订阅与取消订阅事件
                            case EventType.UNSUBSCRIBE: msg = Utils.ConvertObj<SubEventMsg>(postStr); break;
                            case EventType.LOCATION: msg = Utils.ConvertObj<LocationEventMsg>(postStr); break;
                            case EventType.MASSSENDJOBFINISH: msg = Utils.ConvertObj<GroupJobEventMsg>(postStr); break;
                            default:
                                msg = Utils.ConvertObj<EventMsg>(postStr); break;
                        }
                    }
                    break;
                default: msg = Utils.ConvertObj<BaseMsg>(postStr); break;
            }

            // 根据消息类型，获取回调程序
            var ac = GetAction(mheList, type, eventtype);
            // 如果回调程序存在，并且消息实体转换成功，则执行回调程序
            if(ac != null && msg != null)
            {
                ac(msg);
            }
        }

        /// <summary>
        /// 根据消息类型，获取回调程序
        /// </summary>
        /// <param name="mheList">回调委托列表</param>
        /// <param name="msgType">消息类型</param>
        /// <param name="eventType">回掉委托</param>
        /// <returns>回调委托</returns>
        private static Action<BaseMsg> GetAction(List<MsgHandlerEntity> mheList, MsgType msgType, EventType eventType)
        {
            MsgHandlerEntity temp = mheList.FirstOrDefault(mhe =>
                {
                    if (msgType != MsgType.EVENT)
                        return mhe.MsgType == msgType;
                    return mhe.EventType == eventType;
                });
            return temp.Action;
        }
    }
}
