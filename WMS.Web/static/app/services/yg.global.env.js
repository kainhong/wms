var ygEnvirment = angular.module('ui.envirment', []);
ygEnvirment.factory('env', function ($rootScope) {
    var sharedService = {};

    sharedService.broadcast = function (event, msg) {
        $rootScope.$broadcast(event, msg);
        //console.log(event);
    };

    sharedService.on = function (event, callback) {
        $rootScope.$on(event, callback);
    };

    return sharedService;
});
