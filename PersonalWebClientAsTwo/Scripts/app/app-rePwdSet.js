$(document).ready(function () {
    $('#retrievePassword2').bootstrapValidator({
        message: '这个值是无效的',
        feedbackIcons: {/*输入框不同状态，显示图片的样式*/
            valid: 'fa fa-check',
            invalid: 'fa fa-close',
            validating: 'fa fa-refresh'
        },
        fields: {/*验证*/
            rtPwdSet_password: {
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
                        regexp: /^((?!\d+$)(?![a-zA-Z]+$)[a-zA-Z\d@#$%^&_+].{5,19})+$/,
                        message: '密码只能由字母、数字、字符的最少两个组合'
                    }
                }
            },
            rtPwdSet_confirmPassword: {
                message: '重复密码无效',
                validators: {
                    notEmpty: {},
                    identical: {
                        field: 'password',
                        message: '两次输入的密码不一致'
                    }
                }
            }
        }
    }).on("success.form.bv", function (e) {
        e.preventDefault();
        var rtPwdSet_password = $("#rtPwdSet_password").val();
        var setPwd = {
            "Password": rtPwdSet_password
        };
        $server.accessToData("user_vertifyCode", setPwd, function (data) {
            swal(data.title, data.message, data.isRight ? 'success' : 'error');
            if (data.isRight) {
                $("#retrievePassword2").data('bootstrapValidator').destroy();
                $('#retrievePassword2').data('bootstrapValidator', null);
                location.href = 'SignIn';
            }
        });
    });

    $('#btnRtPwdEmail').click(function () {
        var rtEmail=$('#rtpwd_email').val();
        if (rtEmail == null || rtEmail == '' || rtEmail.length < 5 || !rtEmail.match(/^([a-zA-Z0-9_-])+@([a-zA-Z0-9_-])+((\.[a-zA-Z0-9_-]{2,3}){1,2})$/)) {
            swal("找回密码提示", "当前email地址格式不正确，请重新输入", "warning");
            return;
        }
        $server.accessToData("retrievePwdEmail", { "Email": rtEmail }, function (data) { swal(data.title, data.message, data.isRight ? 'success' : 'error') });
    });
});