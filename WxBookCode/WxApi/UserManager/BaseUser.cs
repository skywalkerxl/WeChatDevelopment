using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WxApi.ReceiveEntity;

namespace WxApi.UserManager
{
    public class BaseUser
    {
        /// <summary>
        /// 封装操作微信用户信息的方法
        /// </summary>
        /// <param name="openid"></param>
        /// <param name="access_token"></param>
        /// <returns></returns>
        public static UserInfo GetUserInfo(string openid, string access_token)
        {
            var url = string.Format("https://api.weixin.qq.com/cgi-bin/user/info?access_token={0}&openid={1}", access_token, openid);
            return Utils.GetResult<UserInfo>(url);
        }
        /// <summary>
        /// 对指定用户设置备注名，该接口暂时开放给微信认证的服务号
        /// </summary>
        /// <param name="openid"></param>
        /// <param name="remark"></param>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public static ErrorEntity UpdateRemark(string openid, string remark, string accessToken)
        {
            var url = string.Format("https://api.weixin.qq.com/cgi-bin/user/info/updateremark?access_token={0}", accessToken);
            var obj = new
            {
                openid = openid,
                remark = remark
            };
            return Utils.PostResult<ErrorEntity>(obj, url);
        }

        public static UserListEntity GetUserList(string accessToken, string next_openid="")
        {
            var url = string.Format("https://api.weixin.qq.com/cgi-bin/user/get?access_token={0}&next_openid={1}", accessToken, next_openid);
            var retdata = Utils.GetResult<UserListEntity>(url);
            // 判断调用是否成功。当调用成功，且总关注人数大于10000，本次获取到的用户数量等于10000时，则说明有尚未获取到的用户，此时递归调用，添加到列表
            if(retdata.ErrCode == 0 && retdata.total > 10000 && retdata.count == 10000)
            {
                retdata.data.openid.AddRange(GetUserList(accessToken, retdata.next_openid).data.openid);
            }
            return retdata;
        }
    }
}
