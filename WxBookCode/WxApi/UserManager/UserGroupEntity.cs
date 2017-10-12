using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WxApi.ReceiveEntity;

namespace WxApi.UserManager
{
    public class UserGroupEntity:ErrorEntity
    {
        /// <summary>
        /// 分组ID
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 分组名
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 分组内用户数量
        /// </summary>
        public int count { get; set; }

        public static UserGroups QueryAll(string accessToken)
        {
            var url = string.Format("https://api.weixin.qq.com/cgi-bin/groups/get?access_token={0}", accessToken);
            return Utils.GetResult<UserGroups>(url);
        }
        /// <summary>
        /// 查询用户所在分组
        /// </summary>
        /// <param name="OpenID"></param>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public static QueryGroupEntity QueryByOpenID(string OpenID, string accessToken)
        {
            var url = string.Format("https://api.weixin.qq.com/cgi-bin/groups/getid?access_token={0}", accessToken);
            return Utils.PostResult<QueryGroupEntity>(new { openid = OpenID }, null);
        }
        /// <summary>
        /// 修改分组名
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public static ErrorEntity Update(int id, string name, string accessToken)
        {
            var url = string.Format("https://api.weixin.qq.com/cgi-bin/groups/update?access_token={0}", accessToken);
            var obj = new
            {
                group = new { id = id, name = name }
            };
            return Utils.PostResult<ErrorEntity>(obj, url);
        }
        /// <summary>
        /// 移动用户分组
        /// </summary>
        /// <param name="OpenID"></param>
        /// <param name="to_groupid"></param>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public static ErrorEntity UpdateOpenIdToGroup(string OpenID, int to_groupid, string accessToken)
        {
            var url = string.Format("https://api.weixin.qq.com/cgi-bin/groups/update?access_token={0}", accessToken);
            var obj = new
            {
                openid = OpenID,
                to_groupid = to_groupid
            };
            return Utils.PostResult<ErrorEntity>(obj, url);
        }
        /// <summary>
        /// 批量移动用户到指定的分组
        /// </summary>
        /// <param name="openid_list"></param>
        /// <param name="to_groupid"></param>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public static ErrorEntity UpdateOpenIdListToGroup(List<string> openid_list, int to_groupid, string accessToken)
        {
            var url = string.Format("https://api.weixin.qq.com/cgi-bin/groups/members/batchupdate?access_token={0}", accessToken);
            var obj = new
            {
                openid_list = openid_list,
                to_groupid = to_groupid
            };
            return Utils.PostResult<ErrorEntity>(obj, url);
        }

        public static UserInfo GetUserInfo(string authtoken, string openid)
        {
            var url = string.Format("https://api.weixin.qq.com/sns/userinfo?access_token={0}&openid={1}&lang=zh_CN", authtoken, openid);
            return Utils.GetResult<UserInfo>(url);
        }
    }
}
