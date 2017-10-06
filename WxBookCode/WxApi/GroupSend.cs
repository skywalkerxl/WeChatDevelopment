using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WxApi.ReceiveEntity;
using WxApi.SendEntity;

namespace WxApi
{
    public class GroupSend
    {
        /// <summary>
        /// 上传图文消息
        /// </summary>
        /// <param name="artList"></param>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public static GroupUpLoadEntity UpLoadNew(List<Article> artList, string accessToken)
        {
            var url = string.Format("https://api.weixin.qq.com/cgi-bin/media/uploadnews?access_token={0}", accessToken);
            var json = new
            {
                articles = artList
            };
            return Utils.PostResult<GroupUpLoadEntity>(json, url);
        }
        /// <summary>
        /// 上传视频消息
        /// </summary>
        /// <param name="media_id"></param>
        /// <param name="title"></param>
        /// <param name="description"></param>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public static GroupUpLoadEntity UpLoadVideo(string media_id, string title, string description, string accessToken)
        {
            var url = string.Format("https://file.api.weixin.qq.com/cgi-bin/media/uploadvideo?access_token={0}", accessToken);
            var json = new
            {
                media_id = media_id,
                title = title,
                description = description
            };
            return Utils.PostResult<GroupUpLoadEntity>(json, url);
        }
        /// <summary>
        /// 基础发送接口
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="accessToken">accessToken</param>
        /// <param name="sendtype">群发类型:1 为按分组群发， 2 为按openid列表群发 3 为预览接口。 默认为按分组群发</param>
        /// <returns></returns>
        private static GroupSendEntity BaseSend(object obj, string accessToken, int sendtype = 1)
        {
            string url = null;
            switch(sendtype)
            {
                case 1: url = string.Format("https://api.weixin.qq.com/cgi-bin/message/mass/sendall?access_token={0}", accessToken); break;
                case 2: url = string.Format("https://api.weixin.qq.com/cgi-bin/message/mass/send?access_token={0}", accessToken); break;
                case 3: url = string.Format("https://api.weixin.qq.com/cgi-bin/message/mass/preview?access_token={0}", accessToken); break;
                default: url = string.Format("https://api.weixin.qq.com/cgi-bin/message/mass/sendall?access_token={0}", accessToken); break;
            }
            return Utils.PostResult<GroupSendEntity>(obj, url);
        }
        /// <summary>
        /// 按分组群发消息， isall为true时，说明群发所有用户，此时group_id可为空，否则，根绝group_id进行群发
        /// </summary>
        /// <param name="media_id"></param>
        /// <param name="accessToken"></param>
        /// <param name="isall"></param>
        /// <param name="group_id"></param>
        /// <returns></returns>
        public static GroupSendEntity SendArticleByGroup(string media_id, string accessToken, bool isall = true, string group_id = "")
        {
            var json = new
            {
                filter = new
                {
                    is_to_all = isall,
                    group_id = group_id
                },
                mpnews = new
                {
                    media_id = media_id
                },
                msgtype = "mpnews"
            };
            return BaseSend(json, accessToken);
        }

        /// <summary>
        /// 文本消息
        /// </summary>
        /// <param name="content"></param>
        /// <param name="accessToken"></param>
        /// <param name="isall"></param>
        /// <param name="group_id"></param>
        /// <returns></returns>
        public static GroupSendEntity SendTextByGroup(string content, string accessToken, bool isall = true, string group_id="")
        {
            var json = new
            {
                filter = new
                {
                    is_to_all = isall,
                    group_id = group_id
                },
                text = new
                {
                    content = content
                },
                msgtype = "text"
            };
            return BaseSend(json, accessToken);
        }
        /// <summary>
        /// 语音消息
        /// </summary>
        /// <param name="media_id"></param>
        /// <param name="accessToken"></param>
        /// <param name="isall"></param>
        /// <param name="group_id"></param>
        /// <returns></returns>
        public static GroupSendEntity SendVoiceByGroup(string media_id, string accessToken, bool isall = true, string group_id ="")
        {
            var json = new
            {
                filter = new
                {
                    is_to_all = isall,
                    group_id = group_id
                },
                voice = new
                {
                    media_id = media_id
                },
                msgtype = "voice"
            };
            return BaseSend(json, accessToken);
        }
        /// <summary>
        /// 图片消息
        /// </summary>
        /// <param name="media_id"></param>
        /// <param name="accessToken"></param>
        /// <param name="isall"></param>
        /// <param name="group_id"></param>
        /// <returns></returns>
        public static GroupSendEntity SendImgByGroup(string media_id, string accessToken, bool isall =true, string group_id = "" )
        {
            var json = new
            {
                filter = new
                {
                    is_to_all = isall,
                    group_id = group_id
                },
                image = new
                {
                    media_id = media_id
                },
                msgtype = "image"
            };
            return BaseSend(json, accessToken, 1);
        }
        /// <summary>
        /// 视频消息
        /// </summary>
        /// <param name="media_id"></param>
        /// <param name="accessToken"></param>
        /// <param name="isall"></param>
        /// <param name="group_id"></param>
        /// <returns></returns>
        public static GroupSendEntity SendVideoByGroup(string media_id, string accessToken, bool isall = true, string group_id = "")
        {
            var json = new
            {
                filter = new
                {
                    is_to_all = isall,
                    group_id = group_id
                },
                mpvideo = new
                {
                    media_id = media_id
                },
                msgtype = "mpvideo"
            };
            return BaseSend(json, accessToken);
        }

        #region 按openid列表群发和预览接口【订阅号不可用，服务号认证后可用】
        /// <summary>
        /// 按用户列表群发文本消息
        /// </summary>
        /// <param name="content">群发内容</param>
        /// <param name="accessToken">accessToken</param>
        /// <param name="touser">如果是数组，则表示openid列表，调用的是群发接口：否则表示openid,调用的是预览接口</param>
        /// <returns></returns>
        public static GroupSendEntity SendTextByOpenID(string content, string accessToken, object touser)
        {
            var json = new
            {
                touser = touser,
                text = new
                {
                    content = content
                },
                msgtype = "text"
            };
            return BaseSend(json, accessToken, touser.GetType().IsArray?2:3);
        }

        /// <summary>
        /// 按用户列表群发图片消息
        /// </summary>
        /// <param name="media_id">图片媒体ID</param>
        /// <param name="accessToken">accessToken</param>
        /// <param name="touser">如果是数组，则表示openid列表，调用的是群发接口：否则表示openid,调用的是预览接口</param>
        /// <returns></returns>
        public static GroupSendEntity SendImgByOpenID(string media_id, string accessToken, object touser)
        {
            var json = new
            {
                touser = touser,
                image = new
                {
                    media_id = media_id
                },
                msgtype = "image"
            };
            return BaseSend(json, accessToken, touser.GetType().IsArray ? 2 : 3);
        }
        /// <summary>
        /// 按用户列表群发语音消息
        /// </summary>
        /// <param name="media_id">语音媒体ID</param>
        /// <param name="accessToken">accessToken</param>
        /// <param name="touser">如果是数组，则表示openid列表，调用的是群发接口：否则表示openid,调用的是预览接口</param>
        /// <returns></returns>
        public static GroupSendEntity SendVoiceByOpenID(string media_id, string accessToken, object touser)
        {
            var json = new
            {
                touser = touser,
                voice = new
                {
                    media_id = media_id
                },
                msgtype = "voice"
            };
            return BaseSend(json, accessToken, touser.GetType().IsArray ? 2 : 3);
        }
        /// <summary>
        /// 按用户列表群发图文消息
        /// </summary>
        /// <param name="media_id">图文ID</param>
        /// <param name="accessToken">accessToken</param>
        /// <param name="touser">如果是数组，则表示openid列表，调用的是群发接口：否则表示openid,调用的是预览接口</param>
        /// <returns></returns>
        public static GroupSendEntity SendArticleByOpenID(string media_id, string accessToken, object touser)
        {
            var json = new
            {
                touser = touser,
                mpnews = new
                {
                    media_id = media_id
                },
                msgtype = "mpnews"
            };
            return BaseSend(json, accessToken, touser.GetType().IsArray ? 2 : 3);
        }
        /// <summary>
        /// 按用户列表群发视频消息
        /// </summary>
        /// <param name="media_id">视频ID</param>
        /// <param name="accessToken">accessToken</param>
        /// <param name="touser">如果是数组，则表示openid列表，调用的是群发接口：否则表示openid,调用的是预览接口</param>
        /// <returns></returns>
        public static GroupSendEntity SendVideoByOpenID(string media_id, string accessToken, object touser)
        {
            var json = new
            {
                touser = touser,
                mpvideo = new
                {
                    media_id = media_id
                },
                msgtype = "mpvideo"
            };
            return BaseSend(json, accessToken, touser.GetType().IsArray ? 2 : 3);
        }
        #endregion
    }
}
