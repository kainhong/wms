﻿//var app = angular.module('app', ['ui.component', 'yg.services', 'ui.envirment']);


app.factory("app.Ctrl", function (BaseController, Module, DataQueryFactory) {

    function Ctrl($scope, Module, DataQueryFactory) {

        BaseController.call(this, $scope, Module, DataQueryFactory);

        this.init('22010070');
    }

    Ctrl.prototype = Object.create(BaseController.prototype);

    return (Ctrl);

});