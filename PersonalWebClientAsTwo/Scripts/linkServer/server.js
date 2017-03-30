/// <reference path="../jquery/jquery-3.1.1.js" />
/// <reference path="../jquery/jquery-1.10.2.js" />

var userInfo;
var $server = [];
//var serverUrl = 'http://localhost:10841/Service/api/';
var serverUrl = 'http://localhost:4525/Client/';
var isPost = true;
var isAsync = true;
jQuery.support.cors = true;

$server.accessToDataThis = function (fullUrl, jsonData, manage) {
    $.ajax({
        async: isAsync,
        url: fullUrl,
        type: isPost ? "POST" : "GET",
        dataType: "json",
        contentType: "application/x-www-form-urlencoded",//这个是默认方式，可写可不写
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
            childUrl = "Sign/Login";
            break;
        case "user_LoginIndex"://登录Index
            childUrl = "Sign/LoginIndex";
            break;
        case "getVFCIndex"://获取验证码Index
            childUrl = "Sign/GetVerificationCode";
            break;
        case "getVFC"://获取验证码
            childUrl = "Sign/GetVerificationCode2";
            break;
        case "user_constractNickNameWeb"://查询用户昵称是否存在
            childUrl = "Sign/ContrastNickNameWeb";
            break;
        case "user_constractEmail"://查询用户邮箱是否已存在
            childUrl = "Sign/ContrastEmail";
            break;
        case "user_contrastEmailWeb"://查询用户邮箱地址
            childUrl = "Sign/ContrastEmailWeb";
            break;
        case "user_register"://注册用户
            childUrl = "Sign/Register";
            break;
        case "user_vertifyCode"://找回密码后的修改密码
            childUrl = "Sign/VertifyCode";
            break;
        case "registerSendEmail"://发送邮件确认信息
            childUrl = "Sign/RegisterSendEmail";
            break;
        case "registerReEmail"://发送邮件确认信息
            childUrl = "Sign/ContrastNotEmail";
            break;
        case "retrieveVFCAndEmail"://找回密码第一步提交
            childUrl = "Sign/RetrieveVFCAndEmail";
            break;
        case "retrievePwdEmail"://找回密码发送邮件确认信息
            childUrl = "Sign/RetrievePwdEmail";
            break;
        case "logout"://退出
            childUrl = "Sign/Logout";
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
        default:
            childUrl = null;
            break;
    }
    return serverUrl + childUrl;
}

$server.logReturn = function consoleLog(status) {
    console.log('isRight:' + status['isRight'] + ',title:' + status['title'] + ',message:' + status['message']);
}
