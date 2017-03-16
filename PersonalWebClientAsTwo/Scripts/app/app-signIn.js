/// <reference path="../jquery/jquery-1.10.2.js" />
/// <reference path="../bootstrap/bootstrap.js" />
/// <reference path="../bootstrap/bootstrapValidator.js" />

$(document).ready(function () {
    $('#signInForm').bootstrapValidator({
        message: '这个值是无效的',
        feedbackIcons: {/*输入框不同状态，显示图片的样式*/
            valid: 'fa fa-check',
            invalid: 'fa fa-close',
            validating: 'fa fa-refresh'
        },
        fields: {/*验证*/
            email: {
                validators: {
                    notEmpty: {
                        message: '请输入登录账号'
                    },
                    emailAddress: {
                        message: '输入的不是一个有效的电子邮件地址'
                    }
                    //,
                    //remote: {/*远程验证*/
                    //    url: 'remote.aspx',
                    //    message: '此电子邮箱已被使用，请输入正确的邮箱地址'
                    //}
                }
            },
            password: {
                message: '密码无效',
                validators: {
                    notEmpty: {
                        message: '密码不能为空'
                    },
                    stringLength: {
                        min: 6,
                        max: 20,
                        message: '密码长度必须在6到20之间'
                    },
                    different: {
                        field: 'username',
                        message: '密码不能和用户名相同'
                    },
                    regexp: {
                        regexp: /^((?!\d+$)(?![a-zA-Z]+$)[a-zA-Z\d@@#$%^&_+].{5,19})+$/,
                        message: '密码只能由字母、数字、字符的最少两个组合'
                    }
                }
            }
        }
    });
});