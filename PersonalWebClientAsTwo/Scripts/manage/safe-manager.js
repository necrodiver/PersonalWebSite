/// <reference path="../jquery/jquery-1.10.2.js" />
$(document).ready(function () {
    $('.safe_lni_content').click(function () {
        $('.list_child_group').addClass('hidden');
        if ($(this).children('.safe_lnic_right').hasClass('fa-chevron-right')) {
            $('.list_child_item').removeClass('list_child_item_select');
            $('.safe_lnic_right').removeClass('fa-chevron-down');
            $('.safe_lnic_right').addClass('fa-chevron-right');
            $('.safe_lni_content').removeClass('safe_lni_content_select');
            $(this).children('.safe_lnic_right').removeClass('fa-chevron-right');
            $(this).children('.safe_lnic_right').addClass('fa-chevron-down');
            $(this).addClass('safe_lni_content_select');
            $(this).next('.list_child_group').removeClass('hidden');
        } else {
            $(this).children('.safe_lnic_right').removeClass('fa-chevron-down');
            $(this).children('.safe_lnic_right').addClass('fa-chevron-right');
            $(this).removeClass('safe_lni_content_select');
        }
        if ($(this).hasClass('select_index')) {
            $.get('/Client/Admin/ManagerOperation/Context', function (data) {
                $('.safe_rc_context').html(data);
            });
            $('.safe_right_title p').text('首页');
        }
        else if ($(this).hasClass('select_user')) {
            $.get('/Client/Admin/UserControl/UserCtrl', function (data) {
                $('.safe_rc_context').html(data);
            });
            $('.safe_right_title p').text('用户管理');
        }
        else if ($(this).hasClass('select_system')) {
            $('.safe_rc_iframe').attr('src', 'UserControl/system.html');
            $('.safe_right_title p').text('系统设定   ——暂时为空，以后根据需求逐步添加');
        }
    });
    $('.list_child_item').click(function () {
        if (!$(this).hasClass('list_child_item_select')) {
            $('.list_child_item').removeClass('list_child_item_select');
            $(this).addClass('list_child_item_select');

            $('.safe_lni_content').removeClass('safe_lni_content_select');
            $('.safe_lnic_right').removeClass('fa-chevron-down');
            $('.safe_lnic_right').addClass('fa-chevron-right');

            $(this).parent('.list_child_group').prev('.safe_lni_content').children('.safe_lnic_right').addClass('fa-chevron-down');
            $(this).parent('.list_child_group').prev('.safe_lni_content').addClass('safe_lni_content_select');

            if ($(this).hasClass('select_newMail')) {
                $.get('/Client/Admin/ManagerOperation/Mail', function (data) {
                    $('.safe_rc_context').html(data);
                });
                $('.safe_right_title p').text('邮件');
            }
            else if ($(this).hasClass('select_workRemind')) {
                $.get('/Client/Admin/ManagerOperation/WorkReminder', function (data) {
                    $('.safe_rc_context').html(data);
                });
                $('.safe_right_title p').text('工作提醒');
            }
            else if ($(this).hasClass('select_userMessage')) {
                $.get('/Client/Admin/ManagerOperation/UserMessage', function (data) {
                    $('.safe_rc_context').html(data);
                });
                $('.safe_right_title p').text('用户反馈');
            }
            else if ($(this).hasClass('select_sysNotice')) {
                $.get('/Client/Admin/ManagerOperation/SystemNotice', function (data) {
                    $('.safe_rc_context').html(data);
                });
                $('.safe_right_title p').text('系统公告');
            }
            else if ($(this).hasClass('select_Scrawl')) {
                $.get('/Client/Admin/ProductionOperation/ScrawlManage', function (data) {
                    $('.safe_rc_context').html(data);
                });
                $('.safe_right_title p').text('涂鸦管理');
            }
            else if ($(this).hasClass('select_Jottings')) {
                $.get('/Client/Admin/ProductionOperation/JottingsManage', function (data) {
                    $('.safe_rc_context').html(data);
                });
                $('.safe_right_title p').text('随笔管理');
            }
            else if ($(this).hasClass('select_visit')) {
                $.get('/Client/Admin/Statistics/VisitStatistics', function (data) {
                    $('.safe_rc_context').html(data);
                });
                $('.safe_right_title p').text('访问统计');
            }
            else if ($(this).hasClass('select_error')) {
                $.get('/Client/Admin/Statistics/ErrorStatistics', function (data) {
                    $('.safe_rc_context').html(data);
                });
                $('.safe_right_title p').text('错误记录');
            }
        }
    });
    //$.get('/Client/Admin/ManagerOperation/Context', function (data) {
    //    $('.safe_rc_context').html(data);
    //});
});
