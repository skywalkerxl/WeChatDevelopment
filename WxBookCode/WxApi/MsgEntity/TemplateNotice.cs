using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WxApi.ReceiveEntity;

namespace WxApi.MsgEntity
{
    public class TemplateNotice
    {
        private static Dictionary<string, Dictionary<string, int>> _industryList;
        public static Dictionary<string, Dictionary<string, int>> IndustryList
        {
            get
            {
                if (_industryList == null)
                    _industryList = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, int>>>(Code.IndustryCode);
                return _industryList;
            }
        }

        /// <summary>
        /// 设置公众号所属行业
        /// </summary>
        /// <param name="id1"></param>
        /// <param name="id2"></param>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public static ErrorEntity SetIndustry(string id1, string id2, string accessToken)
        {
            var url = string.Format("https://api.weixin.qq.com/cgi-bin/templates/api_get_industry?access_token={0}", accessToken);
            var json = new { industry_id1 = id1, industry_id2 = id2 };
            return Utils.PostResult<ErrorEntity>(json, url);
        }
        /// <summary>
        /// 获取模板ID
        /// </summary>
        /// <param name="templateNo">模板编号</param>
        /// <param name="accessToken">accessToken</param>
        /// <returns></returns>
        public static TemplateID GetTemplatedId(string templateNo, string accessToken)
        {
            var url = string.Format("https://api.weixin.qq.com/cgi-bin/template/api_add_template?access_token={0}", accessToken);
            var json = new { template_id_short = templateNo };
            return Utils.PostResult<TemplateID>(json, url);
        }

        /// <summary>
        /// 发送模板消息
        /// </summary>
        /// <param name="touser">要发送的用户的openid</param>
        /// <param name="template_id">模板ID</param>
        /// <param name="topcolor">消息卡片顶部的颜色</param>
        /// <param name="dataKeys">模板字段列表</param>
        /// <param name="accessToken">accessToken</param>
        /// <param name="url">单击消息卡片跳转的地址。默认为空。如果为空，ios设备会跳转到空白页面；安卓则不跳转</param>
        /// <returns></returns>
        public static TemplateMsg Send(string touser, string template_id, string topcolor, Dictionary<string, TemplateKey> dataKeys, string accessToken, string url = "")
        {
            var turl = string.Format("https://api.weixin.qq.com/cgi-bin/message/template/send?access_token={0}", accessToken);
            var json = new
            {
                touser = touser,
                template_id = template_id,
                url = url,
                topcolor = topcolor,
                data = dataKeys
            };
            return Utils.PostResult<TemplateMsg>(json, turl);
        }
    }
}
