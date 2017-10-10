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
    }
}
