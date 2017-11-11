$(function () {
    $('#class-recommend').delegate('li', 'click', function () {
        $(this).parent().addClass('dhidden');
        $('#search-content-wrap').removeClass('dhidden');
    });

    // 解决键盘输入搜索的问题
    $('#searchInput').bind('search', function (event) {
        $('#class-recommend').addClass('dhidden');
        $('#search-content-wrap').removeClass('dhidden');
        event.preventDefault();
    })
});