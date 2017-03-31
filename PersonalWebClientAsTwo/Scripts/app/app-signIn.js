/// <reference path="../jquery/jquery-1.10.2.js" />
/// <reference path="../bootstrap/bootstrap.js" />
/// <reference path="../bootstrap/bootstrapValidator.js" />
/// <reference path="../linkServer/server.js" />

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
            },
            vcf: {
                message: '验证码无效',
                validators: {
                    notEmpty: {
                        message: '验证码不能为空'
                    },
                    regexp: {
                        regexp: /^^[A-Za-z0-9]{6}$/,
                        message: '验证码只能是数字或字母组成的6位长度'
                    }
                }
            }
            //
        }
    }).on("success.form.bv", function (e) {
        e.preventDefault();
        var sin_email = $("#sin_email").val();
        var sin_pwd = $("#sin_pwd").val();
        var sin_vcf = $("#sin_vcf").val();
        var userLogin = {
            "Email": sin_email,
            "PassWord": sin_pwd,
            "ValidateCode": sin_vcf
        };

        $server.accessToData("user_Login", userLogin, function (data) {
            swal(data.title, data.message, data.isRight ? 'success' : 'error');
            if (data.isRight) {
                $("#signInForm").data('bootstrapValidator').destroy();
                $('#signInForm').data('bootstrapValidator', null);
                location.href = '../Home/Index';
            } else {
                $('#signInImage').attr('src', 'GetVerificationCode2?time=' + new Date().getMilliseconds());
            }
        });
    });

    $('#signInImage').click(function () {
        $('#signInImage').attr('src', 'GetVerificationCode2?time='+new Date().getMilliseconds());
    });
});