using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WxApi.ReceiveEntity;

namespace WxApi.UserManager
{
    /// <summary>
    /// 用户信息实体
    /// </summary>
    public class UserInfo:ErrorEntity
    {
        /// <summary>
        /// 是否关注
        /// </summary>
        public int subscribe { get; set; }
        
        public string openid { get; set; }
        /// <summary>
        /// 昵称
        /// </summary>
        public string nickname { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public int sex { get; set; }
        /// <summary>
        /// 语言
        /// </summary>
        public string language { get; set; }
        /// <summary>
        /// 城市
        /// </summary>
        public string city { get; set; }
        /// <summary>
        /// 广东
        /// </summary>
        public string province { get; set; }
        /// <summary>
        /// 中国
        /// </summary>
        public string country { get; set; }
        /// <summary>
        /// 图像
        /// </summary>
        public string headingurl { get; set; }
        /// <summary>
        /// 关注事件
        /// </summary>
        public int subscribe_time { get; set; }
        
        public string unionId { get; set; }
        /// <summary>
        /// 用户备注
        /// </summary>
        public string remark { get; set; }

        public string privilege { get; set; }
    }
}
