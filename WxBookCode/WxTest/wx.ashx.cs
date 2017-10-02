﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WxApi;

namespace WxTest
{
    /// <summary>
    /// wx 的摘要说明
    /// </summary>
    public class wx : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            var url = context.Request.RawUrl;
            if(context.Request.HttpMethod=="GET")
            {   
                BaseServices.ValidUrl("skywalkerxl");
            }
            else
            {
                var xml = Utils.GetRequestData();
                System.Diagnostics.Debug.WriteLine("----------------------Receive the message Start----------------------");
                System.Diagnostics.Debug.WriteLine(xml);
                System.Diagnostics.Debug.WriteLine("---------------------- Receive the message End ----------------------");
                context.Response.Write("");
            }
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