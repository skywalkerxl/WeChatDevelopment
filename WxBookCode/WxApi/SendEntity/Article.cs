using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WxApi.ReceiveEntity;

namespace WxApi.SendEntity
{
    public class Article
    {
        public string thumb_media_id { get; set; }

        public string author { get; set; }

        public string title { get; set; }

        public string content_source_url { get; set; }

        public string content { get; set; }

        public string digest { get; set; }

        public int show_cover_pic { get; set; }

        public static UpLoadInfo AddArticle(List<Article> articles, string accessToken)
        {
            var url = string.Format("https://api.weixin.qq.com/cgi-bin/material/add_news?access_token={0}", accessToken);
            return Utils.PostResult<UpLoadInfo>(new { articles = articles }, url);
        }
    }
}
