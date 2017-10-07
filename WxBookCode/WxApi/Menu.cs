using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WxApi.ReceiveEntity;
using WxApi.SendEntity;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace WxApi
{
    /// <summary>
    /// 用于封装自定义菜单相关操作的方法
    /// </summary>
    public class Menu
    {
        public static ErrorEntity Create(MenuEntity menuEntity, string accessToken)
        {
            var url = string.Format("https://api.weixin.qq.com/cgi-bin/menu/create?access_token={0}", accessToken);
            return Utils.PostResult<ErrorEntity>(menuEntity, url);
        }
        /// <summary>
        /// 自定义菜单查询接口
        /// </summary>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public static MenuEntity Query(string accessToken)
        {
            var url = string.Format("https://api.weixin.qq.com/cgi-bin/menu/get?access_token={0}", accessToken);
            var jobj = Utils.GetResult<JObject>(url);
            var menu = jobj["menu"].ToString();
            return JsonConvert.DeserializeObject<MenuEntity>(menu);
        }
        /// <summary>
        /// 自定义菜单删除接口
        /// </summary>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public static ErrorEntity Delete(string accessToken)
        {
            var url = string.Format("https://api.weixin.qq.com/cgi-bin/menu/delete?access_token={0}", accessToken);
            return Utils.GetResult<ErrorEntity>(url);
        }
    }
}
