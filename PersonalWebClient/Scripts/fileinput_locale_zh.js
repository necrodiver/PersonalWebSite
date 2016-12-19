/*!
 * FileInput Spanish (Latin American) Translations
 *
 * This file must be loaded after 'fileinput.js'. Patterns in braces '{}', or
 * any HTML markup tags in the messages must not be converted or translated.
 *
 * @see http://github.com/kartik-v/bootstrap-fileinput
 *
 * NOTE: this file must be saved in UTF-8 encoding.
 */
(function ($) {
    "use strict";
    $.fn.fileinput.locales.es = {
    		fileSingle: '单个文件',
            filePlural: '多个文件',
            browseLabel: '选择文件 &hellip;',
            removeLabel: '删除文件',
            removeTitle: '删除选中文件',
            cancelLabel: '取消',
            cancelTitle: '取消上传',
            uploadLabel: '上传',
            uploadTitle: '上传选中文件',
            msgSizeTooLarge: '文件 "{name}" (<b>{size} KB</b>) 超过最大允许上传的大小 <b>{maxSize} KB</b>. 请重新上传!',
            msgFilesTooLess: '文件数量必须大于 <b>{n}</b> {files} ，请重新上传！',
            msgFilesTooMany: '选择上传的文件数量 <b>({n})</b> 超过最大允许的上传数量 <b>{m}</b>.请重新上传!',
            msgFileNotFound: '文件 "{name}" 未找到!',
            msgFileSecured: '安全限制防止读取文件 "{name}".',
            msgFileNotReadable: '文件 "{name}" 不是可读的.',
            msgFilePreviewAborted: '文件预览失败 "{name}".',
            msgFilePreviewError: '读取文件时发生一个错误 "{name}".',
            msgInvalidFileType: '无效的文件类型 "{name}". 只有 "{types}" 支持文件.',
            msgInvalidFileExtension: '无效的扩展文件 "{name}". 只有 "{extensions}" 支持文件.',
            msgValidationError: '文件上传错误',
            msgLoading: '加载文件 {index} 路径 {files} &hellip;',
            msgProgress: '加载文件 {index} 对于 {files} - {name} - {percent}% completed.',
            msgSelected: '选中{n}个文件',
            msgFoldersNotAllowed: '拖拽文件 {n} 文件夹(s) dropped were skipped.',
            dropZoneTitle: '你可以把图片文件拖进此框中'
    };

    $.extend($.fn.fileinput.defaults, $.fn.fileinput.locales.es);
})(window.jQuery);
