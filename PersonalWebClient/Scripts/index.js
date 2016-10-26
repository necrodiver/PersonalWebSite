if (!String.prototype.format) {
    String.prototype.format = function () {
        /// <summary>
        /// 这是一个实例方法，用法类似于C#的String.Format
        /// </summary>
        /// <param name="arguments" type="arguments">要设置格式的对象.</param>
        var args = arguments;
        return this.replace(/{(\d+)}/g, function (match, number) {
            return typeof args[number] != 'undefined'
                ? args[number]
                : match
            ;
        });
    };
}


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
            content: ContentMethod1(),
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
        return '<table class="table table-default" style="width:200px;">' +
                    '<tr><td style="width:80px;"><i class="glyphicon glyphicon-user"></i> 昵称</td><td>STAR</td></tr>' +
                    '<tr><td style="width:80px;"><i class="glyphicon glyphicon-star-empty"></i> 等级</td><td>lv.4</td></tr>' +
                    '<tr><td style="width:80px;"><i class="glyphicon glyphicon-bell"></i> 消息</td><td><a href="javascript:(0);">12</a></td></tr>' +
                    '<tr><td style="width:80px;">涂鸦</td><td><a href="javascript:(0);">53</a></td></tr>' +
                    '<tr><td colspan="2" text-align="center"><button type="button" class="btn btn-default"><i class="glyphicon glyphicon-signout"></i> 退出</button></td></tr>'
        '</table>';
    }
    function ContentMethod1() {
        return '<table class="table table-default" style="width:248px;">' +
      '<tr>' +
          '<td colspan="3">' +
              '<div class="pull-left">' +
                  '<a href="#"><img class="img-circle user-img" src="../Images/user-ceshi.jpg" /></a>' +
              '</div>' +
              '<div class="pull-left user-name">' +
                  '<a href="#" style="color:black;">&nbsp;日狗啊日狗，日个狗！</a>' +
              '</div>' +
              '<div class="pull-left user-chick-p">' +
                  '<button class="btn btn-default user-chick" id="user-check">签到</button>' +
              '</div>' +
          '</td>' +
      '</tr>' +
      '<tr>' +
         '<td colspan="3">' +
              '<div class="pull-left user-dgf">' +
                  '<a href="#" class="user-dgf-link">' +
                      '<div class="user-dgf-link-1"><span>12314</span></div>' +
                      '<div class="user-dgf-link-2"><span>动态</span></div>' +
                  '</a>' +
              '</div>' +
              '<div class="pull-left user-dgf">' +
                  '<a href="#" class="user-dgf-link">' +
                      '<div class="user-dgf-link-1"><span>2423</span></div>' +
                      '<div class="user-dgf-link-2"><span>关注</span></div>' +
                 ' </a>' +
              '</div>' +
              '<div class="pull-left user-dgf-1">' +
                  '<a href="#" class="user-dgf-link">' +
                      '<div class="user-dgf-link-1"><span>236234</span></div>' +
                      '<div class="user-dgf-link-2"><span>粉丝</span></div>' +
                  '</a>' +
              '</div>' +
          '</td>' +
      '</tr>' +
      '<tr>' +
          '<td colspan="2" class="user-dxg"><i class="glyphicon glyphicon-star-empty"></i> 等级</td>' +
          '<td><a href="#">Lv.4<i class="glyphicon glyphicon-chevron-right pull-right  user-right"></i></a></td>' +
      '</tr>' +
      '<tr>' +
          '<td colspan="2" class="user-dxg"><i class="glyphicon glyphicon-bell"></i> 消息</td>' +
          '<td><a href="#">12<i class="glyphicon glyphicon-chevron-right pull-right user-right"></i></a></td>' +
      '</tr>' +
      '<tr>' +
          '<td colspan="3" class="user-dxg"><i class="glyphicon glyphicon-cog"></i> <a href="#" class="user-set">个人信息设置<i class="glyphicon glyphicon-chevron-right pull-right  user-right"></i></a></td>' +
      '</tr>' +
      '<tr>' +
          '<td colspan="3" class="center-block"><button type="button" class="btn btn-default"><i class="glyphicon glyphicon-log-out"></i> 退出</button></td>' +
      '</tr>' +
  '</table>'
    }

    function ContentMethod2() {
        return '<div class="user-login-content">' +
            '<div class="user-login-prompt">' +
                '<div class="pull-left">' +
                    '<a href="#" title="当前未登录"><img class="img-circle user-img" src="../Images/user-ceshi.jpg" / ></a>' +
                '</div>' +
            '<div class="pull-left user-login-prompt-2">' +
                '<span>你的用户还未登录？</span>' +
            '</div>' +
        '</div>' +
        '<div class="user-login-dp">' +
            '<div><button class="btn btn-primary btn-sm user-login-d">登录<i class="glyphicon glyphicon-chevron-right pull-right"></i></button></div>' +
            '<div><button class="btn btn-info btn-sm user-login-d">注册<i class="glyphicon glyphicon-chevron-right pull-right"></i></button></div>' +
            '</div>' +
        '</div>';
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
    for (var i = 0; i < 5; i++) {
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