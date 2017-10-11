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

        //public static ErrorEntity 
    }
}
