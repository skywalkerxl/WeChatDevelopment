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

        /// <summary>
        /// 添加素材。临时素材的有效时间为3天
        /// </summary>
        /// <param name="filepath">服务器文件的物理路径，可用Request.MapPath将虚拟路径转换为物理路径。也可为网络路径，如: http://XXXX</param>
        /// <param name="accessToken">调用凭据</param>
        /// <param name="mediaType">媒体类型枚举</param>
        /// <param name="IsTemp">是否是临时素材</param>
        /// <param name="videotitle"永久视频素材标题></param>
        /// <param name="videointroduction">永久视频素材描述</param>
        /// <returns></returns>
        public static UpLoadInfo Add(string filepath, string accessToken, MaterialType mediaType, bool IsTemp = true, string videotitle = "", String videointroduction)
        {
            try
            {
                var url = "https://api.weixin.qq.com/cgi-bin/media/upload?access_token={0}&type={1}";
                if(!IsTemp)
                {

                    url = "https://api.weixin.qq.com/cgi-bin/material/add_material?access_token={0}&type={1}";
                }
                var formlist = new List<FormEntity>
                {
                    new FormEntity{ IsFile = true, Name = "media", Value = filepath }
                };
                if(mediaType == MaterialType.video && !IsTemp)
                {
                    var value = JsonConvert.SerializeObject(new { title = videotitle, introduction = videointroduction });
                    formlist.Add(new FormEntity { IsFile = false, Name = "description", Value = value });

                }
                return Utils.PostFormResult<UpLoadInfo>(formlist, string.Format(url, accessToken, mediaType.ToString()));
            }
            catch(Exception e)
            {
                return new UpLoadInfo
                {
                    ErrCode = -2,
                    ErrDescription = e.Message
                };
            }
        }

        /// <summary>
        /// 获取临时素材的url
        /// </summary>
        /// <param name="mediaId"></param>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public static string GetTempUrl(string mediaId, string accessToken)
        {
            var url = "http://api.weixin.qq.com/cgi-bin/media/get?access_token={0}&media_id={1}";
            return string.Format(url, accessToken, mediaId);
        }
        /// <summary>
        /// 删除素材的方法
        /// </summary>
        /// <param name="mediaId"></param>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public static ErrorEntity Del(string mediaId, string accessToken)
        {
            var url = "https://api.weixin.qq.com/cgi-bin/material/del_material?access_token={0}";
            return Utils.PostResult<ErrorEntity>(new { media_id = mediaId }, string.Format(url, accessToken));
        }
        /// <summary>
        /// 修改永久图文素材
        /// </summary>
        /// <param name="mediaId">图文素材ID</param>
        /// <param name="accessToken">调用凭据</param>
        /// <param name="index">要更新的文章在图文消息中的位置（多图文消息时，此字段才有意义），第一篇为0</param>
        /// <param name="article">图文实体。此处表示的是修改后的图文信息</param>
        /// <returns></returns>
        public static ErrorEntity Update(string mediaId, string accessToken, int index, Article article)
        {
            var url = string.Format("https://api.weixin.qq.com/cgi-bin/material/update_news?access_token]={0}", accessToken);
            var obj = new
            {
                media_id = mediaId,
                index = index,
                articles = article
            };
            return Utils.PostResult<ErrorEntity>(obj, url);
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
