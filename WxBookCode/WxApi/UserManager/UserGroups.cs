using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace WxApi.UserManager
{
    public class UserGroups
    {
        public static UserGroupEntity Create(string name, string accessToken)
        {
            var url = string.Format("https://api.weixin.qq.com/cgi-bin/groups/create?access_token={0}", accessToken);
            var obj = new { group = new { name = name } };
            var Jobj = Utils.PostResult<JObject>(obj, url);
            JToken errcode = null;
            errcode = Jobj.GetValue("errcode");
            var ret = new UserGroupEntity();
            if(errcode == null) // 判断是否存在错误返回码。如果不存在，则说明分组创建成功
            {
                ret.ErrCode = 0;
                ret.id = Jobj["group"]["id"].Value<int>();
                ret.name = Jobj["group"]["name"].Value<string>();
            }
            else
            {
                ret.ErrCode = errcode.Value<int>();
            }
            return ret;
        }
        /// <summary>
        /// 创建映射实体
        /// </summary>
        public List<UserGroupEntity> groups { get; set; }
    }
}
