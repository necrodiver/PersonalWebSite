$(document).ready(function () {
    $('#retrievePassword1').bootstrapValidator({
        message: '这个值是无效的',
        feedbackIcons: {/*输入框不同状态，显示图片的样式*/
            valid: 'fa fa-check',
            invalid: 'fa fa-close',
            validating: 'fa fa-refresh'
        },
        fields: {/*验证*/
            rtpwd_email: {
                validators: {
                    notEmpty: {
                        message: '请输入登录账号'
                    },
                    emailAddress: {
                        message: '输入的不是一个有效的电子邮件地址'
                    },
                    remote: {/*远程验证*/
                        url: $server.getFullUrl('registerReEmail'),
                        message: '当前邮箱地址未注册，请重新输入',
                        type: 'POST',
                        crossDomain: false,//是否跨域
                        name: 'rtpwd_email',
                        dataType: 'json',
                        delay: 2000,
                        data: function (validator) {
                            return {
                                email: $('#rtpwd_email').val()
                            };
                        }
                    }
                }
            },
            rtpwd_verificationCode: {
                message: '验证码无效',
                validators: {
                    notEmpty: {
                        message: '验证码不能为空'
                    },
                    regexp: {
                        regexp: /^[A-Za-z0-9]{6}/,
                        message: '验证码必须是6位数字'
                    }
                }
            }
        }
    }).on("success.form.bv", function (e) {
        e.preventDefault();
        //验证email和密码是否正确
        var rtpwd_email = $("#rtpwd_email").val();
        var rtpwd_verificationCode = $('#rtpwd_verificationCode').val();

        var vcfAndEmail = {
            "Email": rtpwd_email,
            "ValidateCode": rtpwd_verificationCode
        };
        $server.accessToData("retrieveVFCAndEmail", vcfAndEmail, function (data) {
            swal(data.title, data.message, data.isRight ? 'success' : 'error');
            if (data.isRight) {
                $("#retrievePassword1").data('bootstrapValidator').destroy();
                $('#retrievePassword1').data('bootstrapValidator', null);
                location.href = 'RetrievePwdSet';
            }
        });
    });

    $('#btnRtPwdEmail').click(function () {
        var rtEmail = $('#rtpwd_email').val();
        if (rtEmail == null || rtEmail == '' || rtEmail.length < 5 || !rtEmail.match(/^([a-zA-Z0-9_-])+@([a-zA-Z0-9_-])+((\.[a-zA-Z0-9_-]{2,3}){1,2})$/)) {
            swal("找回密码提示", "当前email地址格式不正确，请重新输入", "warning");
            return;
        }
        $server.accessToData("retrievePwdEmail", { "Email": rtEmail }, function (data) { swal(data.title, data.message, data.isRight ? 'success' : 'error') });
    });
});