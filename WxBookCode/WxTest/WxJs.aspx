<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WxJs.aspx.cs" Inherits="WxTest.WxJs" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta name="viewport" content="width=device-width,userscalable=no"/>
    <title>JS-SDK测试页面</title>
    <script>
        (function () {
            var html = document.documentElement;
            var hWidth = html.getBoundingClientRect().width;
            html.style.fontSize = hWidth / 15 + "px";
        })();
    </script>
    <script src="https://cdn.bootcss.com/jquery/1.12.0/jquery.min.js"></script>
    <script src="http://res.wx.qq.com/open/js/jweixin-1.2.0.js"></script>
    
    <script type="text/javascript">
        wx.config({
            debug: false, // 开启调试模式，调试的所有api的返回值会在客户端alert出来
            appId: 'wxf50808b364418ffb', // 必填，公众号的唯一标识
            timestamp: '<%=timestamp%>', // 必填，生成签名的时间戳
            nonceStr: '<%=noncestr%>', // 必填，生产签名的唯一戳
            signature: '<%=sign%>', // 必填，签名
            jsApiList: [
                'checkJsApi',
                'onMenuShareTimeline',
                'onMenuShareAppMessage',
                'onMenuShareWeibo',
                'onMenuShareQQ',
                'chooseImage'
            ] // 必填，需要使用的JS接口列表，所有JS接口列表见开发文档
        });

        wx.ready(function () {
            $('#checkJsApi').click(function () {
                wx.checkJsApi({
                    jsApiList: [
                        'getNetworkType',
                        'previewImage',
                        'chooseImage'
                    ],
                    success: function (res) {
                        alert(JSON.stringify(res));
                    }
                });
            });

            $('#UpLoadImg').click(function () {
                wx.chooseImage({
                    success: function (res) {
                        alert(JSON.stringify(res));
                        var localIds = res.localIds;
                    }
                });
            });
            
            wx.onMenuShareTimeline({
                title: '我要分享到朋友圈', // 分享标题
                link: 'http://www.baidu.com', // 分享链接
                imgUrl: '', // 分享图标
                success: function () {
                    alert('分享成功');
                    // 用户确认分享后执行的回调函数
                },
                cancel: function () {
                    alert("取消分享了");
                    // 用户取消分享后执行的回调函数
                }
            });

            wx.onMenuShareTimeline({
                title: '我要分享给朋友',
                link: 'http://www.baidu.com',
                imgUrl: '',
                success: function () {
                    alert("分享成功");
                    // 用户确认分享后执行的回调函数
                },
                cancel: function () {
                    alert("用户取消了分享");
                    // 用户取消分享后执行的回调函数
                }
            });

            wx.onMenuShareQQ({
                title: '我要分享到QQ',
                link: 'http://www.baidu.com',
                imgUrl: '',
                success: function () {
                    alert('分享成功');
                    // 用户确认分享后执行的回调函数
                },
                cancel: function () {
                    alert('取消分享了');
                    // 用户取消分享后执行的回调函数
                }
            })
        });

    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <input type="button" value="checkJsApi" id="checkJsApi"/>
        <input type="button" value="上传图片" id="UpLoadImg"/>
        <input type="button" value="下载图片" id="DownLoadImg"/>
    </div>
    </form>
</body>
</html>