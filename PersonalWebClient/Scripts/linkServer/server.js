var userInfo;
var $server = [];
var serverUrl = 'http://localhost:10841/Service/api/';

//登录
$server.login = function (username, pwd, validatecode) {
    if (!username || !pwd || !validatecode) {
        return false;
    }
    $.post(serverUrl + 'Account/Login', { 'UserName': username, 'PassWord': pwd, 'ValidateCode': validatecode }, function (data) {
        var status;
        try {
            status = $.parseJSON(data);
            if (status['isRight'] || status['isRight'] === 'true') {
                return true;
            } else {
                consoleLog(status);
            }
        } catch (e) {
            console.log(e);
        }
        return false;
    });
}

//获取验证码
$server.getVCF = function () {
    $.post(serverUrl + 'Account/GetVFC', null, function (data) {
        return data;
    });
}

//获取当前登录用户信息
$server.get = function () {
    $.post(serverUrl + 'Account/Get', null, function (data) {
        var status;
        try {
            status = $.parseJSON(data);
            if (status['UserId'].length == 32) {
                userInfo = status;
            }
        } catch (e) {
            console.log(e);
        }
        return false;
    });
}

//修改当前用户信息
$server.edit = function () {
    //服务端写的有问题，需要再次修改
    alert('有问题，需修改');
}

//退出登录
$server.logout = function () {
    $.post(serverUrl + 'Account/Logout', null, function (data) {
        var status;
        try {
            status = $.parseJSON(data);
            if (status['isRight']) {
                return true;
            }
        } catch (e) {
            console.log(e);
        }
        return false;
    });
}

//注册
$server.add = function () {
    alert('注册需要重新整理');
    $.post(serverUrl + 'Account/Add', null, function (data) {

    });
}

//找回密码
$server.retrievePwd = function (email, validatecode) {
    if (!email || !validatecode) {
        return false;
    }
    $.post(serverUrl + 'Account/RetrievePwd', { 'Email': username, 'ValidateCode': validatecode }, function (data) {
        var status;
        try {
            status = $.parseJSON(data);
            if (status['isRight'] || status['isRight'] === 'true') {
                return true;
            } else {
                consoleLog(status);
            }
        } catch (e) {
            console.log(e);
        }
        return false;
    });
}

//找回密码后修改密码
$server.vertifyCode = function (email, validatecode, password) {
    if (!email || !validatecode || !password) {
        return false;
    }
    $.post(serverUrl + 'Account/VertifyCode', { 'Email': email, 'ValidateCode': validatecode, 'Password': password }, function (data) {
        var status;
        try {
            status = $.parseJSON(data);
            if (status['isRight'] || status['isRight'] === 'true') {
                return true;
            } else {
                consoleLog(status);
            }
        } catch (e) {
            console.log(e);
        }
        return false;
    });
}

$server.ceshi = function (num) {
    $.get(serverUrl + 'Account/GetTestValues', { DM: num }, function (data) {
        console.log('从服务器获取的时间：'+data);
    });
}
function consoleLog(status) {
    console.log('isRight:' + status['isRight'] + ',title:' + status['title'] + ',message:' + status['message']);
}
