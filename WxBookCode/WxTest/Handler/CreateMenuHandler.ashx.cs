﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WxApi.ReceiveEntity;
using WxApi.SendEntity;
using WxApi.MsgEntity;
using WxApi;
using WxApi.UserManager;

namespace WxTest.Handler
{
    /// <summary>
    /// CreateMenuHandler 的摘要说明
    /// </summary>
    public class CreateMenuHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            #region 创建菜单按钮
            string appid = "wxf50808b364418ffb";
            string appSerect = "bfaf8363dc64787091b3bbb7740dcf44";
            var accessToken = AccessTokenBox.GetTokenValue(appid, appSerect);
            var child1 = new List<BaseMenu>();
            var child2 = new List<BaseMenu>();
            var child3 = new List<BaseMenu>();
            var basebtn = new List<BaseMenu>();
            string rUrl = OAuth.GetAuthUrl("wxf50808b364418ffb", "http://skywalkerxl.free.ngrok.cc/UnusedEntering.html", "success");



            child1.Add(new BaseMenu
            {
                url = rUrl,
                name = "闲置对比",
                type = MenuType.view
            });
            child1.Add(new BaseMenu
            {
                url = "http://skywalkerxl.free.ngrok.cc/UnusedEntering.html",
                name = "闲置录入",
                type = MenuType.view
            });

            child2.Add(new BaseMenu
            {
                key = "SelectionClick",
                name = "精选好物",
                type = MenuType.click
            });
            child2.Add(new BaseMenu
            {
                url = "http://skywalkerxl.free.ngrok.cc/SearchYouWant.html",
                name = "搜你所想",
                type = MenuType.view
            });

            child3.Add(new BaseMenu
            {
                url = "http://skywalkerxl.free.ngrok.cc/MyInfomation.html",
                name = "我的信息",
                type = MenuType.view
            });

            child3.Add(new BaseMenu
            {
                url = "http://skywalkerxl.free.ngrok.cc/RecentActivity.html",
                name = "最近活动",
                type = MenuType.view
            });
            child3.Add(new BaseMenu
            {
                url = "http://skywalkerxl.free.ngrok.cc/UseGuide.html",
                name = "使用指南",
                type = MenuType.view
            });
            child3.Add(new BaseMenu
            {
                url = "http://skywalkerxl.free.ngrok.cc/FeedBack.html",
                name = "意见反馈",
                type = MenuType.view
            });
            child3.Add(new BaseMenu
            {
                url = "http://skywalkerxl.free.ngrok.cc/AboutUs.html",
                name = "关于我们",
                type = MenuType.view
            });

            basebtn.Add(new BaseMenu
            {
                name = "出售闲置",
                sub_button = child1
            });
            basebtn.Add(new BaseMenu
            {
                name = "精选好物",
                sub_button = child2
            });
            basebtn.Add(new BaseMenu
            {
                name = "查看更多",
                sub_button = child3
            });

            /*
             child1.Add(new BaseMenu
            {
                key = "我是click按钮",
                name = "Click按钮",
                type = MenuType.click
            });
            child1.Add(new BaseMenu
            {
                key = "我是选择地理位置按钮",
                name = "选择地理位置",
                type = MenuType.location_select
            });
            child1.Add(new BaseMenu
            {
                url = "http://skywalkerxl.free.ngrok.cc/WxJs.aspx",
                name = "跳转链接",
                type = MenuType.view
            });

            child2.Add(new BaseMenu
            {
                key = "我是扫码事件按钮",
                name = "扫码推事件",
                type = MenuType.scancode_push
            });
            child2.Add(new BaseMenu
            {
                key = "我是扫码推事件按钮且弹出消息接收中",
                name = "扫码等待",
                type = MenuType.scancode_waitmsg
            });

            child3.Add(new BaseMenu
            {
                key = "我是拍照或相册按钮",
                name = "拍照或相册",
                type = MenuType.pic_photo_or_album
            });

            child3.Add(new BaseMenu
            {
                key = "我是系统拍照",
                name = "系统拍照",
                type = MenuType.pic_sysphoto
            });
            child3.Add(new BaseMenu
            {
                key = "我是弹出微信相册按钮",
                name = "微信相册",
                type = MenuType.pic_weixin
            });

            basebtn.Add(new BaseMenu
            {
                name = "常用菜单",
                sub_button = child1
            });
            basebtn.Add(new BaseMenu
            {
                name = "扫码",
                sub_button = child2
            });
            basebtn.Add(new BaseMenu
            {
                name = "发图",
                sub_button = child3
            });
            */

            var ret = WxApi.Menu.Create(new MenuEntity { button = basebtn }, accessToken);
            context.Response.Write("状态码：" + ret.ErrCode + "状态描述：" + ret.ErrDescription);
            #endregion
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