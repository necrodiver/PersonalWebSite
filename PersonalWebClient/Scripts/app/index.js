$(function () {
    $(window).load(function () {
        $(".col-3 input").val("");
        $(".input-effect input").focusout(function () {
            if ($(this).val() != "") {
                $(this).addClass("has-content");
            } else {
                $(this).removeClass("has-content");
            }
        });
    });

    //这里进行改变是否登录了后的弹框
    function IsLogin(islogin) {
        if (!islogin) {
            $('#userBtn').attr('data-target', 'ASAS');
        }
    }


    $('#userId').each(function () {
        var element = $(this);
        var txt = '用户信息';
        element.popover({
            trigger: 'manual',
            placement: 'bottom', //top, bottom, left or right
            title: txt,
            html: 'true',
            content: ContentMethod(),
        }).on("mouseenter", function () {
            var _this = this;
            $(this).popover("show");
            $(this).siblings(".popover").on("mouseleave", function () {
                $(_this).popover('hide');
            });
        }).on("mouseleave", function () {
            var _this = this;
            setTimeout(function () {
                if (!$(".popover:hover").length) {
                    $(_this).popover("hide")
                }
            }, 100);
        });
    });

    function ContentMethod() {
        return $('#loggedOnUserTemplate').html();
    }

    function ContentMethod2() {
        return $('#signInTemplate').html();
    }

    $('#userId').popover('hide');

    $.post('VCode.ashx', { 'M': 'A' }, function (e) {
        $('#img').attr('src', e);
    });
    $('#img').click(function (e) {
        var x = e.offsetX - $('body').offset().top - 7;
        var y = e.offsetY - $('body').offset().left - 7;
        var style = 'style="left:' + x + 'px;top:' + y + 'px;"';
        var span = '<span class="yz-span" ' + style + '></span>';
        $('#yz-Content1').append(span);
        bindingClick();
    });
    $('#img2').click(function (e) {
        var x = e.offsetX - $('body').offset().top - 7;
        var y = e.offsetY - $('body').offset().left - 7;
        var style = 'style="left:' + x + 'px;top:' + y + 'px;"';
        var span = '<span class="yz-span" ' + style + '></span>';
        $('#yz-Content2').append(span);
        bindingClick();
    });
    function bindingClick() {
        $('.yz-span').on({
            'click': function () {
                $(this).remove();
            }
        });
    }

    function Tijiao() {
        var x1Str = $($('.yz-span')[0]).css('left');
        var y1Str = $($('.yz-span')[0]).css('top');
        var asas = 'adadadadad';
        var x1 = x1Str.substr(0, x1Str.length - 2);
        var y1 = y1Str.substr(0, y1Str.length - 2);
        var x2 = x1;
        var y2 = y1;
        if ($('.yz-span').length > 1) {
            var x2Str = $($('.yz-span')[1]).css('left');
            var y2Str = $($('.yz-span')[1]).css('top');
            x2 = x2Str.substr(0, x2Str.length - 2);
            y2 = y2Str.substr(0, y2Str.length - 2);
        }
        $.post('VCode.ashx', { 'M': 'B', 'x1': x1, 'y1': y1, 'x2': x2, 'y2': y2 }, function (e) {
            alert(e);
        });
        $('.yz-span').remove();
    }


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
    //图片灰度展示
    $.philter();

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
});