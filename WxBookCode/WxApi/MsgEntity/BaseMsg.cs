using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using WxApi.SendEntity;

namespace WxApi.MsgEntity
{
    public abstract class BaseMsg
    {
        /// <summary>
        /// 开发者微信号
        /// </summary>
        public string ToUserName { get; set; }
        /// <summary>
        /// 发送方账号(openid)
        /// </summary>
        public string FromUserName { get; set; }
        /// <summary>
        /// 消息创建时间
        /// </summary>
        public string CreateTime { get; set; }
        /// <summary>
        /// 消息类型
        /// </summary>
        public MsgType MsgType { get; set; }

        /// <summary>
        /// 回复消息(文本)
        /// </summary>
        /// <param name="content"></param>
        public virtual void ResText(string content)
        {
            var resxml = new StringBuilder();
            resxml.AppendFormat("<xml><ToUserName><![CDATA[{0}]]></ToUserName>", FromUserName);
            resxml.AppendFormat("<FromUserName><![CDATA[{0}]]></FromUserName>", ToUserName);
            resxml.AppendFormat("<CreateTime>{0}</CreateTime>", Utils.ConvertDateTimeInt(DateTime.Now));
            resxml.Append("<MsgType><![CDATA[text]]></MsgType>");
            resxml.AppendFormat("<Content><![CDATA[{0}]]></Content></xml>", content);
            HttpContext.Current.Response.Write(resxml);
            return;
        }
        /// <summary>
        /// 回复消息，图片
        /// </summary>
        /// <param name="media_id"></param>
        public void ResPicture (string media_id)
        {
            var resxml = new StringBuilder();
            resxml.AppendFormat("<xml><ToUserName><![CDATA[{0}]]></ToUserName>", FromUserName);
            resxml.AppendFormat("<FromUserName><![CDATA[{0}]]></FromUserName>", ToUserName);
            resxml.AppendFormat("<CreateTime>{0}</CreateTime>", Utils.ConvertDateTimeInt(DateTime.Now));
            resxml.Append("<MsgType><![CDATA[image]]></MsgType>");
            resxml.AppendFormat("<Image><MediaId><![CDATA[{0}]]></MediaId></Image></xml>", media_id);
            HttpContext.Current.Response.Write(resxml);
            return;
        }
        /// <summary>
        /// 回复消息，视频
        /// </summary>
        /// <param name="video"></param>
        public void ResVideo(ResVideo video)
        {
            var resxml = new StringBuilder();
            resxml.AppendFormat("<xml><ToUserName><![CDATA[{0}]]></ToUserName>", FromUserName);
            resxml.AppendFormat("<FromUserName><![CDATA[{0}]]></FromUserName>", ToUserName);
            resxml.AppendFormat("<CreateTime>{0}</CreateTime>", Utils.ConvertDateTimeInt(DateTime.Now));
            resxml.Append("<MsgType><![CDATA[video]]></MsgType>");
            resxml.AppendFormat("<Video><MediaId><![CDATA[{0}]]></MediaId></Video>", video.MediaId);
            resxml.AppendFormat("<Title><![CDATA[{0}]]></Title>", video.Title);
            resxml.AppendFormat("<Description><![CDATA[{0}]]></Description></xml>", video.Description);
            HttpContext.Current.Response.Write(resxml);
            return;
        }

        public void ResArticles(List<ResArticle> artList)
        {
            var resxml = new StringBuilder();
            resxml.AppendFormat("<xml><ToUserName><![CDATA[{0}]]></ToUserName>", FromUserName);
            resxml.AppendFormat("<FromUserName><![CDATA[{0}]]></FromUserName>", ToUserName);
            resxml.AppendFormat("<CreateTime>{0}</CreateTime>", Utils.ConvertDateTimeInt(DateTime.Now));
            resxml.Append("<MsgType><![CDATA[news]]></MsgType>");
            resxml.AppendFormat("<ArticleCount>{0}</ArticleCount><Articles>", artList.Count);
            foreach(var article in artList)
            {
                resxml.AppendFormat("<item><Title><![CDATA[{0}]]></Title>", article.Title);
                resxml.AppendFormat("<PicUrl><![CDATA[{0}]]></PicUrl>", article.PicUrl);
                resxml.AppendFormat("<Url><![CDATA[{0}]]></Url>", article.Url);
                resxml.AppendFormat("<Description><![CDATA[{0}]]></Description></item>", article.Description);
            }
            resxml.Append("</Articles></xml>");
            HttpContext.Current.Response.Write(resxml);
            return;
        }
    }

    /// <summary>
    /// 消息类型枚举
    /// </summary>
    public enum MsgType
    {
        /// <summary>
        /// 文本类型
        /// </summary>
        TEXT,
        /// <summary>
        /// 图片类型
        /// </summary>
        IMAGE,
        /// <summary>
        /// 语音类型
        /// </summary>
        VOICE,
        /// <summary>
        /// 视频类型
        /// </summary>
        VIDEO,
        /// <summary>
        /// 地理位置类型
        /// </summary>
        LOCATION,
        /// <summary>
        /// 链接类型
        /// </summary>
        LINK,
        /// <summary>
        /// 事件类型(注：C#中event是关键字)
        /// </summary>
        EVENT
    }

    /// <summary>
    /// 事件类型枚举
    /// </summary>
    public enum EventType
    {
        /// <summary>
        /// 非事件类型
        /// </summary>
        NOEVENT,
        /// <summary>
        /// 订阅
        /// </summary>
        SUBSCRIBE,
        /// <summary>
        /// 取消订阅
        /// </summary>
        UNSUBSCRIBE,
        /// <summary>
        /// 扫描带参数的二维码
        /// </summary>
        SCAN,
        /// <summary>
        /// 地理位置
        /// </summary>
        LOCATION,
        /// <summary>
        /// 单击按钮
        /// </summary>
        CLICK,
        /// <summary>
        /// 链接按钮
        /// </summary>
        VIEW,
        /// <summary>
        /// 扫码推事件
        /// </summary>
        SCANCODE_PUSH,
        /// <summary>
        /// 扫码推事件且弹出“消息接收中”提示框
        /// </summary>
        SCANCODE_WAITMSG,
        /// <summary>
        /// 弹出系统拍照发图
        /// </summary>
        PIC_SYSPHOTO,
        /// <summary>
        /// 弹出拍照或者相册发图
        /// </summary>
        PIC_PHOTO_OR_ALBUM,
        /// <summary>
        /// 弹出微信相册发图器
        /// </summary>
        PIC_WEIXIN,
        /// <summary>
        /// 弹出地理位置选择器
        /// </summary>
        LOCATION_SELECT,
        /// <summary>
        /// 模板消息推送
        /// </summary>
        TEMPLATESENDJOBFINISH,

        MASSSENDJOBFINISH
    }

    public　class TextMsg:BaseMsg
    {
        /// <summary>
        /// 消息内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 消息ID, 64位整型
        /// </summary>
        public string MsgId { get; set; }
    }

    public class VoiceMsg : BaseMsg
    {
        public string MsgId { get; set; }

        public string Format { get; set; }

        public string MediaId { get; set; }

        public string Recognition { get; set; }
    }

    public class VideoMsg : BaseMsg
    {
        public string ThumbMediaId { get; set; }

        public string MsgId { get; set; }

        public string MediaId { get; set; }
    }

    public class LinkMsg : BaseMsg
    {
        public string MsgId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Url { get; set; }
    }

    public class ImgMsg : BaseMsg
    {
        public string PicUrl { get; set; }

        public string MsgId { get; set; }

        public string MediaId { get; set; }
    }

    public class LocationMsg : BaseMsg
    {
        /// <summary>
        /// 地理位置维度
        /// </summary>
        public string Location_X { get; set; }
        /// <summary>
        /// 地理位置精度
        /// </summary>
        public string Location_Y { get; set; }
        /// <summary>
        /// 地图缩放大小
        /// </summary>
        public int Scale { get; set; }
        /// <summary>
        /// 地理位置信息
        /// </summary>
        public string Label { get; set; }
        /// <summary>
        /// 链接地址
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// 消息ID
        /// </summary>
        public string MsgId { get; set; }
    }

    public class EventMsg : BaseMsg
    {
        public EventType Event { get; set; }
    }

    public class SubEventMsg : EventMsg
    {

    }

    public class LocationEventMsg:EventMsg
    {
        /// <summary>
        /// 地理位置维度
        /// </summary>
        public string Latitude { get; set; }
        /// <summary>
        /// 地理位置经度
        /// </summary>
        public string Longitude { get; set; }
        /// <summary>
        /// 地理位置精度
        /// </summary>
        public string Precision { get; set; }
    }
}
