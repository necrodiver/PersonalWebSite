
var helloModule = angular.module('app', []);
helloModule.config(function ($routeProvider) {
    $routeProvider.when('/hello', {
        templateUrl: 'template/hello.html',
        controller: 'HelloCtrl'
    }).when('/list', {
        templateUrl: 'template/bookList.html',
        controller: 'BookListCtrl'
    }).otherwise({ redirectTo: '/hello' });
});