<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WxJs.aspx.cs" Inherits="WxTest.WxJs" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <script src="https://cdn.bootcss.com/jquery/1.12.0/jquery.min.js"></script>
    <script src="http://res.wx.qq.com/open/js/jweixin-1.2.0.js"></script>
    <script type="text/javascript">
        wx.config({
            debug: false,
            appId: 'wxf50808b364418ffb',
            timestamp: '<%=timestamp%>',
            nonceStr: '<%=noncestr%>',
            signature: '<%=sign%>',
            jsApiList: ['checkJsApi']
        });

        wx.ready(function () {
            $('#checkJsApi').click(function () {
                wx.checkJsApi({
                    jsApiList: [
                        'getNetworkType',
                        'previewImage'
                    ],
                    success: function (res) {
                        alert(JSON.stringify(res));
                    }
                });
            });
        });

    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <input type="button" value="checkJsApi" id="checkJsApi"/>
    </div>
    </form>
</body>
</html>