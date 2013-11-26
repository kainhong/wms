define(function (require) {
    var angular = require('angular');
    //var $ = require('jquery');
    var app = angular.module('app', []);

    app.controller('ctrl', ['$scope', function ($scope) {
        $scope.name = ' world!';       
    }]);
   
    return {
        init: function () {
            angular.bootstrap(document.body, ['app'])
        }
    }
});