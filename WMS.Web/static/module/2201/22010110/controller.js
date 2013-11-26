//var app = angular.module('app', ['ui.component', 'yg.services', 'ui.envirment']);


app.factory("app.Ctrl", function (BaseController,$location, Module, DataQueryFactory) {

    function Ctrl($scope, $location, Module, DataQueryFactory) {
        $scope.updateRow = function () {
            var dataQuery = $scope.currentDataQuery;
            if (!dataQuery)
                return;
            var row = dataQuery.focusRow;
            if (row == null || !dataQuery.view)
                return;
            var grid = dataQuery.view;
            grid.datagrid('updateRow', {
                index: dataQuery.focusIndex,
                row:row
            });
        };

        BaseController.call(this, $scope, $location, Module, DataQueryFactory);
        this.init('22010110');
    }

    Ctrl.prototype = Object.create(BaseController.prototype);

    return (Ctrl);

});