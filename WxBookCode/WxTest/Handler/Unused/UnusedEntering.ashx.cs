using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WxApi;
using WxApi.UserManager;
using System.Web.SessionState;

namespace WxTest.Handler.Unused
{
    /// <summary>
    /// UnusedEntering 的摘要说明
    /// </summary>
    public class UnusedEntering : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            // var openid = HttpContext.Current.Session["openid"];
            // context.Response.Write(openid);
            // context.Response.ContentType = "text/plain";
            // context.Response.Write("Hello World");
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}