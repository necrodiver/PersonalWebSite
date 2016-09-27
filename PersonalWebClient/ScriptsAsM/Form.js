var userInfoModule = angular.module('UserInfoModule', []);
userInfoModule.controller('MyCtrl', ['$scope', function ($scope) {
    $scope.userInfo = {
        email: '12445123@163.com',
        password: '162413151',
        autoLogin: true
    };
    $scope.getFormData = function () {
        console.log($scope.userInfo);
    }
    $scope.setFormData = function () {
        $scope.userInfo = {
            email: '34673@qq.com',
            password: 'ut3436', autoLogin: false
        };
    }
    $scope.resetFormData = function () {
        $scope.userInfo = {
            email: '12445123@163.com',
            password: '162413151',
            autoLogin: true
        };
    }
}]);