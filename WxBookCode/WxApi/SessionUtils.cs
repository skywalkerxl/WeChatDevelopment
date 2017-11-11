using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.SessionState;
using System.Web;

namespace WxApi
{
    public class SessionUtils
    {
        public static void Add(string strSessionName, string strValue)
        {
            HttpContext.Current.Session[strSessionName] = strValue;
            HttpContext.Current.Session.Timeout = 20;
        }

        public static void Adds(string strSessionName, string[] strValue)
        {
            HttpContext.Current.Session[strSessionName] = strValue;
            HttpContext.Current.Session.Timeout = 20;
        }

        public static void Add(string strSessionName, string strValue, int iExpires)
        {
            HttpContext.Current.Session[strSessionName] = strValue;
            HttpContext.Current.Session.Timeout = iExpires;
        }

        public static void Adds(string strSessionName, string[] strValue, int iExpires)
        {
            HttpContext.Current.Session[strSessionName] = strValue;
            HttpContext.Current.Session.Timeout = iExpires;
        }

        public static string Get(string strSessionName)
        {
            if (HttpContext.Current.Session[strSessionName] == null)
            {
                return null;
            }
            else
            {
                return HttpContext.Current.Session[strSessionName].ToString();
            }
        }

        public static string[] Gets(string strSessionName)
        {
            if(HttpContext.Current.Session[strSessionName] == null)
            {
                return null;
            }
            else
            {
                return (string[])HttpContext.Current.Session[strSessionName];
            }
        }

        public static void Del(string strSessionName)
        {
            HttpContext.Current.Session[strSessionName] = null;
        }
    }
}
