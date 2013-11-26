var app = angular.module('app', ['ngRoute']);

app.provider({
    $exceptionHandler: function () {
        var handler = function (exception, cause) {
            console.log(exception);
        };

        this.$get = function () {
            return handler;
        };
    }
});
app.config(function ($routeProvider) {
    $routeProvider.
      when('/', { controller: 'ListCtrl', templateUrl: 'list.html' }).
      when('/edit/:projectId', { controller: 'EditCtrl', templateUrl: 'detail.html' }).
      otherwise({ redirectTo: '/' });
});

app.controller('ctrl', function ($scope) {
    $scope.todo = {id:0,name:''};
});

app.controller('EditCtrl', function ($scope) {
    $scope.todo = { id: 0, name: 'detail' };
});

app.controller('ListCtrl', function ($scope) {
    $scope.todo = { id: 0, name: 'list' };
});

