using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WxApi.MsgEntity
{
    public class CustomArticles
    {
        public List<CustomArticle> articles { get; set; }
    }
    
    public class CustomArticle
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string description {get; set;}
        /// <summary>
        /// 图文链接
        /// </summary>
        public string url {get; set;}
        /// <summary>
        /// 图片链接
        /// </summary>
        public string picurl { get; set; }
    }
}
