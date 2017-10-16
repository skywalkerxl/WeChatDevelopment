using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WxApi.ReceiveEntity;
using System.Web.Security;

namespace WxApi
{
    public class JsApi
    {
        /// <summary>
        /// 根据access_token获取jsapi_ticket
        /// </summary>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public static JsApiTicket GetHsJsApTicket(string accessToken)
        {
            var url = string.Format("https://api.weixin.qq.com/cgi-bin/ticket/getticket?access_token={0}&type=jsapi", accessToken);
            return Utils.GetResult<JsApiTicket>(url);
        }


        public static string GetJsApiSign(string noncestr, string jsapi_ticket, string timestamp, string url)
        {
            // 将字段添加到列表中
            string[] arr = new[]
            {
                string.Format("noncestr={0}", noncestr),
                string.Format("jsapi_ticket={0}", jsapi_ticket),
                string.Format("timestamp={0}", timestamp),
                string.Format("url={0}", url)
            };
            // 字典序排序
            Array.Sort(arr);
            // 使用URL值键值对的格式拼接成字符串
            var temp = string.Join("&", arr);
            return FormsAuthentication.HashPasswordForStoringInConfigFile(temp, "SHA1");
        }
    }
}
