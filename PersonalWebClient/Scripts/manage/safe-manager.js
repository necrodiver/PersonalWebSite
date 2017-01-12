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
            $('.safe_rc_iframe').attr('src', 'ManagerOperation/Index.html');
            $('.safe_right_title p').text('首页');
        }
        else if ($(this).hasClass('select_user')) {
            $('.safe_rc_iframe').attr('src', 'UserControl/UserCtrl.html');
            $('.safe_right_title p').text('用户管理');
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
                $('.safe_rc_iframe').attr('src', 'ManagerOperation/Mail.html');
                $('.safe_right_title p').text('邮件');
            }
            else if ($(this).hasClass('select_workRemind')){
                $('.safe_rc_iframe').attr('src', 'ManagerOperation/WorkReminder.html');
                $('.safe_right_title p').text('工作提醒');
            }
            else if ($(this).hasClass('select_userMessage')) {
                $('.safe_rc_iframe').attr('src', 'ManagerOperation/UserMessage.html');
                $('.safe_right_title p').text('用户反馈');
            }
            else if ($(this).hasClass('select_sysNotice')) {
                $('.safe_rc_iframe').attr('src', 'ManagerOperation/SystemNotice.html');
                $('.safe_right_title p').text('系统公告');
            }
           

        }
    });
});
