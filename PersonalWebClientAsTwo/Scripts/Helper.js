/// <reference path="jquery/jquery-1.10.2.js" />

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

function setTimeoutThis(sid,msg) {
    $(sid).attr('disabled', true);
    var lastSecond = 2 * 60;
    var lasttime = 0;
    var timer = setInterval(function () {
        if (lastSecond == 0) {
            $(sid).attr('disabled', false).text(msg);
            window.clearTimeout(timer);
            return;
        }
        $(sid).text(lastSecond + "秒 后可用");
        lastSecond--;
    }, 1000);
}
