var userInfo;
var $server = [];
var serverUrl = 'http://localhost:10841/Service/api/';

$server.accessToDataThis = function (fullUrl, jsonData, isAsync) {
    $.ajax({
        async: isAsync,
        url: fullUrl,
        type: "post",
        dataType: "json",
        data: jsonData,
        success: function (msgObj) {
            return msgObj;
        },
        error: function (xmlHttpRequest, textStatus, errorThrown) {
            console.log(xmlHttpRequest.status);
        }
    });
}

$server.accessToData = function (childUrlName, jsonData, isAsync) {
    return $server.accessToDataThis($server.getChildUrl(childUrlName), jsonData, isAsync);
}

$server.accessToData = function (childUrlName, jsonData) {
    return $server.accessToDataThis($server.getChildUrl(childUrlName), jsonData, true);
}

$server.getFullUrl = function GetChildUrl(urlName) {
    var urlFull = "";
    switch (urlName) {
//Account
        case "user_Login"://登录
            urlFull=serverUrl+ "Account/Login";
            
        case "getVFC"://获取验证码
            urlFull = serverUrl + "Account/GetVFC";

        case "user_edit"://修改用户信息
            urlFull = serverUrl + "Account/Edit";

        case "user_constractNickName"://查询用户昵称是否存在
            urlFull = serverUrl + "Account/ContrastNickName";

        case "user_constractEmail"://查询用户是否存在
            urlFull = serverUrl + "Account/ContrastEmail";

        case "user_add"://添加用户，注册
            urlFull = serverUrl + "Account/Add";

        case "user_getSelf"://获取个人信息
            urlFull = serverUrl + "Account/Get";

        case "user_logout"://退出登录
            urlFull = serverUrl + "Account/Logout";

        case "user_retrievePwd"://找回密码时发送验证码
            urlFull = serverUrl + "Account/RetrievePwd";

        case "user_vertifyCode"://找回密码后的修改密码
            urlFull = serverUrl + "Account/VertifyCode";

//Article
        case "article_add"://添加文章
            urlFull = serverUrl + "Article/Add";

        case "article_getList"://获取文章列表
            urlFull = serverUrl + "Article/GetList";

        case "article_edit"://修改文章
            urlFull = serverUrl + "Article/Edit";

        case "article_delete"://删除文章
            urlFull = serverUrl + "Article/Delete";
//Scrawl
        case "scrawl_add"://添加涂鸦
            urlFull = serverUrl + "Scrawl/Add";

        case "scrawl_getList"://获取涂鸦列表
            urlFull = serverUrl + "Scrawl/GetList";

        case "scrawl_edit"://修改涂鸦
            urlFull = serverUrl + "Scrawl/Edit";

        case "scrawl_delete"://删除涂鸦
            urlFull = serverUrl + "Scrawl/Delete";
//Comment
        case "commend_add"://添加消息
            urlFull = serverUrl + "Comment/Add";

        case "commend_delete"://删除消息
            urlFull = serverUrl + "Comment/Delete";

        case "commend_getList"://查询消息
            urlFull = serverUrl + "Comment/GetList";

        default:
            urlFull =null;
    }
    return urlFull;
}

$server.ceshi = function (num) {
    $.get(serverUrl + 'Account/GetTestValues', { DM: num }, function (data) {
        console.log('从服务器获取的时间：' + data);
    });
}
$server.ceshi1 = function (num) {
    $.get(serverUrl + 'Account/GetTestValues1', { DM: num }, function (data) {
        console.log('从服务器获取的时间：' + data);
    });
}

function consoleLog(status) {
    console.log('isRight:' + status['isRight'] + ',title:' + status['title'] + ',message:' + status['message']);
}
