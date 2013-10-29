//var app = angular.module('app', ['ui.component', 'yg.services', 'ui.envirment']);


app.factory("app.Ctrl", function (BaseController, Module, DataQueryFactory) {

    function Ctrl($scope, Module, DataQueryFactory) {
        $scope.BillNOFieldName = 'BillNO';

        BaseController.call(this, $scope, Module, DataQueryFactory);

        this.init('22050010');

        $scope.$on('onDblClickRow', function (event, args) {
            var query = args.dataquery;
            if (query.Name != 'dqBillList')
                return;
            
            console.log(query);
        });
    }

    Ctrl.prototype = Object.create(BaseController.prototype);

    return (Ctrl);

});