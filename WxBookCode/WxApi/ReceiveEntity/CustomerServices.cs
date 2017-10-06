using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WxApi.MsgEntity;

namespace WxApi.ReceiveEntity
{
    /// <summary>
    /// 客服消息接口
    /// </summary>
    public class CustomerServices
    {
        /// <summary>
        /// http请求方式：POST
        /// </summary>
        private static string url = "https://api.weixin.qq.com/cgi-bin/message/custom/send?access_token={0}";
        /// <summary>
        /// 发送文本
        /// </summary>
        /// <param name="openid"></param>
        /// <param name="content"></param>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public static ErrorEntity SendText(string openid, string content, string accessToken)
        {
            var json = new
            {
                touser = openid,
                msgtype = "text",
                text = new
                {
                    content = content
                }
            };
            return Send(json, accessToken);
        }
        /// <summary>
        /// 发送图片消息
        /// </summary>
        /// <param name="openid"></param>
        /// <param name="media_id"></param>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public static ErrorEntity SendImg(string openid, string media_id, string accessToken)
        {
            var json = new
            {
                touser = openid,
                msgtype = "image",
                image = new
                {
                    media_id = media_id
                }
            };
            return Send(json, accessToken);
        }
        /// <summary>
        /// 发送声音消息
        /// </summary>
        /// <param name="openid"></param>
        /// <param name="media_id"></param>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public static ErrorEntity SendVoice(string openid, string media_id, string accessToken)
        {
            var json = new
            {
                touser = openid,
                msgtype = "voice",
                voice = new
                {
                    media_id = media_id
                }
            };
            return Send(json, accessToken);
        }
        /// <summary>
        /// 发送视频消息
        /// </summary>
        /// <param name="openid"></param>
        /// <param name="Video"></param>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public static ErrorEntity SendVideo(string openid, CustomVideo Video, string accessToken)
        {
            var json = new
            {
                touser = openid,
                msgtype = "video",
                video = Video
            };
            return Send(json, accessToken);
        }
        /// <summary>
        /// 发送音乐消息
        /// </summary>
        /// <param name="openid"></param>
        /// <param name="music"></param>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public static ErrorEntity SendMusic(string openid, CustomMusic music, string accessToken)
        {
            var json = new
            {
                touser = openid,
                msgtype = "music",
                music = music
            };
            return Send(json, accessToken);
        }
        /// <summary>
        /// 发送图文消息
        /// </summary>
        /// <param name="openid"></param>
        /// <param name="article"></param>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public static ErrorEntity SendArticle(string openid, CustomArticle article, string accessToken)
        {
            var json = new
            {
                touser = openid,
                msgtype = "news",
                news = article
            };
            return Send(json, accessToken);
        }

        private static ErrorEntity Send(object obj, string accessToken)
        {
            return Utils.PostResult<ErrorEntity>(obj, string.Format(url, accessToken));
        }
    }
}
