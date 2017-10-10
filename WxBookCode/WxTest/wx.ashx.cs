using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WxApi;
using WxApi.MsgEntity;
using WxApi.SendEntity;

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

            
            
            if (context.Request.HttpMethod=="GET")
            {   
                BaseServices.ValidUrl("skywalkerxl");
                
                #region 创建菜单按钮
                string appid = "wxf50808b364418ffb";
                string appSerect = "bfaf8363dc64787091b3bbb7740dcf44";
                var accessToken = AccessTokenBox.GetTokenValue(appid, appSerect);
                var child1 = new List<BaseMenu>();
                var child2 = new List<BaseMenu>();
                var child3 = new List<BaseMenu>();
                var basebtn = new List<BaseMenu>();

                child1.Add(new BaseMenu
                {
                    key = "我是click按钮",
                    name = "Click按钮",
                    type = MenuType.click
                });
                child1.Add(new BaseMenu
                {
                    key = "我是选择地理位置按钮",
                    name = "选择地理位置",
                    type = MenuType.location_select
                });
                child1.Add(new BaseMenu
                {
                    url = "http://www.baidu.com",
                    name = "跳转链接",
                    type = MenuType.view
                });

                child2.Add(new BaseMenu
                {
                    key = "我是扫码事件按钮",
                    name = "扫码推事件",
                    type = MenuType.scancode_push
                });
                child2.Add(new BaseMenu
                {
                    key = "我是扫码推事件按钮且弹出消息接收中",
                    name = "扫码等待",
                    type = MenuType.scancode_waitmsg
                });

                child3.Add(new BaseMenu
                {
                    key = "我是拍照或相册按钮",
                    name = "拍照或相册",
                    type = MenuType.pic_photo_or_album
                });

                child3.Add(new BaseMenu
                {
                    key = "我是系统拍照",
                    name = "系统拍照",
                    type = MenuType.pic_sysphoto
                });
                child3.Add(new BaseMenu
                {
                    key = "我是弹出微信相册按钮",
                    name = "微信相册",
                    type = MenuType.pic_weixin
                });

                basebtn.Add(new BaseMenu
                {
                    name = "常用菜单",
                    sub_button = child1
                });
                basebtn.Add(new BaseMenu
                {
                    name = "扫码",
                    sub_button = child2
                });
                basebtn.Add(new BaseMenu
                {
                    name = "发图",
                    sub_button = child3
                });

                var ret = WxApi.Menu.Create(new MenuEntity { button = basebtn }, accessToken);
                #endregion
            }

            else
            {
                //var xml = Utils.GetRequestData();
                //System.Diagnostics.Debug.WriteLine("----------------------Receive the xml message Start----------------------");
                //System.Diagnostics.Debug.WriteLine(xml);
                //System.Diagnostics.Debug.WriteLine("---------------------- Receive the xml message End ----------------------");
                //context.Response.Write("");

                // 判断MsgHandlerEntities是否为空，如果是，则实例化，并将各个消息类型的处理程序实体添加到此列表中
                // 因为MsgHandlerEntities是静态变量，之需绑定一次即可，免去了重复绑定的性能损耗
                
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

                    #region 图片事件处理绑定
                    
                    MsgHandlerEntity.MsgHandlerEntities.Add(new MsgHandlerEntity
                    {
                        MsgType = MsgType.EVENT,
                        EventType = EventType.PIC_WEIXIN,
                        Action = PicWeiXinHandler
                    });
                    #endregion

                    #region 订阅事件处理绑定
                    MsgHandlerEntity.MsgHandlerEntities.Add(new MsgHandlerEntity
                    {
                        MsgType = MsgType.EVENT,
                        EventType = EventType.SUBSCRIBE,
                        Action = SubscribeEventHandler
                    });
                    #endregion
                    /***
                     * 
                     * 
                     *   其它一系列的事件处理
                     * 
                     * 
                     ***/
                    // TODO
                    #region 模板消息事件处理
                    MsgHandlerEntity.MsgHandlerEntities.Add(new MsgHandlerEntity
                    {
                        MsgType = MsgType.EVENT,
                        EventType = EventType.TEMPLATESENDJOBFINISH,
                        Action = TEMPLATESENDJOBFINISHEventHandler
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
            if( msg.Content == "音乐" )
            {
                msg.ResText("回复音乐");
            }
            else if (msg.Content == "图片")
            {
                msg.ResPicture("h8WC_sN8lManF1ZHbetiuS0vu9t3dLCjsncLiiJ63qiCyo_RX4I862F0NvjI04aY");
            }
            else
            {
                msg.ResText("服务器收到你发送的消息了,你发送的内容是：\r\n" + msg.Content + "\r\n点击<a href=\"http://xulang.site\">这里</a>,我们有话跟你说哦");
            }
            
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

        #region 图片事件处理程序
        private void PicWeiXinHandler(BaseMsg baseMsg)
        {
            // TODO
        }
        #endregion

        #region 地理位置事件处理程序
        private void LocationEventHandler(BaseMsg baseMsg)
        {
            // TODO
        }
        #endregion

        #region 订阅事件处理程序
        private void SubscribeEventHandler(BaseMsg baseMsg)
        {
            // TODO
        }
        #endregion

        #region 点击事件处理程序
        private void ClickEventHandler(BaseMsg baseMsg)
        {
            // TODO
        }
        #endregion

        #region 浏览事件处理程序
        private void ViewEventHandler(BaseMsg baseMsg)
        {
            // TODO
        }
        #endregion

        #region 扫码推送事件处理程序
        private void ScanCodePushEventHandler(BaseMsg baseMsg)
        {
            // TODO
        }
        #endregion

        #region 扫码等待事件处理程序
        private void ScanCOdeWatingEventHandler(BaseMsg baseMsg)
        { 
            // TODO
        }
        #endregion

        #region 系统照相事件处理程序
        private void PicSysphotoEventHandler(BaseMsg baseMsg)
        {
            // TODO
        }
        #endregion

        #region 系统照相或相册事件处理程序
        private void PicPhotoOrAlbumEventHandler(BaseMsg baseMsg)
        {
            // TODO
        }
        #endregion

        #region 弹出地理位置选择器事件处理程序
        private void LocationSelectEventHandler(BaseMsg baseMsg)
        {
            // TODO
        }
        #endregion

        #region 群发消息处理程序
        private void GroupJobHandler(BaseMsg baseMsg)
        {
            // TODO
        }
        #endregion

        #region 模板消息事件处理程序
        private void TEMPLATESENDJOBFINISHEventHandler(BaseMsg baseMsg)
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