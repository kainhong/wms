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

app.controller('globalCtrl', ['$scope', 'System', function ($scope, System) {
    System.list({}, function (data) {
        $scope.systems = data[0];
    });

    $scope.menuClick = function (node) {
        //alert(node.attributes.moduleId);
    }
} ]);