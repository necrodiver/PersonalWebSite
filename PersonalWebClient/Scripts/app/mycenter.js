$(function () {

    $('#mycenter_news').click(function () {
        $('.mycenter_right_content').attr('src', 'MyCenterOperation/MessageCenter.html');
    });
    $('#mycenter_newScrawlJottings').click(function () {
        $('.mycenter_right_content').attr('src', 'MyCenterOperation/NewScrawlAndJottings.html');
    });
    $('#mycenter_ScrawlJottings').click(function () {
        $('.mycenter_right_content').attr('src', 'MyCenterOperation/MyScrawlAndJottings.html');
    });
    $('#mycenter_collectScrawlJottings').click(function () {
        $('.mycenter_right_content').attr('src', 'MyCenterOperation/CollectScrawlAndJottings.html');
    });
    $('#mycenter_attentionPerson').click(function () {
        $('.mycenter_right_content').attr('src', 'MyCenterOperation/AttentionPerson.html');
    });
    $('#mycenter_myFans').click(function () {
        $('.mycenter_right_content').attr('src', 'MyCenterOperation/MyFans.html');
    });

    $('#mycenter_editInformation,#editInformation').click(function () {
        $('.mycenter_right_content').attr('src', 'MyCenterOperation/EditInformation.html');
        $('.mycenter_tabs').removeClass('mycenter_tabs_click');
        $('#mycenter_editInformation').addClass('mycenter_tabs_click');
    });

    $('.mycenter_tabs').click(function () {
        $('.mycenter_tabs').removeClass('mycenter_tabs_click');
        $(this).addClass('mycenter_tabs_click');
    });
});