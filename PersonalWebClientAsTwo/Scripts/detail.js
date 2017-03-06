$('#userIntro').each(function () {
    var element = $(this);
    var txt = '用户信息';
    element.popover({
        trigger: 'manual',
        placement: 'right', //top, bottom, left or right
        title: txt,
        html: 'true',
        content: $('#userIntroTemplate').html(),
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
    $('#userIntro').popover('hide');
});

$('.anchor_PlayTour').each(function () {
    var element = $(this);
    var txt = '谢谢大佬打赏O(∩_∩)O~~';
    element.popover({
        trigger: 'manual',
        placement: 'left', //top, bottom, left or right
        title: txt,
        html: 'true',
        content: $('#QRcodeTemplate').html(),
    }).on("mousedown", function () {
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
$('.anchor_PlayTour').popover('hide');

$(function () {
    var userCommpntTemplate = $('#userCommentTemplate');
    for (var i = 1; i < 6; i++) {
        var templateBu = userCommpntTemplate.html().format(i, "张三" + i, "2016-12-5 17:17", i + "wahahahahahahhaha哇哈哈哈哈" + i);
        $('#userCommont').append(templateBu);
    }
});