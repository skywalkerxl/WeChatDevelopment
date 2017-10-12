using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WxApi.ReceiveEntity;

namespace WxApi.UserManager
{
    public class UserListEntity : ErrorEntity
    {
        /// <summary>
        /// 关注该公众号的总用户数
        /// </summary>
        public int total { get; set; }
        /// <summary>
        /// 拉取得opendi个数,最大数为10000
        /// </summary>
        public int count { get; set; }
        /// <summary>
        /// 列表数据，openid的列表
        /// </summary>
        public OpenidList data { get; set; }
        /// <summary>
        /// 拉去列表最后一个用户的openid
        /// </summary>
        public string next_openid { get; set; }
    }

    public class OpenidList
    {
        public List<string> openid { get; set; }
    }
}
