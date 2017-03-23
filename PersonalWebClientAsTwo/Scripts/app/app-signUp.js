/// <reference path="../linkServer/server.js" />
/// <reference path="../jquery/jquery-1.10.2.js" />
$(function () {
    $server.ceshi1(1001);
    $('#signUpForm').bootstrapValidator({
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
                        message: '电子邮件是必填的'
                    },
                    emailAddress: {
                        message: '输入的不是一个有效的电子邮件地址'
                    }
                    ,
                    remote: {/*远程验证*/
                        url: $server.getFullUrl('user_contrastEmailWeb'),
                        message: '此电子邮箱已被使用，请输入正确的邮箱地址',
                        type: 'POST',
                        crossDomain: true,
                        name: 'email',
                        dataType: 'json',
                        delay: 2000,
                        data: function (validator) {
                            return {
                                "": $('#sup_email').val()
                            };
                        }
                    }
                }
            },
            username: {
                message: '用户名无效',
                validators: {
                    notEmpty: {
                        message: '用户名不能为空'
                    },
                    stringLength: {
                        min: 4,
                        max: 20,
                        message: '用户名长度必须在4到20之间'
                    },
                    regexp: {
                        regexp: /^[\u4e00-\u9fa5a-zA-Z][\u4e00-\u9fa5-\-\·a-zA-Z0-9_\.]+$/,
                        message: '用户名只能是中文、字母、数字、_、-、·的组合，以中文或字母开头'
                    },
                    different: {
                        field: 'password',
                        message: '用户名不能和密码相同'
                    }
                    ,
                    remote: {
                        url: $server.getFullUrl('user_constractNickNameWeb'),
                        message: '此用户名已被使用，请换一个用户名尝试',
                        type: 'POST',
                        crossDomain: true,
                        name: 'email',
                        dataType: 'json',
                        delay: 2000,
                        data: function (validator) {
                            return {
                                "": $('#sup_nickname').val()
                            };
                        }
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
            confirmPassword: {
                message: '重复密码无效',
                validators: {
                    notEmpty: {},
                    identical: {
                        field: 'password',
                        message: '两次输入的密码不一致'
                    }
                }
            },
            verificationCode: {
                validators: {
                    notEmpty: {
                        message: '请输入验证码'
                    },
                    regexp: {
                        regexp: /^[A-Za-z0-9]{6}/,
                        message: '验证码必须是6位数字'
                    }
                }
            },
            vnotice: {
                validators: {
                    notEmpty: {
                        message: '如果未同意规则条件，将不能注册'
                    }
                }

            }
        }
    }).on("success.form.bv", function (e) {
        e.preventDefault();
        console.log("提交用户注册表单star");
        var sup_email = $("#sup_email").val();
        var sup_nickname = $("#sup_nickname").val();
        var sup_pwd = $("#sup_pwd").val();
        var sup_vcode = $("#sup_vcode").val();
        var userRegister = {
            "UserName": sup_email,
            "NickName": sup_nickname,
            "PassWord": sup_pwd,
            "ValidateCode": sup_vcode
        };
        $server.accessToData("user_register", userRegister, $server.logReturn);
        console.log("提交用户注册表单end");
    });

    $("#btnSendVCode").click(function () {
        var email = $("#sup_email").val();
        $server.accessToData("registerSendEmail", { "Email": email }, $server.logReturn);
    });
});