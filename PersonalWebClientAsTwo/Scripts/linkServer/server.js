/// <reference path="../jquery/jquery-3.1.1.js" />

var userInfo;
var $server = [];
var serverUrl = 'http://localhost:10841/Service/api/';
var isPost = true;
var isAsync = true;
jQuery.support.cors = true;

$server.accessToDataThis = function (fullUrl, jsonData, manage) {
    $.ajax({
        async: isAsync,
        url: fullUrl,
        type: isPost ? "POST" : "GET",
        dataType: "json",
        data: jsonData,
        success: function (dataJson) {
            console.log("msgObj的值:" + dataJson);
            manage(dataJson);
        },
        error: function (xmlHttpRequest, textStatus, errorThrown) {
            console.log(xmlHttpRequest.status);
        }
    });
}

$server.accessToData = function (childUrlName, jsonData, manage) {
    return $server.accessToDataThis($server.getFullUrl(childUrlName), jsonData, manage);
}

$server.getFullUrl = function GetChildUrl(urlName) {
    var childUrl = "";
    switch (urlName) {
        //Account
        case "user_Login"://登录
            childUrl = "Account/Login";
            break;
        case "getVFC"://获取验证码
            childUrl = "Account/GetVFC";
            break;
        case "user_edit"://修改用户信息
            childUrl = "Account/Edit";
            break;
        case "user_constractNickName"://查询用户昵称是否存在
            childUrl = "Account/ContrastNickName";
            break;
        case "user_constractEmail"://查询用户是否存在
            childUrl = "Account/ContrastEmail";
            break;
        case "user_register"://注册用户
            childUrl = "Account/FirstRegisterUserInfo";
            break;
        case "user_add"://添加用户-----------------------------此方法将挪到管理端
            childUrl = "Account/Add";
            break;
        case "user_getSelf"://获取个人信息
            childUrl = "Account/Get";
            break;
        case "user_logout"://退出登录
            childUrl = "Account/Logout";
            break;
        case "user_retrievePwd"://找回密码时发送验证码
            childUrl = "Account/RetrievePwd";
            break;
        case "user_vertifyCode"://找回密码后的修改密码
            childUrl = "Account/VertifyCode";
            break;
            //Article
        case "article_add"://添加文章
            childUrl = "Article/Add";
            break;
        case "article_getList"://获取文章列表
            childUrl = "Article/GetList";
            break;
        case "article_edit"://修改文章
            childUrl = "Article/Edit";
            break;
        case "article_delete"://删除文章
            childUrl = "Article/Delete";
            break;
            //Scrawl
        case "scrawl_add"://添加涂鸦
            childUrl = "Scrawl/Add";
            break;
        case "scrawl_getList"://获取涂鸦列表
            childUrl = "Scrawl/GetList";
            break;
        case "scrawl_edit"://修改涂鸦
            childUrl = "Scrawl/Edit";
            break;
        case "scrawl_delete"://删除涂鸦
            childUrl = "Scrawl/Delete";
            break;
            //Comment
        case "commend_add"://添加消息
            childUrl = "Comment/Add";
            break;
        case "commend_delete"://删除消息
            childUrl = "Comment/Delete";
            break;
        case "commend_getList"://查询消息
            childUrl = "Comment/GetList";
            break;
        case "registerSendEmail"://发送邮件确认信息
            childUrl = "Comment/RegisterSendEmail";
            break;

        case "Test"://用于测试
            childUrl = "Account/GetTestValuesModal";
            break;
        default:
            childUrl = null;
            break;
    }
    return serverUrl + childUrl;
}




$server.ceshi = function (num) {
    $.get(serverUrl + 'Account/GetTestValuesModal', { DM: num }, function (data) {
        console.log('从服务器获取的时间：' + data);
    });
}

$server.ceshi1 = function (num) {
    isPost = false;
    $server.accessToData("Test", { "DM": num }, function (a) { console.log("a的值：" + a); });
}

$server.logReturn = function consoleLog(status) {
    console.log('isRight:' + status['isRight'] + ',title:' + status['title'] + ',message:' + status['message']);
}
