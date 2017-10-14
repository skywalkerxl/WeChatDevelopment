using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WxApi.MsgEntity;

namespace WxTest
{
    public partial class TemplateTest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var accessToken = AccessTokenBox.GetTokenValue("wxf50808b364418ffb", "bfaf8363dc64787091b3bbb7740dcf44");
            var dickKeys = new Dictionary<string, TemplateKey>();
            dickKeys.Add("tradeDateTime", new TemplateKey
            {
                value = DateTime.Now.ToString(),
                color = "#cc0000",
            });
            dickKeys.Add("orderFrom", new TemplateKey
            {
                value = "微信端",
                color = "#cc0000"
            });
            dickKeys.Add("customerInfo", new TemplateKey
            {
                value = "张三 电话:110",
                color = "#cc0000"
            });
            dickKeys.Add("orderInfo", new TemplateKey
            {
                value = "卡迪拉克",
                color = "#cc0000"
            });
            dickKeys.Add("remark", new TemplateKey
            {
                value = "请及时确认",
                color = "#cc0000"
            });
            var d = TemplateNotice.Send("o5F3X0iVqFiqV8gNyQMapZFTLPGc","EUfxyqBkvVyOPfD3_Tts8qi2Imsos_1oC2gD_hJ8nT0", "#cccccc", dickKeys, accessToken);
            if(d.ErrCode == 0)
            {
                Response.Write("模板消息发送成功！消息ID为：" + d.msgid);
            }
            else
            {
                Response.Write("模板消息发送失败!错误消息是:" + d.ErrDescription);
            }
        }
    }
}