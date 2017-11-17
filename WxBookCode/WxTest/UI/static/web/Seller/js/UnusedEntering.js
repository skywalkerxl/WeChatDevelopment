$(function () {
    $.ajax({
        url: 'Handler/Unused/UnusedEntering.ashx',
        data: {
            option: 'getAuth'
        },
        success: function (data) {
            // alert(data);
        },
        error: function (res) {

        }
    })
})