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

                    #region 点击事件处理程序
                    MsgHandlerEntity.MsgHandlerEntities.Add(new MsgHandlerEntity
                        {
                            MsgType = MsgType.EVENT,
                            EventType = EventType.CLICK,
                            Action = ClickEventHandler
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
                    #region 获取地理位置信息事件处理绑定
                    MsgHandlerEntity.MsgHandlerEntities.Add(new MsgHandlerEntity
                    {
                        MsgType = MsgType.EVENT,
                        EventType = EventType.LOCATION,
                        Action = LocationEventHandler
                    });
                    #endregion

                    #region 浏览事件处理程序绑定
                    MsgHandlerEntity.MsgHandlerEntities.Add(new MsgHandlerEntity
                    {
                        MsgType = MsgType.EVENT,
                        EventType = EventType.VIEW,
                        Action = ViewEventHandler
                    });
                    #endregion

                    #region 扫码推送事件处理程序绑定
                    MsgHandlerEntity.MsgHandlerEntities.Add(new MsgHandlerEntity
                    {
                        MsgType = MsgType.EVENT,
                        EventType = EventType.SCANCODE_PUSH,
                        Action = ScanCodePushEventHandler
                    });
                    #endregion

                    #region 扫码等待事件处理程序绑定
                    MsgHandlerEntity.MsgHandlerEntities.Add(new MsgHandlerEntity
                    {
                        MsgType = MsgType.EVENT,
                        EventType = EventType.SCANCODE_WAITMSG,
                        Action = ScanCOdeWatingEventHandler
                    });
                    #endregion

                    #region 系统照相事件处理程序绑定
                    MsgHandlerEntity.MsgHandlerEntities.Add(new MsgHandlerEntity
                    {
                        MsgType = MsgType.EVENT,
                        EventType = EventType.PIC_SYSPHOTO,
                        Action = PicSysphotoEventHandler
                    });
                    #endregion

                    #region 系统照相或相册事件处理程序绑定
                    MsgHandlerEntity.MsgHandlerEntities.Add(new MsgHandlerEntity
                    {
                        MsgType = MsgType.EVENT,
                        EventType = EventType.PIC_PHOTO_OR_ALBUM,
                        Action = PicPhotoOrAlbumEventHandler
                    });
                    #endregion

                    #region 弹出地理位置选择器事件处理绑定
                    MsgHandlerEntity.MsgHandlerEntities.Add(new MsgHandlerEntity
                    {
                        MsgType = MsgType.EVENT,
                        EventType = EventType.LOCATION_SELECT,
                        Action = LocationSelectEventHandler
                    });
                    #endregion


                    #region 模板消息事件处理绑定
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

            switch(msg.Content)
            {
                case "音乐": msg.ResText("回复音乐"); break;
                case "语音": msg.ResVoice(new ResVoice
                    {
                        Title = "title",
                        MediaId = "KPd2y1s8CdSF0_0V4KNpPKVybbV5UMvJXHLgqOo-ujNkYLvC6fGImY3Y1J0rWt5d",
                        Desription = "description"
                    }); 
                    break;
                case "图片": msg.ResPicture("L95vVzYKNPG5wXbZRsO2446tvH9XdgOxmW9u2cnGwzTo-dZB6lQuhfE9FR8fQlmr"); break;
                case "视频": msg.ResText("这是一个视频"); break;
                case "一条图文": 
                    List<ResArticle> acticlesSingle = new List<ResArticle>();
                    acticlesSingle.Add(new ResArticle { Title = "图文1的标题", Description = "图文1的描述", Url = "", PicUrl = "https://timgsa.baidu.com/timg?image&quality=80&size=b9999_10000&sec=1507971147471&di=954cd5f3cd5348ac55ab41cace99eb2d&imgtype=0&src=http%3A%2F%2Fimg31.mtime.cn%2FCMS%2FNews%2F2014%2F11%2F19%2F080838.63641086_620X620.jpg" });
                    msg.ResArticles(acticlesSingle); 
                    break;
                case "多条图文":
                    List<ResArticle> acticlesMulti = new List<ResArticle>();
                    acticlesMulti.Add(new ResArticle { Title = "图文1的标题", Description = "图文1的描述", Url = "", PicUrl = "https://timgsa.baidu.com/timg?image&quality=80&size=b9999_10000&sec=1507971147471&di=954cd5f3cd5348ac55ab41cace99eb2d&imgtype=0&src=http%3A%2F%2Fimg31.mtime.cn%2FCMS%2FNews%2F2014%2F11%2F19%2F080838.63641086_620X620.jpg" });
                    acticlesMulti.Add(new ResArticle { Title = "图文2的标题", Description = "图文2的描述", Url = "", PicUrl = "https://timgsa.baidu.com/timg?image&quality=80&size=b9999_10000&sec=1507971147471&di=954cd5f3cd5348ac55ab41cace99eb2d&imgtype=0&src=http%3A%2F%2Fimg31.mtime.cn%2FCMS%2FNews%2F2014%2F11%2F19%2F080838.63641086_620X620.jpg" });
                    acticlesMulti.Add(new ResArticle { Title = "图文2的标题", Description = "图文2的描述", Url = "", PicUrl = "https://timgsa.baidu.com/timg?image&quality=80&size=b9999_10000&sec=1507971147471&di=954cd5f3cd5348ac55ab41cace99eb2d&imgtype=0&src=http%3A%2F%2Fimg31.mtime.cn%2FCMS%2FNews%2F2014%2F11%2F19%2F080838.63641086_620X620.jpg" });
                    acticlesMulti.Add(new ResArticle { Title = "图文2的标题", Description = "图文2的描述", Url = "", PicUrl = "https://timgsa.baidu.com/timg?image&quality=80&size=b9999_10000&sec=1507971147471&di=954cd5f3cd5348ac55ab41cace99eb2d&imgtype=0&src=http%3A%2F%2Fimg31.mtime.cn%2FCMS%2FNews%2F2014%2F11%2F19%2F080838.63641086_620X620.jpg" });
                    acticlesMulti.Add(new ResArticle { Title = "图文1的标题", Description = "图文1的描述", Url = "", PicUrl = "https://timgsa.baidu.com/timg?image&quality=80&size=b9999_10000&sec=1507971147471&di=954cd5f3cd5348ac55ab41cace99eb2d&imgtype=0&src=http%3A%2F%2Fimg31.mtime.cn%2FCMS%2FNews%2F2014%2F11%2F19%2F080838.63641086_620X620.jpg" });
                    acticlesMulti.Add(new ResArticle { Title = "图文2的标题", Description = "图文2的描述", Url = "", PicUrl = "https://timgsa.baidu.com/timg?image&quality=80&size=b9999_10000&sec=1507971147471&di=954cd5f3cd5348ac55ab41cace99eb2d&imgtype=0&src=http%3A%2F%2Fimg31.mtime.cn%2FCMS%2FNews%2F2014%2F11%2F19%2F080838.63641086_620X620.jpg" });
                    acticlesMulti.Add(new ResArticle { Title = "图文2的标题", Description = "图文2的描述", Url = "", PicUrl = "https://timgsa.baidu.com/timg?image&quality=80&size=b9999_10000&sec=1507971147471&di=954cd5f3cd5348ac55ab41cace99eb2d&imgtype=0&src=http%3A%2F%2Fimg31.mtime.cn%2FCMS%2FNews%2F2014%2F11%2F19%2F080838.63641086_620X620.jpg" });
                    acticlesMulti.Add(new ResArticle { Title = "图文2的标题", Description = "图文2的描述", Url = "", PicUrl = "https://timgsa.baidu.com/timg?image&quality=80&size=b9999_10000&sec=1507971147471&di=954cd5f3cd5348ac55ab41cace99eb2d&imgtype=0&src=http%3A%2F%2Fimg31.mtime.cn%2FCMS%2FNews%2F2014%2F11%2F19%2F080838.63641086_620X620.jpg" });
                    
                    msg.ResArticles(acticlesMulti);
                    break;
                case "李秀茹":
                    msg.ResText("阳光，空气，和水");
                    break;
                default: msg.ResText("服务器收到你发送的消息了,你发送的内容是：\r\n" + msg.Content); break;
            }
            /*if( msg.Content == "音乐" )
            {
                //msg.ResMusic()
            }
            else if (msg.Content == "图片")
            {
                msg.ResPicture("h8WC_sN8lManF1ZHbetiuS0vu9t3dLCjsncLiiJ63qiCyo_RX4I862F0NvjI04aY");
            }
            else
            {
                msg.ResText("服务器收到你发送的消息了,你发送的内容是：\r\n" + msg.Content + "\r\n点击<a href=\"http://xulang.site\">这里</a>,我们有话跟你说哦");
            }*/
            
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
            var msg = (SubEventMsg)baseMsg;
            msg.ResText("欢迎关注安工大二手交易公众号~");
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