using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WxApi.ReceiveEntity;

namespace WxApi.SendEntity
{
    /// <summary>
    /// 图片素材图文项实体
    /// </summary>
    public class Article
    {
        /// <summary>
        /// 图片消息的封面图片素材id(必须是永久mediaID)
        /// </summary>
        public string thumb_media_id { get; set; }
        /// <summary>
        /// 图文消息的作者
        /// </summary>
        public string author { get; set; }
        /// <summary>
        /// 图文消息的标题
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// 在图文消息页面单击"阅读原文"后的页面
        /// </summary>
        public string content_source_url { get; set; }
        /// <summary>
        /// 图文消息页面的内容,支持HTML标签，且此处会去除JS
        /// </summary>
        public string content { get; set; }
        /// <summary>
        /// 图文消息的摘要，仅有单图文消息才有摘要，多图文此处为空
        /// </summary>
        public string digest { get; set; }
        /// <summary>
        /// 是否显示封面。1为显示，0为不显示
        /// </summary>
        public int show_cover_pic { get; set; }
        /// <summary>
        /// 新增永久图文消息的方法
        /// </summary>
        /// <param name="articles"></param>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public static UpLoadInfo AddArticle(List<Article> articles, string accessToken)
        {
            var url = string.Format("https://api.weixin.qq.com/cgi-bin/material/add_news?access_token={0}", accessToken);
            return Utils.PostResult<UpLoadInfo>(new { articles = articles }, url);
        }
    }
}
