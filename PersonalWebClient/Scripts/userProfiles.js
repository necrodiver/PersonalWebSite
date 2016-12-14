var maxCount = 49;
var everyCount = 6;
var length = 0;
var imagesLoading = false;
var $container = $('#masonry');
var isP
//判断当前浏览器窗口滚动条是否离底部距离小于20
$(window).scroll(function () {

    //下边这一段不知道影响性能不
    $container.imagesLoaded(function () {
        $container.masonry({
            itemSelector: '.thumbnail',
            isAnimated: true,
            transitionDuration: '0.6s'
        });
    });
    //上方这段

    if ($(document).height() - $(window).height() - $(document).scrollTop() < 50) {
        if (!imagesLoading) {
            imagesLoading = true;
            appendToMasonry();
            imagesLoading = false;
        }
    }
});
function appendToMasonry() {
    var data = "";
    var l = Math.ceil(everyCount * length);
    var min = length * everyCount;
    if (everyCount * (length + 1) < maxCount) {
        var max = (length + 1) * everyCount;
        for (var i = min + 1; i < max; i++) {
            var r = parseInt(Math.random() * 18) + 1;
            //data += $("#Imageformat").html().format(i);
            //data += $("#Jottingformat").html().format(r, i);
            data += $('#workTemplate').html().format(i);
            data += $('#workArticleTemplate').html().format(i);
            data += $('#userTemplate').html().format(i);

        }
    }
    else if (min < maxCount && everyCount * (length + 1) > maxCount) {
        for (var j = min; j < maxCount; j++) {
            var r = parseInt(Math.random() * 19);
            //data += $("#Imageformat").html().format(j);
            //data += $("#Jottingformat").html().format(r, j);
            data += $('#workTemplate').html().format(i);
            data += $('#workArticleTemplate').html().format(i);
            data += $('#userTemplate').html().format(i);
        }
    }
    else
        return;
    ++length;

    var $data = $(data);
    if (length > 1) {
        if (true) {
            $container.imagesLoaded().always(function () {
                $container.append($data).masonry('appended', $data).masonry();
            });
        } else {
            $container.append($data).masonry('appended', $data).masonry();
        }
        return;
    }

    $container.append($data);
    $container.imagesLoaded(function () {
        $container.masonry({
            itemSelector: '.thumbnail',
            isAnimated: true,
            transitionDuration: '0.6s'
        });
    });
}

$(function () {
    appendToMasonry();
    $.philter();
});