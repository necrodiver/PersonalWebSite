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