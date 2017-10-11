using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WxApi
{
    public class QrCode
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj">POST的数据实体</param>
        /// <param name="accessToken">接口调用凭据</param>
        /// <returns></returns>
        private static QrTicket Create(object obj, string accessToken)
        {
            var url = string.Format("https://api.weixin.qq.com/cgi-bin/qrcode/create?access_token={0}", accessToken);
            return Utils.PostResult<QrTicket>(obj, url);
        }
        /// <summary>
        /// 生成临时二维码
        /// </summary>
        /// <param name="expireSeconds">过期时间，最大不超过604800，即7天</param>
        /// <param name="sceneId">场景值ID，临时二维码时为32位非0整型</param>
        /// <param name="accessToken">accessToken</param>
        /// <returns>ticket 实体，ticket可以换取二维码，也可以根据URL自行生成</returns>
        public static QrTicket CreateTemp(int expireSeconds, int sceneId, string accessToken)
        {
            if(expireSeconds<=0||expireSeconds>1800)
            {
                return new QrTicket { ErrCode = -3, ErrDescription = "有效时间不合法" };
            }
            var json = new
            {
                expire_seconds = expireSeconds,
                action_name = "QR_SCENE",
                action_info = new { scene = new { scene_id = sceneId } }
            };
            return Create(json, accessToken);
        }

        /*public static QrTicket CreateByID(int sceneId, string accessToken)
        {
            if(sceneId < 1 || sceneId > 10000)
            {
                return new QrTicket { ErrCode = -3, ErrDescription = "场景值不合法，有效范围 1-1000000" };
            }
            var json = new 
            {
                action_name = "QR_LIMIT_SCENE",
                action_name = new {scene_id = sceneId}
            }
        }*/
    }
}
