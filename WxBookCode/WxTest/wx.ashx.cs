using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WxApi;
using WxApi.MsgEntity;

namespace WxTest
{
    /// <summary>
    /// wx 的摘要说明
    /// </summary>
    public class wx : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {

            // 测试时暂不使用
            /*var ip = context.Request.UserHostAddress;
            var ipEntity = BaseServices.GetIpArray("你的access_token");
            if(ipEntity!=null && !ipEntity.ip_list.Contains(ip))
            {
                context.Response.Write("非法请求");
                return;
            }*/
            // var url = context.Request.RawUrl;
            
            
            if(context.Request.HttpMethod=="GET")
            {   
                BaseServices.ValidUrl("skywalkerxl");
            }

            else
            {
                //var xml = Utils.GetRequestData();
                //System.Diagnostics.Debug.WriteLine("----------------------Receive the xml message Start----------------------");
                //System.Diagnostics.Debug.WriteLine(xml);
                //System.Diagnostics.Debug.WriteLine("---------------------- Receive the xml message End ----------------------");
                //context.Response.Write("");

                // 判断MsgHandlerEntities是否为空，如果是，则实例化，并将各个消息类型的处理程序实体添加到此列表中
                // 因为MsgHandlerEntities是静态变量，之需绑定一次即可，免去了重读绑定的性能损耗
                
                if(MsgHandlerEntity.MsgHandlerEntities == null)
                {
                    MsgHandlerEntity.MsgHandlerEntities = new List<MsgHandlerEntity>();

                    #region 文本消息绑定处理

                    MsgHandlerEntity.MsgHandlerEntities.Add(new MsgHandlerEntity
                        {
                            MsgType = MsgType.TEXT,
                            Action = TextHandler
                        });
                    #endregion

                    #region 图片消息处理绑定

                    MsgHandlerEntity.MsgHandlerEntities.Add(new MsgHandlerEntity
                        {
                            MsgType = MsgType.IMAGE,
                            Action = ImgHandler
                        });
                    #endregion

                    #region 语音消息处理绑定

                    MsgHandlerEntity.MsgHandlerEntities.Add(new MsgHandlerEntity
                        {
                            MsgType = MsgType.VOICE,
                            Action = VoiceHandler
                        });
                    #endregion

                    #region 视频消息处理绑定

                    MsgHandlerEntity.MsgHandlerEntities.Add(new MsgHandlerEntity
                        {
                            MsgType = MsgType.VIDEO,
                            Action = VideoHandler
                        });
                    #endregion

                    #region 地理位置消息处理绑定

                    MsgHandlerEntity.MsgHandlerEntities.Add(new MsgHandlerEntity
                        {
                            MsgType = MsgType.LOCATION,
                            Action = LocationHandler
                        });
                    #endregion

                    #region 链接消息处理绑定

                    MsgHandlerEntity.MsgHandlerEntities.Add(new MsgHandlerEntity
                        {
                            MsgType = MsgType.LINK,
                            Action = linkHandler
                        });
                    #endregion

                    #region 群发消息处理绑定
                    MsgHandlerEntity.MsgHandlerEntities.Add(new MsgHandlerEntity
                    {
                        MsgType = MsgType.EVENT,
                        EventType = EventType.MASSSENDJOBFINISH,
                        Action = GroupJobHandler
                    });
                    #endregion

                }
                MsgFactory.LoadMsg(MsgHandlerEntity.MsgHandlerEntities);
            }
        }

        #region 文本消息处理程序
        /// <summary>
        /// 文本消息处理程序
        /// </summary>
        /// <param name="baseMsg"></param>
        private void TextHandler(BaseMsg baseMsg)
        {
            var msg = (TextMsg)baseMsg;
            Utils.OutPrint(msg.Content);
            msg.ResText("服务器收到你发送的消息了,你发送的内容是：\r\n" + msg.Content + "\r\n点击<a href=\"http://xulang.site\">这里</a>,我们有话跟你说哦");
            //msg.ResText("服务器收到了你发送的消息了");
        
        }
        #endregion

        #region 图片消息处理程序
        /// <summary>
        /// 图片消息处理程序
        /// </summary>
        /// <param name="baseMsg"></param>
        private void ImgHandler(BaseMsg baseMsg)
        {
            var msg = (ImgMsg)baseMsg;
            Utils.OutPrint(msg.PicUrl);
        }
        #endregion

        #region 视频消息处理程序
        /// <summary>
        /// 视频消息处理程序
        /// </summary>
        /// <param name="baseMsg"></param>
        private void VideoHandler(BaseMsg baseMsg)
        {
            var msg = (VideoMsg)baseMsg;
            Utils.OutPrint(msg.ThumbMediaId);
        }
        #endregion

        #region 语音消息处理程序
        /// <summary>
        /// 语音消息处理程序
        /// </summary>
        /// <param name="baseMsg"></param>
        private void VoiceHandler(BaseMsg baseMsg)
        {
            var msg = (VoiceMsg)baseMsg;
            Utils.OutPrint(msg.MediaId);
        }
        #endregion

        #region 链接消息处理程序
        /// <summary>
        /// 链接消息处理程序
        /// </summary>
        /// <param name="baseMsg"></param>
        private void linkHandler(BaseMsg baseMsg)
        {
            var msg = (LinkMsg)baseMsg;
            Utils.OutPrint(msg.Description);
        }
        #endregion

        #region 地理位置消息处理程序
        /// <summary>
        /// 地理位置消息处理程序
        /// </summary>
        /// <param name="baseMsg"></param>
        private void LocationHandler(BaseMsg baseMsg)
        {
            var msg = (LocationMsg)baseMsg;
            Utils.OutPrint(msg.Label);
        }
        #endregion

        #region 群发消息处理程序
        private void GroupJobHandler(BaseMsg baseMsg)
        {
            // TODO
        }
        #endregion
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}