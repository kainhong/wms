var app = angular.module('app', ['ui.component', 'yg.services', 'ui.envirment']);
app.provider({
    $exceptionHandler: function () {
        var handler = function (exception, cause) {
            //debugger;
            //alert(exception);
        };

        this.$get = function () {
            return handler;
        };
    }
});

app.controller('globalCtrl', ['$scope', 'System','env', function ($scope, System,env) {
    System.list({}, function (data) {
        $scope.systems = data[0];
    });

    $scope.recentModules = [];
    $scope.menuClick = function (node) {
       
    }

    $scope.openModule = function(module)
    {
        env.broadcast("global.tree.node.selected", module);
        return false;
    }
} ]);