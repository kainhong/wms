//var app = angular.module('app', ['ui.component', 'yg.services', 'ui.envirment']);


app.factory("app.Ctrl", function (BillController, Module, DataQueryFactory, $timeout) {

    function Ctrl($scope, Module, DataQueryFactory) {
        $scope.viewType = 'bill';
        $scope.click = function () {
            $scope.viewType = $scope.viewType + "s";
        }
        $scope.BillNOFieldName = 'RECEIPT_NO';

        BillController.call(this, $scope, Module, DataQueryFactory);

        this.init('22060020');
    }

    Ctrl.prototype = Object.create(BillController.prototype);

    return (Ctrl);

});