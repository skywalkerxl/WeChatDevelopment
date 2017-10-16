using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WxApi;

namespace WxTest
{
    public partial class WxJs : System.Web.UI.Page
    {
        protected string timestamp; // 时间戳
        protected string noncestr; // 随机字符串
        protected string url; // 当前url
        protected string sign; // 签名
        private static WxApi.ReceiveEntity.JsApiTicket ticket;

        protected void Page_Load(object sender, EventArgs e)
        {
            timestamp = Utils.ConvertDateTimeInt(DateTime.Now).ToString();
            noncestr = timestamp; // 随机字符串也使用时间戳
            url = "http://" + Utils.GetCurrentFullHost() + Request.RawUrl;
            var accessToken = AccessTokenBox.GetTokenValue("wxf50808b364418ffb", "bfaf8363dc64787091b3bbb7740dcf44");
            if(ticket == null || ticket.expires_time < DateTime.Now )
            {
                ticket = JsApi.GetHsJsApTicket(accessToken);
            }
            sign = JsApi.GetJsApiSign(noncestr, ticket.ticket, timestamp, url);
        }

        
    }
}