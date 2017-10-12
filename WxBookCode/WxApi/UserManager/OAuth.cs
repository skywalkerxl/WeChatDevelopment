using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WxApi.ReceiveEntity;

namespace WxApi.UserManager
{
    public class OAuth
    {
        public static string GetAuthUrl(string appid, string redirect_url, string state, AuthType authType = AuthType.snsapi_base)
        {
            var url = "https://open.weixin.qq.com/connect/oauth2/authorize?appid={0}&redirect_url={1}&response_type=code&scope={2}&state={3}#wechat_redirect";
            return string.Format(url, appid, Utils.UrlEncode(redirect_url), authType, state);
        }
        /// <summary>
        /// 检验授权凭证是否有效的接口
        /// </summary>
        /// <param name="authtoken"></param>
        /// <param name="openid"></param>
        /// <returns></returns>
        public static ErrorEntity CheckAuthToken(string authtoken, string openid)
        {
            var url = string.Format("https://api.weixin.qq.com/sns/auth?access_token={0}&openid={1}", authtoken, openid);
            return Utils.GetResult<ErrorEntity>(url);
        }

        public static OAuthToken GetRefreshToken(string appid, string refresh_token)
        {
            var url = string.Format("https://api.weixin.qq.com/sns/oauth2/refresh_token?appid={0}&grant_type=refresh_token&refresh_token={1}", appid, refresh_token);
            return Utils.GetResult<OAuthToken>(url);
        }
    }
}
