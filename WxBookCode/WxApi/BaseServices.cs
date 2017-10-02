using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.Security;

namespace WxApi
{
    public class BaseServices
    {
        public static bool ValidUrl(string token)
        {
            var signature = HttpContext.Current.Request.QueryString["signature"];
            var timestamp = HttpContext.Current.Request.QueryString["timestamp"];
            var nonce = HttpContext.Current.Request.QueryString["nonce"];
            string[] temp = { token, timestamp, nonce };
            Array.Sort(temp);
            var tempstr = string.Join("", temp);

            //SHA1加密
            var tempsign = FormsAuthentication.HashPasswordForStoringInConfigFile(tempstr, "SHA1").ToLower();
            if (tempsign == signature)
            {
                var echostr = HttpContext.Current.Request.QueryString["echostr"];
                HttpContext.Current.Response.Write(echostr);
                return true;
            }
            return false;
        }
    }
}
