$(document).ready(function () {
    var $contextTable = $('#contextTable'),
    $deleteContent = $('#btn_DeleteContent');
    $passContent = $('#btn_PassContent');
    $contextTable.bootstrapTable({
        method: 'get',
        toggle: 'table',
        dataType: "json",
        url: "../../../Test/data1.json",
        height: $(window).height() - 200,
        striped: true,
        dataType: "json",
        pagination: true,
        pageSize: 20,
        pageList: [10, 25, 50, 100],
        pageNumber: 1,
        sortable: true,
        sortOrder: 'desc',
        //sidePagination: "server",//分页条件
        queryParams: function (params) {//查询参数，用于自定义查询传的条件参数
            var subcompany = $('#subcompany option:selected').val();
            var name = $('#name').val();
            return {
                pageNumber: params.offset + 1,
                pageSize: params.limit,
                companyId: subcompany,
                name: name
            };
        },
        search: false,
        idField: "id",
        uniqueId: "id",
        queryParamsType: "limit", //参数格式,发送标准的RESTFul类型的参数请求
        showRefresh: false, //显示 刷新按钮
        searchOnEnterKey: true,  //设置为 true时，按回车触发搜索方法，否则自动触发搜索方法
        columns: [{
            field: 'id',
            title: "id",
            checkbox: true,
            width: '5%'
        }, {
            title: '状态',
            align: 'center',
            width: '5%',
            formatter: function (value, row, index) {
                //return '<em class="fa fa-ban edit_isRead em_delete"  title="未通过"></em>';
                //return '<em class="fa fa-check edit_isRead em_reply" title="通过"></em>';
                return '<em class="fa fa-envelope edit_isRead" title="未读"></em>';
            }
        }, {
            field: 'price',
            title: '错误等级',
            width: '5%',
            formatter: function (value, row, index) {
                return value;
                //return "精品";
                //return "推荐";
            }
        }, {
            field: 'name',
            width: '20%',
            title: '错误标题',
            align: 'center',
            formatter: function (value, row, index) {
                return value;
            }
        }, {
            field: 'price',
            title: '错误内容',
            width: '45%',
            formatter: function (value, row, index) {
                return value;
            }
        }, {
            field: 'time',
            title: '创建日期',
            width: '10%',
            formatter: function (value, row, index) {
                var myDate = new Date();
                var year = myDate.getFullYear();
                var month = myDate.getMonth() + 1;
                var date = myDate.getDate();
                var h = myDate.getHours();
                var m = myDate.getMinutes();
                var s = myDate.getSeconds();
                function p(s) {
                    return s < 10 ? '0' + s : s;
                }
                var now = year + '-' + p(month) + "-" + p(date) + " " + p(h) + ':' + p(m) + ":" + p(s);
                return '<p style="font-size:10px;">' + now + '</p>';
            }
        }, {
            field: null,
            title: '操作',
            width: '20%',
            align: 'center',
            formatter: function (value, row, index) {
                return $('#tableEditTemplate').html().format(value);
            },
            events: {
                //'click .table_edit_read': function (e, value, row, index) {
                //    alert("这里需要写个链接传随笔内容");
                //},
                'click .table_edit_delete': function (e, value, row, index) {
                    var num = $(this).attr('editId');
                    swal({
                        title: "你确认要删除选中的消息？" + index,
                        text: "这些消息将会被删除而无法找到，请确认是否存在未读消息，或者有未接收的附件",
                        type: "warning",
                        showCancelButton: true,
                        confirmButtonColor: "#DD6B55",
                        confirmButtonText: "是的，删除",
                        cancelButtonText: "不，让我再想想",
                        closeOnConfirm: false,
                        closeOnCancel: false
                    }, function (isConfirm) {
                        if (isConfirm) {
                            $contextTable.bootstrapTable('removeByUniqueId', num);
                            swal("已删除！", "所选中的项删除成功！", "success");
                        } else {
                            swal.close();
                        }
                    });
                }
                //'click .table_edit_edit': function (e, value, row, index) {
                //    var userId = $(this).attr('editId');
                //    location.href = "EditUser.html?userId=" + userId;
                //},
            }
        }]

    });
    $(".form_datetime").datetimepicker({
        format: 'yyyy-mm-dd hh:ii',
        language: 'zh-CN'
    });

    $deleteContent.click(function () {
        var ids = $.map($contextTable.bootstrapTable('getSelections'), function (row) {
            return row.id;
        });
        if (ids.length >= 1) {
            swal({
                title: "你确定要删除选中的项？",
                text: "请输入删除操作key:",
                type: "input",
                showCancelButton: true,
                closeOnConfirm: false,
                animation: "slide-from-top",
                inputPlaceholder: "删除操作key"
            }, function (inputValue) {
                if (inputValue === false)
                    return false;
                if (inputValue === "") {
                    swal.showInputError("请输入操作key才能执行删除操作！");
                    return false
                }
                if (inputValue === "666") {
                    swal("删除成功!", "你已删除所有选中项！", "success");
                    return true;
                }
                swal.showInputError("请输入正确的执行key！");
                return false;
            });
        } else {
            //['error', 'warning', 'info', 'success', 'input', 'prompt']
            swal("删除提示", "请先选择要删除的项~~", "info");
        }
    });
    $passContent.click(function () {
        var ids = $.map($contextTable.bootstrapTable('getSelections'), function (row) {
            return row.id;
        });
        if (ids.length >= 1) {
            swal({
                title: "你确认要执行通过选中的作品？",
                text: "被选中的用户将会通过，这些作品将会面向大众",
                type: "warning",
                showCancelButton: true,
                confirmButtonText: "是的，通过",
                cancelButtonText: "不，让我再想想",
                closeOnConfirm: false,
                closeOnCancel: false
            }, function (isConfirm) {
                if (isConfirm) {
                    swal("已通过！", "所有选中的项通过成功！", "success");
                } else {
                    swal.close();
                }
            });
        } else {
            //['error', 'warning', 'info', 'success', 'input', 'prompt']
            swal("删除提示", "请先选择要删除的项~~", "info");
        }
    });

    $('#summernoteAdd,#summernoteEdit').summernote({
        lang: 'zh-CN',
        fontNames: ['宋体', '微软雅黑', '楷体', '黑体', '隶书', 'andale mono', 'arial', 'arial black', 'comic sans ms', 'impact']
    });
    $('#btnNickNameSearch').click(function () {
        var users = $('#textUsers').val().split(';');
        if (users.length > 0) {
            users.forEach(function (val, index) {
                if (val.length > 1) {
                    $('.new_mail_users').append($('#userSearchTemplate').html().format('../../../Images/user-ceshi.jpg', val));
                    $('.remove_user').click(function () {
                        $(this).parent('.users_context').remove();
                    });
                }
            });
        }
    });

    $('#btn_NewContent').click(function () {

    });

    $('#btnUserType').change(function () {
        var val = $(this).children('.active.focus').children('input').val();
        if (val == '1' || val == '2')
            $('.new_mail_userCtrl').addClass('hidden');
        else
            $('.new_mail_userCtrl').removeClass('hidden');
    });

    $('#btnTableRefresh').click(function () {
        $contextTable.bootstrapTable('refresh', { url: '../../../Test/data1.json' });
    });

    $('#signUp').bootstrapValidator({
        message: '这个值是无效的',
        feedbackIcons: {/*输入框不同状态，显示图片的样式*/
            valid: 'glyphicon glyphicon-ok',
            invalid: 'glyphicon glyphicon-remove',
            validating: 'glyphicon glyphicon-refresh'
        },
        fields: {/*验证*/
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
                    //,
                    //remote: {/*远程验证*/
                    //    url: 'remote.aspx',
                    //    message: '此用户名已被占用，请换一个尝试'
                    //}
                }
            },
            email: {
                validators: {
                    notEmpty: {
                        message: '电子邮件是必填的'
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
                        regexp: /^((?!\d+$)(?![a-zA-Z]+$)[a-zA-Z\d@#$%^&_+].{5,19})+$/,
                        message: '密码只能由字母、数字、字符的最少两个组合'
                    }
                }
            }, confirmPassword: {
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
    });
    $("#inputUserName").bsSuggest('init', {
        /*url: "/rest/sys/getuserlist?keyword=",
        effectiveFields: ["userName", "email"],
        searchFields: [ "shortAccount"],
        effectiveFieldsAlias:{userName: "姓名"},*/
        clearable: false,
        url: "../../../Test/bootstrapSuggestPlugin/data.json",
        idField: "userId",
        keyField: "userName"
    }).on('onDataRequestSuccess', function (e, result) {
        console.log('onDataRequestSuccess: ', result);
    }).on('onSetSelectValue', function (e, keyword, data) {
        console.log('onSetSelectValue: ', keyword, data);//F:\Codes\PersonalWebSite\PersonalWebClient\Test/bootstrapSuggestPlugin/data.json
    }).on('onUnsetSelectValue', function () {
        console.log('onUnsetSelectValue');
    });
});
