using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WxApi;
using WxApi.UserManager;

namespace WxTest.Handler.Unused
{
    /// <summary>
    /// UnusedComparison 的摘要说明
    /// </summary>
    public class UnusedComparison : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            OAuth auth = new OAuth();
            
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