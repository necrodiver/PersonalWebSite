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
        return [{ 'x': x1, 'y': y1 }, { 'x': x2, 'y': y2 }];
    }

    function LoginIndex() {
        $('#btnLogin').click(function () {
            var login_email = $('#login_email').val();
            var login_pwd = $('#login_pwd').val();

            if (login_email == null || login_email == '' || login_email.length < 5 ||
                !login_email.match(/^([a-zA-Z0-9_-])+@([a-zA-Z0-9_-])+((\.[a-zA-Z0-9_-]{2,3}){1,2})$/)) {

                $('.alert-message>.alert').text('你输入的Email地址格式有问题，请重新输入！').css("display", "block");
                window.setTimeout(function () {
                    $('.alert-message>.alert').css("display", "none");
                }, 2000);
                return;
            }

            if (login_pwd == null || login_pwd == '' || !login_pwd.match(/^[A-Za-z0-9]{6}$/)) {
                $('.alert-message>.alert').text('你输入的密码格式有问题，请重新输入！').css("display", "block");
                window.setTimeout(function () {
                    $('.alert-message>.alert').css("display", "none");
                }, 2000);
                return;
            }

            var xyCoord = GetCoord();
            if (xyCoord == null) {
                $('.alert-message>.alert').text('当前验证码无效，请操作验证码验证！').css("display", "block");
                window.setTimeout(function () {
                    $('.alert-message>.alert').css("display", "none");
                }, 2000);
                return;
            }
            var userLogin = {
                "UserName": login_email,
                "PassWord": login_pwd,
                "ValidateCode": xyCoord
            };

            $server.accessToData("user_LoginIndex", userLogin, function (data) {
                if (data.isRight) {
                    $('#myModal').modal('hide');
                    swal(data.title, data.message, data.isRight ? 'success' : 'error');
                    window.location.reload();
                } else {
                    $('.alert-message>.alert').text(data.message).css("display", "block");
                    window.setTimeout(function () {
                        $('.alert-message>.alert').css("display", "none");
                    }, 2000);
                }
                $('#img').attr('src', $server.getFullUrl('getVFCIndex') + '?time=' + new Date().getMilliseconds());
            });

        });

        $('#yz-Refresh').click(function () {
            $('#img').attr('src', $server.getFullUrl('getVFCIndex') + '?time=' + new Date().getMilliseconds());
        });
    }

    LoginIndex();

    $('#userId').click(function () {
        $('#img').attr('src', $server.getFullUrl('getVFCIndex') + '?time=' + new Date().getMilliseconds());
    });


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
    $('#userId').popover('hide');
});


function InitializeUser() {

}