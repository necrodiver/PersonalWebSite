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

        }
    });
});
