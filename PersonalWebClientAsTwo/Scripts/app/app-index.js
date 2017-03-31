/// <reference path="../jquery/jquery-1.10.2.js" />
/// <reference path="../linkServer/server.js" />
$(function () {
    function FillModule() {
        for (var i = 1; i < 9; i++) {
            var data = $("#Imageformat").html().format(i);
            $("#scrawl_New").append(data);
        }
        for (var i = 9; i < 17; i++) {
            var data = $("#Imageformat").html().format(i);
            $("#scrawl_Top").append(data);
        }
        for (var i = 27; i < 35; i++) {
            var data = $("#Imageformat").html().format(i);
            $("#scrawl_RE").append(data);
        }
        for (var i = 0; i < 6; i++) {
            var data = $("#Jottingformat").html().format(i);
            $("#jotting_New").append(data);
            $("#jotting_Top").append(data);
            $("#jotting_RE").append(data);
        }
    }
    //小模块填充页面
    FillModule();
    //图片灰度展示

    var $container1 = $('.jotting_parent');
    $container1.imagesLoaded(function () {
        $container1.masonry({
            itemSelector: '.jotting_thumbnail',
            isAnimated: true
        });
    });

    var $container = $('.moduleParent');
    $container.imagesLoaded(function () {
        $container.masonry({
            itemSelector: '.img_thumbnail',
            isAnimated: true
        });
    });
    $.philter();
});
