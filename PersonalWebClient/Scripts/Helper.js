$(function () {
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
});

$(function () {

    for (var i = 1; i < 224; i++) {
        var bd = "<div class='thumbnail'>" +
               "<div class='imgs'>" +
                    "<input type='hidden' value='test" + i + ".jpg'>" +
               "</div>" +
               "<div class='caption'>" +
                   "<div class='title'>图集第" + i + "张</div>" +
                       "<div class='content'>" +
                       "</div>" +
                   "<div class='author'>" +
                       "by <a target='_blank' href='http://www.notlives.cc/Image/Wallpaper/" + i + ".jpg'>NECC</a>" +
                   "</div>" +
               "</div>" +
            "</div>";
        var data = $("#htmlformat").html().format(i);
        $("#masonry_ghost").append(data);
    }

    var ghostNode = $('#masonry_ghost').find('.thumbnail'), i, ghostIndexArray = [];
    var ghostCount = ghostNode.length;
    for (i = 0; i < ghostCount; i++) {
        ghostIndexArray[i] = i;
    }
    ghostIndexArray.sort(function (a, b) {
        if (Math.random() > 0.5) {
            return a - b;
        } else {
            return b - a;
        }
    });

    var currentIndex = 0;
    var masNode = $('#masonry');
    var imagesLoading = false;


    function getNewItems() {
        var newItemContainer = $('<div/>');
        for (var i = 0; i < 6; i++) {
            if (currentIndex < ghostCount) {
                newItemContainer.append(ghostNode.get(ghostIndexArray[currentIndex]));
                currentIndex++;
            }
        }
        return newItemContainer.find('.thumbnail');
    }

    function processNewItems(items) {
        items.each(function () {
            var $this = $(this);
            var imgsNode = $this.find('.imgs');
            var title = $this.find('.title').text();
            var author = $this.find('.author').text();
            title += '&nbsp;&nbsp;(' + author + ')';
            var lightboxName = 'lightbox_'; // + imgNames[0];

            var imgNames = imgsNode.find('input[type=hidden]').val().split(',');
            $.each(imgNames, function (index, item) {
                imgsNode.append('<a href="Content/Images/' + item + '" data-lightbox="' + lightboxName + '" title="' + title + '"><img src="Content/Images/' + item + '" /></a>');
            });
        });
    }

    function initMasonry() {
        var items = getNewItems().css('opacity', 0);
        processNewItems(items);
        masNode.append(items);

        imagesLoading = true;
        items.imagesLoaded(function () {
            imagesLoading = false;
            items.css('opacity', 1);
            masNode.masonry({
                itemSelector: '.thumbnail',
                isFitWidth: true
            });
        });
    }


    function appendToMasonry() {
        var items = getNewItems().css('opacity', 0);
        processNewItems(items);
        masNode.append(items);

        imagesLoading = true;
        items.imagesLoaded(function () {
            imagesLoading = false;
            items.css('opacity', 1);
            masNode.masonry('appended', items);
        });
    }

    initMasonry();

    //判断当前浏览器窗口滚动条是否离底部距离小于10
    $(window).scroll(function () {

        if ($(document).height() - $(window).height() - $(document).scrollTop() < 20) {

            if (!imagesLoading) {
                appendToMasonry();
            }

        }

    });

    //此方法不可删除
    function randomColor() {
        var rand = Math.floor(Math.random() * 0xFFFFFF).toString(16);
        for (; rand.length < 6;) {
            rand = '0' + rand;
        }
        return '#' + rand;
    }
});