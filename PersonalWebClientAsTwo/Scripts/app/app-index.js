/// <reference path="../jquery/jquery-1.10.2.js" />
/// <reference path="../linkServer/server.js" />
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

    function LoginIndex() {
        $('#btnLogin').click(function () {
            var login_email = $('#login_email').val();
            var login_pwd = $('#login_pwd').val();

            if (login_email == null || login_email == '' || login_email.length < 5 ||
                !login_email.match(/^([a-zA-Z0-9_-])+@([a-zA-Z0-9_-])+((\.[a-zA-Z0-9_-]{2,3}){1,2})$/)) {

                $('.alert-message .alert').text('你输入的Email地址格式有问题，请重新输入！').removeClass('hidden');
                window.setTimeout(function () {
                    $('.alert-message').addClass('hideen');
                }, 2000);
                return;
            }

            if (login_pwd == null || login_pwd == '' || !login_pwd.match(/^[A-Za-z0-9]{6}/)) {
                $('.alert-message .alert').text('你输入的密码格式有问题，请重新输入！').removeClass('hidden');
                window.setTimeout(function () {
                    $('.alert-message').addClass('hideen');
                }, 2000);
                return;
            }

            var xyCoord = GetCoord();
            var userLogin = {
                "UserName": login_email,
                "PassWord": login_pwd,
                "ValidateCode": 'daiding'
            };

        });
    }
    LoginIndex();



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

    $('#img').click(function (e) {
        var x = e.offsetX - $('body').offset().top - 7;
        var y = e.offsetY - $('body').offset().left - 7;
        var style = 'style="left:' + x + 'px;top:' + y + 'px;"';
        var span = '<span class="yz-span" ' + style + '></span>';
        $('#yz-Content').append(span);
        bindingClick();
    });

    function bindingClick() {
        $('.yz-span').on({
            'click': function () {
                $(this).remove();
            }
        });
    }

    function GetCoord() {
        var x1 = 0, x2 = 0, y1 = 0, y2 = 0;
        if ($('.yz-span').length > 0) {
            var x1Str = $($('.yz-span')[0]).css('left');
            var y1Str = $($('.yz-span')[0]).css('top');
            x1 = x1Str.substr(0, x1Str.length - 2);
            y1 = y1Str.substr(0, y1Str.length - 2);
            x2 = x1;
            y2 = y1;
        } else {
            return null;
        }
        if ($('.yz-span').length > 1) {
            var x2Str = $($('.yz-span')[1]).css('left');
            var y2Str = $($('.yz-span')[1]).css('top');
            x2 = x2Str.substr(0, x2Str.length - 2);
            y2 = y2Str.substr(0, y2Str.length - 2);
        }
        $('.yz-span').remove();
        return { 'x1': x1, 'y1': y1, 'x2': x2, 'y2': y2 };
    }

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

    $('#userId').popover('hide');
    $.philter();
});


function InitializeUser() {

}