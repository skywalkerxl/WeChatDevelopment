$(function () {
    $(document.body).pullToRefresh();

    $(document.body).on("pull-to-refresh", function () {
        setTimeout(function () {
            $(document.body).pullToRefreshDone();
            setTimeout(function () {
                $.toast("刷新成功");
            }, 400)
        }, 3000)
        
    });

    

    $('#list-content').delegate('li div.opt-btn', 'click', function () {
        $.modal({
            title: '物品操作',
            text: '点击下方按钮修改物品状态',
            buttons: [
                { text: "下架", onClick: function () { console.log(1) } },
                { text: "售出", className: 'warn', onClick: function () { console.log(2) } },
                { text: "取消", className: "default", onClick: function () { console.log(3) } },
            ]
        })
    })
});
