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
    var urlFull = "";
    switch (urlName) {
        //Account
        case "user_Login"://登录
            urlFull = serverUrl + "Account/Login";
            break;
        case "getVFC"://获取验证码
            urlFull = serverUrl + "Account/GetVFC";
            break;
        case "user_edit"://修改用户信息
            urlFull = serverUrl + "Account/Edit";
            break;
        case "user_constractNickName"://查询用户昵称是否存在
            urlFull = serverUrl + "Account/ContrastNickName";
            break;
        case "user_constractEmail"://查询用户是否存在
            urlFull = serverUrl + "Account/ContrastEmail";
            break;
        case "user_add"://添加用户，注册
            urlFull = serverUrl + "Account/Add";
            break;
        case "user_getSelf"://获取个人信息
            urlFull = serverUrl + "Account/Get";
            break;
        case "user_logout"://退出登录
            urlFull = serverUrl + "Account/Logout";
            break;
        case "user_retrievePwd"://找回密码时发送验证码
            urlFull = serverUrl + "Account/RetrievePwd";
            break;
        case "user_vertifyCode"://找回密码后的修改密码
            urlFull = serverUrl + "Account/VertifyCode";
            break;
            //Article
        case "article_add"://添加文章
            urlFull = serverUrl + "Article/Add";
            break;
        case "article_getList"://获取文章列表
            urlFull = serverUrl + "Article/GetList";
            break;
        case "article_edit"://修改文章
            urlFull = serverUrl + "Article/Edit";
            break;
        case "article_delete"://删除文章
            urlFull = serverUrl + "Article/Delete";
            break;
            //Scrawl
        case "scrawl_add"://添加涂鸦
            urlFull = serverUrl + "Scrawl/Add";
            break;
        case "scrawl_getList"://获取涂鸦列表
            urlFull = serverUrl + "Scrawl/GetList";
            break;
        case "scrawl_edit"://修改涂鸦
            urlFull = serverUrl + "Scrawl/Edit";
            break;
        case "scrawl_delete"://删除涂鸦
            urlFull = serverUrl + "Scrawl/Delete";
            break;
            //Comment
        case "commend_add"://添加消息
            urlFull = serverUrl + "Comment/Add";
            break;
        case "commend_delete"://删除消息
            urlFull = serverUrl + "Comment/Delete";
            break;
        case "commend_getList"://查询消息
            urlFull = serverUrl + "Comment/GetList";
            break;
        case "Test":
            urlFull = serverUrl + "Account/GetTestValuesModal";
            break;
        default:
            urlFull = null;
            break;
    }
    return urlFull;
}

$server.ceshi = function (num) {
    $.get(serverUrl + 'Account/GetTestValuesModal', { DM: num }, function (data) {
        console.log('从服务器获取的时间：' + data);
    });
}

$server.ceshi1 = function (num) {
    isPost = false;
    $server.accessToData("Test", { "DM": num }, function (a) {console.log("a的值：" + a);});
}

function consoleLog(status) {
    console.log('isRight:' + status['isRight'] + ',title:' + status['title'] + ',message:' + status['message']);
}
