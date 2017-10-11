using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WxApi.ReceiveEntity;

namespace WxApi.UserManager
{
    /// <summary>
    /// 根据openid查询分组ID返回的实体
    /// </summary>
    public class QueryGroupEntity:ErrorEntity
    {
        public int groupid { get; set; }
    }
}
