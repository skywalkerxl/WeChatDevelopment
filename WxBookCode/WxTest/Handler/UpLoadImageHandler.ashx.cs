using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WxApi;
using WxApi.ReceiveEntity;


namespace WxTest.Handler
{
    /// <summary>
    /// UpLoadImageHandler 的摘要说明
    /// </summary>
    public class UpLoadImageHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            var accessToken = AccessTokenBox.GetTokenValue("wxf50808b364418ffb", "bfaf8363dc64787091b3bbb7740dcf44");
            var res = MaterialLib.Add("D:/MyRepository/WeChatDevelopment/Material/voice/test.mp3", accessToken, MaterialType.voice, false, "voiceTitleDemo", "videoIntroDemo");
            if (res.ErrCode == 0)
            {
                context.Response.Write("语音消息发送成功！media_id为：" + res.media_id);
            }
            else
            {
                context.Response.Write("语音消息发送失败!错误消息是:" + res.ErrDescription);
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}