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