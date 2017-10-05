using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WxApi.SendEntity;
using Newtonsoft.Json;
using System.IO;

namespace WxApi.ReceiveEntity
{
    public class MaterialLib
    {
        /// <summary>
        /// 获取永久图文素材
        /// </summary>
        /// <param name="mediaId"></param>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public static MaterialNews GetNews(string mediaId, string accessToken)
        {
            var url = string.Format("https://api.weixin.qq.com/cgi-bin/material/get_matetial?access_token={0}", accessToken);
            return Utils.PostResult<MaterialNews>(url, JsonConvert.SerializeObject(new {media_id = mediaId}));
        }

        /// <summary>
        /// 获取永久视频素材
        /// </summary>
        /// <param name="mediaId"></param>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public static MaterialVideo GetVideo(string mediaId, string accessToken)
        {
            var url = string.Format("https://api.weixin.qq.com/cgi-bin/material/get_material?access_token={0}", accessToken);
            return Utils.PostResult<MaterialVideo>(url, JsonConvert.SerializeObject(new { media_id = mediaId }));
        }

        /// <summary>
        /// 获取除了视频、图文素材之外的其他素材
        /// </summary>
        /// <param name="mediaId"></param>
        /// <param name="stream"></param>
        /// <param name="accessToken"></param>
        public static void GetOther(string mediaId, Stream stream, string accessToken)
        {
            var url = string.Format("https://api.weixin.qq.com/cgi-bin/material/get_material?access_token={0}", accessToken);
            Utils.DownLoadByPost(url, JsonConvert.SerializeObject(new { media_id = mediaId }), stream);

        }
    }

    /// <summary>
    /// 视频素材实体类
    /// </summary>
    public class MaterialVideo:ErrorEntity
    {
        public string title { get; set; }
        public string description { get; set; }
        public string down_url { get; set; }
    }

    /// <summary>
    /// 获取用旧素材时，当获取的素材为图文消息时，返回的实体
    /// </summary>
    public class MaterialNews:ErrorEntity
    {
        /// <summary>
        /// 多图文列表
        /// </summary>
        public List<Article> news_item { get; set; }
    }
}
